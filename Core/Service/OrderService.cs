using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.OrderModule;
using DomainLayer.Models.ProductModule;
using Service.Specifications.OrderModuleSpecifications;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDtos;
using Shared.DataTransferObject.OrderModuleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OrderService(IMapper _mapper , IBasketRepository _basketRepository , IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto OrderDto, string Email)
        {
            //Map Address to OrderAddress
            var OrderAddress = _mapper.Map<AddressDto,OrderAddress>(OrderDto.shipToAddress);

            //Get Basket
            var Basket = await _basketRepository.GetBasketAsync(OrderDto.BasketId) 
                             ?? throw new BasketNotFoundException(OrderDto.BasketId);

            //Get OrderItem List
            List<OrderItem> OrderItems = [];
            var ProductRepo = _unitOfWork.GetRepository<Product,int>();

            foreach (var Item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(Item.Id)
                            ?? throw new ProductNotFoundException(Item.Id);

                OrderItems.Add(CreateOrderItem(Item, Product));
            }

            //Get DeliveryMethod
            var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(OrderDto.DeliveryMethodId)
                                  ?? throw new DeliveryNotFoundException(OrderDto.DeliveryMethodId);

            //Calculate Subtotal
            var Subtotal = OrderItems.Sum(item => item.Price * item.Quantity);

            var Order = new Order(Email, OrderAddress, DeliveryMethod, OrderItems, Subtotal);

            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(Order);

            await _unitOfWork.SaveChangesAsync();


            return _mapper.Map<Order, OrderToReturnDto>(Order);
        }

        private static OrderItem CreateOrderItem(BasketItem Item, Product Product)
        {
            return new OrderItem()
            {
                Product = new ProductItemOrdered { PictureUrl = Product.PictureUrl, ProductId = Product.Id, ProductName = Product.Name },
                Price = Product.Price,
                Quantity = Item.Quantity
            };
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethod>, IEnumerable<DeliveryMethodDto>>(DeliveryMethods);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string Email)
        {
            var Spec = new OrderSpecifications(Email);
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(Spec);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderToReturnDto>>(Orders);
        }

        public async Task<OrderToReturnDto> GetOrdersAsync(Guid Id)
        {
            var Spec = new OrderSpecifications(Id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(Spec);
            return _mapper.Map<Order, OrderToReturnDto>(Order);
        }
    }
}
