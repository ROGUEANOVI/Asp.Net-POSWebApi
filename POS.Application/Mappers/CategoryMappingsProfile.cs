using AutoMapper;
using POS.Application.DTOs.Request;
using POS.Application.DTOs.Response;
using POS.Domain.Entities;
using POS.Infrastructure.Commons.Bases.Response;
using POS.Utilities.Static;

namespace POS.Application.Mappers
{
    public class CategoryMappingsProfile : Profile
    {
        public CategoryMappingsProfile() 
        {
            CreateMap<Category, CategoryResponseDTO>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
                .ForMember(x => x.StateCategory,
                           x => x.MapFrom(y => y.State.Equals((int)StateTypes.Active) ? "Activo" : "Inactivo"))
                .ReverseMap();

            CreateMap<BaseEntityResponse<Category>, BaseEntityResponse<CategoryResponseDTO>>()
                .ReverseMap();

            CreateMap<CategoryRequestDTO, Category>()
                .ReverseMap();

            CreateMap<Category, CategorySelectResponseDTO>()
                .ForMember(x => x.CategoryId, x => x.MapFrom(y => y.Id))
                .ReverseMap();
        }
    }
}
