using AutoMapper;
using LivroMente.Domain.Models.BookModel;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Domain.ViewModel;

namespace LivroMente.Infrastructure.Mapping
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoryBook,CategoryBookViewModel>().ReverseMap();
        }
        
    }
}