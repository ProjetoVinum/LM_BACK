using AutoMapper;
using LivroMente.API.Controllers;
using LivroMente.Domain.Models.CategoryBookModel;
using LivroMente.Infrastructure.Mapping;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace LivroMente.Test.Controllers
{
    public class CategoryBookControllerTest
    {
        private readonly Mock<ICategoryBookRepository> _categoryMock;
        private Mapper _mapper;
        private readonly CategoryBookController _controller;

        public CategoryBookControllerTest()
        {
             _categoryMock = new Mock<ICategoryBookRepository>();
           // _mapper = mapper;
           _mapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfiles>()));
            _controller = new CategoryBookController(_categoryMock.Object,_mapper);
        }

        [Fact]
        public async Task GetById_WhenCalled_ReturnOkResult(){
            //Arrange
            var id = Guid.NewGuid();

            //Act
            var result =  _controller.GetById(id);

            //Asset
            Assert.IsType<OkObjectResult>(result);
        }
    }
}