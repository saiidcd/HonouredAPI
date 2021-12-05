using Honoured.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Honoured.Permissions
{
    public class HonouredPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {

            //var myGroup = context.AddGroup(HonouredPermissions.GroupName);
            var HonouredGroup = context.AddGroup(HonouredPermissions.GroupName, L("Permission:Honoured"));
            //Define your own permissions here. Example:
            //myGroup.AddPermission(HonouredPermissions.MyPermission1, L("Permission:MyPermission1"));
            var DisciplinePermissions = HonouredGroup.AddPermission(HonouredPermissions.Disciplines.Default, L("Permission:Discipline"));
            DisciplinePermissions.AddChild(HonouredPermissions.Disciplines.Create, L("Permission:Discipline.Create"));
            DisciplinePermissions.AddChild(HonouredPermissions.Disciplines.Update, L("Permission:Discipline.Update"));
            DisciplinePermissions.AddChild(HonouredPermissions.Disciplines.Delete, L("Permission:Discipline.Delete"));

            var ArtistPermissions = HonouredGroup.AddPermission(HonouredPermissions.Artists.Default, L("Permission:Artist"));
            ArtistPermissions.AddChild(HonouredPermissions.Artists.Create, L("Permission:Artist.Create"));
            ArtistPermissions.AddChild(HonouredPermissions.Artists.Update, L("Permission:Artist.Update"));
            ArtistPermissions.AddChild(HonouredPermissions.Artists.Delete, L("Permission:Artist.Delete"));
            ArtistPermissions.AddChild(HonouredPermissions.Artists.Profile, L("Permission:Artist.Profile"));
            ArtistPermissions.AddChild(HonouredPermissions.Artists.Portfolio, L("Permission:Artist.Portfolio"));

            var ArtWorkPermissions = HonouredGroup.AddPermission(HonouredPermissions.ArtWorks.Default, L("Permission:ArtWork"));
            ArtWorkPermissions.AddChild(HonouredPermissions.ArtWorks.Create, L("Permission:ArtWork.Create"));
            ArtWorkPermissions.AddChild(HonouredPermissions.ArtWorks.Update, L("Permission:ArtWork.Update"));
            ArtWorkPermissions.AddChild(HonouredPermissions.ArtWorks.Delete, L("Permission:ArtWork.Delete"));

            var ArtLoverPermissions = HonouredGroup.AddPermission(HonouredPermissions.ArtLovers.Default, L("Permission:ArtLover"));
            ArtLoverPermissions.AddChild(HonouredPermissions.ArtLovers.Create, L("Permission:ArtLover.Create"));
            ArtLoverPermissions.AddChild(HonouredPermissions.ArtLovers.Update, L("Permission:ArtLover.Update"));
            ArtLoverPermissions.AddChild(HonouredPermissions.ArtLovers.Delete, L("Permission:ArtLover.Delete"));

            var PlacementPermissions = HonouredGroup.AddPermission(HonouredPermissions.Placements.Default, L("Permission:Placement"));
            PlacementPermissions.AddChild(HonouredPermissions.Placements.Create, L("Permission:Placement.Create"));
            PlacementPermissions.AddChild(HonouredPermissions.Placements.Update, L("Permission:Placement.Update"));
            PlacementPermissions.AddChild(HonouredPermissions.Placements.Delete, L("Permission:Placement.Delete"));

            var DeliveryPermissions = HonouredGroup.AddPermission(HonouredPermissions.Deliveries.Default, L("Permission:Delivery"));
            DeliveryPermissions.AddChild(HonouredPermissions.Deliveries.Create, L("Permission:Delivery.Create"));
            DeliveryPermissions.AddChild(HonouredPermissions.Deliveries.Update, L("Permission:Delivery.Update"));
            DeliveryPermissions.AddChild(HonouredPermissions.Deliveries.Delete, L("Permission:Delivery.Delete"));

            var CountryPermissions = HonouredGroup.AddPermission(HonouredPermissions.Countries.Default, L("Permission:Country"));
            CountryPermissions.AddChild(HonouredPermissions.Countries.Create, L("Permission:Country.Create"));
            CountryPermissions.AddChild(HonouredPermissions.Countries.Update, L("Permission:Country.Update"));
            CountryPermissions.AddChild(HonouredPermissions.Countries.Delete, L("Permission:Country.Delete"));

            var AreaPermissions = HonouredGroup.AddPermission(HonouredPermissions.Markets.Default, L("Permission:ArtistArea"));
            AreaPermissions.AddChild(HonouredPermissions.Markets.Create, L("Permission:ArtistArea.Create"));
            AreaPermissions.AddChild(HonouredPermissions.Markets.Update, L("Permission:ArtistArea.Update"));
            AreaPermissions.AddChild(HonouredPermissions.Markets.Delete, L("Permission:ArtistArea.Delete"));

            var DimensionPermissions = HonouredGroup.AddPermission(HonouredPermissions.Dimensions.Default, L("Permission:Dimension"));
            DimensionPermissions.AddChild(HonouredPermissions.Dimensions.Create, L("Permission:Dimension.Create"));
            DimensionPermissions.AddChild(HonouredPermissions.Dimensions.Update, L("Permission:Dimension.Update"));
            DimensionPermissions.AddChild(HonouredPermissions.Dimensions.Delete, L("Permission:Dimension.Delete"));
        
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<HonouredResource>(name);
        }
    }
}
