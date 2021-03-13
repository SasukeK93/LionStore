using AutoMapper;
using LionStore.Api.Resources.Auth;
using LionStore.Api.Resources.Order;
using LionStore.Api.Resources.Product;
using LionStore.Core.Models;
using LionStore.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LionStore.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Product, ProductResource>();
            CreateMap<Order, OrderResource>();

            // Resource to Domain
            CreateMap<ProductResource, Product>();
            CreateMap<SaveProductResource, Product>();

            CreateMap<BuyProductResource, Order>();

            CreateMap<OrderResource, Order>();
            CreateMap<SaveOrderResource, Order>();

            CreateMap<LionUserLoginResource, LionUser>();
        }
    }
}
