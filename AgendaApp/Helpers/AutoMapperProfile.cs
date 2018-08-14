using AgendaApp.Data.Entities;
using AgendaApp.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaApp.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // ApplicationUser
            CreateMap<ApplicationUser, EditProfileVM>();

            // Agenda
            CreateMap<Agenda, AgendaVM>()
                .ForMember(dest => dest.ItemCount, opt => opt.MapFrom(source => source.Items.Count));
            CreateMap<Agenda, ArchivesVM>()
                .ForMember(dest => dest.CompletedItemsCount, opt => opt.MapFrom(source => source.Items.Where(o => o.Completed).Count()))
                .ForMember(dest => dest.TotalItemsCount, opt => opt.MapFrom(source => source.Items.Count()))
                .ForMember(dest => dest.Ratio, opt => opt.MapFrom(source =>
                    ((double)source.Items.Where(o => o.Completed).Count() / (double)source.Items.Count()) * 100));
            CreateMap<CreateAgendaVM, Agenda>();

            // Category
            CreateMap<CreateCategoryVM, Category>();

            // Item
            CreateMap<Item, ItemVM>();
            CreateMap<CreateItemVM, Item>();
        }
    }
}