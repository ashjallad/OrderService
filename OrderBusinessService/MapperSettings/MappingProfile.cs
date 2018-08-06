using AutoMapper;
using OrderBusinessEntity.Customer;
using OrderBusinessEntity.Order;
using OrderBusinessEntity.Product;
using OrderDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderBusinessService.MapperSettings
{
    class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderEntity>();
            CreateMap<OrderEntry, OrderEntryEntity>();
            CreateMap<Customer, CustomerEntity>();
            CreateMap<Product, ProductEntity>();
            CreateMap<Order, OrderEntity>().ReverseMap();
            CreateMap<OrderEntry, OrderEntryEntity>().ReverseMap();
            CreateMap<Customer, CustomerEntity>().ReverseMap();
            CreateMap<Product, ProductEntity>().ReverseMap();
        }
    }
}
