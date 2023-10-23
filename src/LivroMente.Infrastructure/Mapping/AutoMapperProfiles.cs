using AutoMapper;
using LivroMente.Domain.Models.AdressModel;
using LivroMente.Domain.Models.BookModel;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Domain.Models.Dto;
using LivroMente.Domain.Models.IdentityEntities;
using LivroMente.Domain.Models.PaymentModel;
using LivroMente.Domain.ViewModel;

namespace LivroMente.Infrastructure.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoryBook,CategoryBookViewModel>().ReverseMap();
            CreateMap<Payment,PaymentViewModel>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserLoginDto>().ReverseMap();
            CreateMap<Adress, AdressViewModel>().ReverseMap();
        }
        
    }
}