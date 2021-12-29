using AutoMapper;
using Honoured.ArtDisciplines;
using Honoured.Artists;
using Honoured.ArtistSubscriptions;
using Honoured.ArtLovers;
using Honoured.ArtWorks;
using Honoured.Countries;
using Honoured.Dimensions;
using Honoured.DTOs;
using Honoured.Markets;
using Honoured.Models;
using Honoured.Tags;
using System.Linq;
using Volo.Abp.AutoMapper;

namespace Honoured
{
    public class HonouredApplicationAutoMapperProfile : Profile
    {
        public HonouredApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
              * Alternatively, you can split your mapping configurations
              * into multiple profile classes for a better organization. */

            #region Artdiscipline

            CreateMap<ArtDiscipline, ArtDisciplineDTO>()
                .ReverseMap();
            #endregion Artdiscipline


            #region AddressAndContactPoints
            CreateMap<Address, AddressDTO>();
            CreateMap<ContactPoint, ContactPointDTO>();
            #endregion AddressAndContactPoints

            #region Artist
            CreateMap<ArtistDto, ArtistPersonalInfo>()
                //.ForMember(d=>d.DOB, a=>a.MapFrom(p=>p.DOB))
                //.ForMember(d => d.First, a => a.MapFrom(p => p.First))
                //.ForMember(d => d.Middle, a => a.MapFrom(p => p.Middle))
                //.ForMember(d => d.Last, a => a.MapFrom(p => p.Last))
                //.Ignore(d => d.ContactPoints)
                .Ignore(d => d.Address)
                //.Ignore(d => d.Addresses)
                .Ignore(d => d.BillingAddress)
                .Ignore(d => d.ContactPoints)
                .Ignore(d => d.DeliveryPoints);

            CreateMap<Artist, ArtistDto>()
                .ForMember(d => d.ArtistId, a => a.MapFrom(p => p.Id))
                .ForMember(d => d.DOB, a => a.MapFrom(p => p.PersonalDetails.DOB))
                .ForMember(d => d.First, a => a.MapFrom(p => p.PersonalDetails.First))
                .ForMember(d => d.Middle, a => a.MapFrom(p => p.PersonalDetails.Middle))
                .ForMember(d => d.Last, a => a.MapFrom(p => p.PersonalDetails.Last))
                .ForMember(d => d.Email, a => a.MapFrom(p => p.PersonalDetails.Email));

            CreateMap<ArtistDto, Artist>()
                .ForMember(d => d.PersonalDetails, a => a.MapFrom(src => src))
                .Ignore(d => d.Id);

            CreateMap<ArtistDto, UpdateArtistDto>();
            CreateMap<CreateArtistDto, ArtistPersonalInfo>()
                //.ForMember(d=>d.DOB, a=>a.MapFrom(p=>p.DOB))
                //.ForMember(d => d.First, a => a.MapFrom(p => p.First))
                //.ForMember(d => d.Middle, a => a.MapFrom(p => p.Middle))
                //.ForMember(d => d.Last, a => a.MapFrom(p => p.Last))
                //.Ignore(d => d.ContactPoints)
                .Ignore(d => d.Address)
                //.Ignore(d => d.Addresses)
                .Ignore(d => d.BillingAddress)
                .Ignore(d => d.ContactPoints)
                .Ignore(d => d.DeliveryPoints);

            CreateMap<CreateArtistDto, Artist>()
                .ForMember(d => d.PersonalDetails, a => a.MapFrom(src => src))
                .Ignore(d => d.Id);

            #endregion Artist


            #region Artlover
            CreateMap<ArtLoverDto, ArtLoverPersonalInfo>()
                .Ignore(d => d.Address)
                .Ignore(d => d.ContactPoints)
                .Ignore(d => d.Addresses)
                .Ignore(d => d.BillingAddress)
                .Ignore(d => d.ContactPoints)
                .Ignore(d => d.DeliveryPoints)
                .ReverseMap();

            CreateMap<ArtLoverDto, ArtLover>()
                .ForMember(d => d.Profile, a => a.MapFrom(src => src))
                .Ignore(d => d.Categories)
                .Ignore(d => d.Id);
            CreateMap<ArtLover, ArtLoverDto>()
                .ForMember(d => d.ArtLoverId, a => a.MapFrom(p => p.Profile.Id))
                .ForMember(d => d.DOB, a => a.MapFrom(p => p.Profile.DOB))
                .ForMember(d => d.First, a => a.MapFrom(p => p.Profile.First))
                .ForMember(d => d.Middle, a => a.MapFrom(p => p.Profile.Middle))
                .ForMember(d => d.Last, a => a.MapFrom(p => p.Profile.Last))
                .ForMember(d => d.Email, a => a.MapFrom(p => p.Profile.Email))
                ;
            CreateMap<CreateArtLoverDto, ArtLoverPersonalInfo>()
                .Ignore(d => d.Address)
                .Ignore(d => d
                .ContactPoints)
                .Ignore(d => d.Addresses)
                .Ignore(d => d.BillingAddress)
                .Ignore(d => d.ContactPoints)
                .Ignore(d => d.DeliveryPoints)
                .ReverseMap();
            CreateMap<CreateArtLoverDto, ArtLover>()
                .ForMember(d => d.Profile, a => a.MapFrom(src => src))
                .Ignore(d => d.Categories)
                .Ignore(d => d.Id);
            CreateMap<UpdateArtLoverDto, ArtLover>()
                .ForMember(d => d.Profile, a => a.MapFrom(src => src))
                .Ignore(d => d.Categories)
                .Ignore(d => d.Id);
            CreateMap<CreateArtLoverDto, ArtLoverDto>();

            #endregion Artlover

            #region ArtWork
            CreateMap<ArtWork, ArtWorkDto>()
                .ForMember(d => d.Dimensions, opt => opt.MapFrom(o => $"{o.Width}x{o.Height}x{o.Depth}"));
            CreateMap<UpdateArtWorkDto, ArtWork>();
            CreateMap<ArtWorkDto, UpdateArtWorkDto>();
            CreateMap<CreateArtWorkDto, ArtWork>();
            CreateMap<Artist, UpdateArtistProfileDto>()
                .ForMember(a => a.ArtistId, opt => opt.MapFrom(src => src.Id))
                .ForMember(a => a.Name, opt => opt.MapFrom(src => src.GetFullName()));

            #endregion ArtWork


            #region Dimensions
            CreateMap<Dimension, DimensionDto>().ReverseMap();
            CreateMap<Dimension, CreateDimensionDto>().ReverseMap();
            CreateMap<Dimension, UpdateDimensionDto>().ReverseMap();
            #endregion Dimensions


            #region Countries
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();
            #endregion Countries


            #region Markets
            CreateMap<Market, MarketDto>().ReverseMap();
            CreateMap<Market, CreateMarketDto>().ReverseMap();
            CreateMap<Market, UpdateMarketDto>().ReverseMap();
            #endregion Markets


            #region Tags
            CreateMap<Tag, TagDto>().ReverseMap();
            #endregion Tags


            #region Subscribers
            CreateMap<ArtistDto, SubscriberDto>()
                .ForMember(m => m.Street1, opt => opt.MapFrom(a => a.Addresses.Any() ?  a.Addresses.First().Street1 :""))
                .ForMember(m => m.Street2, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().Street2 :""))
                .ForMember(m => m.City, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().City :""))
                .ForMember(m => m.Province, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().Province :""))
                .ForMember(m => m.PostalCode, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().PostalCode :""))
                .ForMember(m => m.Country, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().Country :""))
                .ReverseMap();

            CreateMap<ArtLoverDto, SubscriberDto>()
                .ForMember(m => m.Street1, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().Street1 : ""))
                .ForMember(m => m.Street2, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().Street2 : ""))
                .ForMember(m => m.City, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().City : ""))
                .ForMember(m => m.Province, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().Province : ""))
                .ForMember(m => m.PostalCode, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().PostalCode : ""))
                .ForMember(m => m.Country, opt => opt.MapFrom(a => a.Addresses.Any() ? a.Addresses.First().Country : ""));


            CreateMap<CreateArtistDto, SubscriberDto>().ReverseMap();
            CreateMap<CreateArtLoverDto, SubscriberDto>().ReverseMap();
            CreateMap<CreateArtistDto, Address>();
            #endregion Subscribers


            #region ArtistSubscriptions
            CreateMap<ArtistSubscription, ArtistSubscriptionDto>()
                .ForMember(m=> m.TierId, opt=>opt.MapFrom(a=>a.Tier.Id));
            CreateMap<ArtistSubscriptionDto, ArtistSubscription>()
                .Ignore(a=>a.Tier);
            CreateMap<SubscriptionTier, SubscriptionTierDto>();
            #endregion ArtistSubscriptions
        }
    }
}
