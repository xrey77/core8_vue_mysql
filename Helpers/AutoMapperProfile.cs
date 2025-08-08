using AutoMapper;
using core8_vue_mysql.Entities;
using core8_vue_mysql.Models.dto;

namespace core8_vue_mysql.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserModel>();
            CreateMap<UserRegister, User>();
            CreateMap<UserLogin, User>();
            CreateMap<UserUpdate, User>();
            CreateMap<UserPasswordUpdate, User>();
            CreateMap<Product, ProductModel>();
            CreateMap<ProductModel, Product>();

        }
    }
    

}