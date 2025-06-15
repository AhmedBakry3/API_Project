using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModule;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketModuleDtos;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = DomainLayer.Models.ProductModule.Product;
namespace Service
{
    public class PaymentService(IConfiguration _configuration ,
        IBasketRepository _basketRepository ,
        IUnitOfWork _unitOfWork ,
        IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            //Configure Stripe : install package Stripe.net
            StripeConfiguration.ApiKey = _configuration["StripeSetting:SecretKey"];

            //Get Basket By BasketId
            var Basket = await _basketRepository.GetBasketAsync(basketId) ?? throw new BasketNotFoundException(basketId);

            //Get Amount = Get Product + DeliveryMethod Cost
            var ProductRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var Item in Basket.Items)
            {
                var Product = await ProductRepo.GetByIdAsync(Item.Id) ?? throw new ProductNotFoundException(Item.Id);
                Item.Price = Product.Price;
            }
             ArgumentNullException.ThrowIfNull(Basket.deliveryMethodId);
             var DeliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetByIdAsync(Basket.deliveryMethodId.Value)
                    ?? throw new DeliveryNotFoundException(Basket.deliveryMethodId.Value);

             Basket.shippingPrice = DeliveryMethod.Price;

             var BasketAmount = (long) (Basket.Items.Sum(P => P.Price * P.Quantity) + DeliveryMethod.Price) * 100;

            var PaymentService = new PaymentIntentService();
            //Create PaymentIntent [Create - Update]
            if (Basket.paymentIntentId is null ) //Create
            {
                var Options = new PaymentIntentCreateOptions
                {
                    Amount = BasketAmount,
                    Currency = "USD",
                    PaymentMethodTypes = ["card"],

                };
                var PaymentIntent = await PaymentService.CreateAsync(Options);
                Basket.paymentIntentId = PaymentIntent.Id;
                Basket.clientSecret = PaymentIntent.ClientSecret;
            }
            else //Update
            {
                var Options = new PaymentIntentUpdateOptions { Amount = BasketAmount };
                await PaymentService.UpdateAsync(Basket.paymentIntentId, Options);

            }
            await _basketRepository.CreateOrUpdateBasketAsync(Basket);
            return _mapper.Map<BasketDto>(Basket);
        }
    }
}
