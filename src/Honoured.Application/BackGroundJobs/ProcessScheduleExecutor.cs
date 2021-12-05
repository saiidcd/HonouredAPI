using Honoured.ArtWorks;
using Honoured.Deliveries;
using Honoured.Interfaces;
using Honoured.ArtLovers;
using Honoured.Subscriptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Uow;

namespace Honoured.BackGroundJobs
{
    [ExposeServices(typeof(IProcessScheduleExecutor))]
    public class ProcessScheduleExecutor : IProcessScheduleExecutor, ITransientDependency
    {

        #region Fields
        private IArtLoverRepository _subRepo;
        private IRepository<Delivery, long> _delRepo;
        private IRepository<ArtWork, long> _artRepo;
        private IUnitOfWorkManager _UoWManager;
        private int GracePeriod = 0;
        private string DeliveryreportPath = @"d:\Logs\Honoured\Deliveries";
        private int DefaultSubscriptionDuration = 3;
        #endregion Fields
        #region Ctors
        public ProcessScheduleExecutor(IArtLoverRepository subRepo,
                                        IRepository<Delivery,long> delRepo,
                                        IRepository<ArtWork, long> artRepo,
                                        IUnitOfWorkManager mgr)
        {
            _subRepo = subRepo;
            _delRepo = delRepo;
            _artRepo = artRepo;
            _UoWManager = mgr;
        }
        #endregion Ctors

        #region Implementations

        #region IProcessScheduleexecutor
        public void GenerateSchedule() => GenerateSchedule(DateTime.Now);
        public async void GenerateSchedule(DateTime effectiveDate)
        {
            //TODO questions to ask:
            //whtat do we do in case there is no available art in the ArtLover's cateogry(ies)?
            //Should repeat placements be optional or just part of the plan (system will avoid when possible)?

            var minDate = effectiveDate.AddDays(GracePeriod * -1);
            var maxDate = effectiveDate.AddDays(GracePeriod);
            var arts = await _artRepo.GetListAsync(a => a.Status == Enumerations.ArtStatus.Available);
            var placedArts = await _artRepo.GetListAsync(a => a.Status == Enumerations.ArtStatus.Placed);
            //Check all active subscriptions where active delivery date = processing date +/- gracePeriod
            var subs = await _subRepo.GetListAsync(s=> s.NextPlacementDate < maxDate);
            var deliveries = new List<Delivery>(subs.Count);
            for (int i = 0; i < subs.Count; i++)
            {
                var sub = subs.ElementAt(i);
                var oldArt = placedArts.FirstOrDefault(arts => arts.Id == sub.Placements.FirstOrDefault().ArtWorkId);
                //create a delivery to retrieve the existing placement
                var newDelivery = new Delivery
                {
                    Address = sub.Profile.DeliveryPoints.FirstOrDefault(),
                    OldArtWorkId = oldArt == null ? 0 :oldArt.Id,
                    OldArtworkTitle = oldArt == null ? "" : oldArt.Title,
                    Status = Enumerations.DeliveryStatus.New,
                    StatusDate = DateTime.Now,
                    ArtLoverId = sub.Id,
                    ArtLoverName = sub.Profile.GetFullName(),
                    Type = Enumerations.DeliveryType.Pickup,
                    Phone = sub.Profile.GetPhone()?.Value
                };
                ArtWork artWork = null;
                //Match with a new artwork
                foreach(var art in arts)
                {
                    
                    if (sub.PreviousArtIds.Contains(art.Id)) continue;
                    if (sub.Categories.Select(a=>a.ArtDisciplineId)
                            .Intersect(art.Categories.Select(a=>a.Id))
                            .Any())
                    {
                        artWork = art;
                        break;
                    }
                }

                if (artWork != null)
                {
                    //Add artwork to ArtLover's history (should we wait for confirmed delivery)
                    sub.AddToArtHistory(artWork.Id);
                    //and add new artwork to delivery
                    newDelivery.NewArtWorkId = artWork.Id;
                    newDelivery.Type = Enumerations.DeliveryType.Mixed;
                    newDelivery.NewArtworkTitle = artWork.Title;
                    artWork.Status = Enumerations.ArtStatus.InTransit;
                    sub.Placements.FirstOrDefault().Status = Enumerations.PlacementStatus.ScheduledForPickup;
                    sub.NextPlacementDate = sub.Status == Enumerations.ArtLoverStatus.Active
                                                            ? DateTime.Now.AddMonths(DefaultSubscriptionDuration)
                                                            : DateTime.MaxValue;
                    using var uow = _UoWManager.Begin(true);
                    await _subRepo.UpdateAsync(sub);
                    await _delRepo.InsertAsync(newDelivery);
                    await _artRepo.UpdateAsync(artWork);
                    await uow.CompleteAsync();
                }
                deliveries.Add(newDelivery);
            }
            GeenrateDeliveriesRepost(deliveries);
        }

        #endregion IProcessScheduleexecutor

        #region IDisposable
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        #endregion IDisposable
        #endregion Implementations


        #region Private Methods
        private void GeenrateDeliveriesRepost(List<Delivery> deliveries)
        {
           StringBuilder writer = new StringBuilder(deliveries.Count * 100);
            writer.AppendLine("Id,ArtId,ArtName,ArtLoverId,ArtLoverName,Address,Phone,pickup");
            foreach (var del in deliveries)
            {
                if(del.Type== Enumerations.DeliveryType.Mixed || del.Type== Enumerations.DeliveryType.Pickup)
                    writer.AppendLine($"{del.Id},{del.OldArtWorkId},{del.OldArtworkTitle},{del.ArtLoverId},{del.ArtLoverName},{del.Address.ToString()},{del.Phone},yes");
               
                if (del.Type == Enumerations.DeliveryType.Mixed || del.Type == Enumerations.DeliveryType.DropOff)
                    writer.AppendLine($"{del.Id},{del.NewArtWorkId},{del.NewArtworkTitle},{del.ArtLoverId},{del.ArtLoverName},{del.Address.ToString()},{del.Phone},no");
            }
            var randPart = Path.GetRandomFileName();
            var dt = DateTime.Now;
            var dateString = string.Format("{0:yyyyMMdd}", dt);
            var path = Path.Combine(DeliveryreportPath, $"{dateString}-{randPart}");

            path = Path.ChangeExtension(path, "csv");
            File.WriteAllText(path, writer.ToString());
        }
        #endregion Private Methods
    }
}
