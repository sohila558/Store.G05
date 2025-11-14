using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store.G02.Shared.Dtos.Baskets;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities.Orders;
using Store.G05.Domain.Entities.Products;
using Store.G05.Domain.Exceptions.NotFoundExceptions;
using Store.G05.Services.Abstractions.Payments;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store.G05.Domain.Entities.Products.Product;

namespace Store.G05.Services.Payments
{
    public class PaymentServices(IBasketRepositry _basketRepositry, IUnitOfWork _unitOfWork, IConfiguration _configuration, IMapper _mapper) : IPaymentServices
    {
        public async Task<BasketDto> CreatePaymentIntentAsync(string basketId)
        {
            // Calculate Amount = SubTotal + Delivery Method Cost

            // Get Basket by Id
            var basket = await _basketRepositry.GetBasketAsync(basketId);
            if (basket is null) throw new BasketNotFoundException(basketId);

            // Check Product and it's Price
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.GetRepositry<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundExceptions(item.Id);

                item.Price = product.Price;
            }

            // Calculate SubTotal
            var subTotal = basket.Items.Sum(I => I.Price * I.Quantity);

            // Get Delivery Method
            if (!basket.DeliveryMethodId.HasValue) throw new DeliveryMethodNotFound(-1);

            var deliveryMethod = await _unitOfWork.GetRepositry<int, DeliveryMethod>().GetAsync(basket.DeliveryMethodId.Value);
            if (deliveryMethod is null) throw new DeliveryMethodNotFound(basket.DeliveryMethodId.Value);

            var amount = subTotal + deliveryMethod.Price;

            basket.ShippingCost = subTotal + deliveryMethod.Price;

            // Send Amount To Stripe 

            StripeConfiguration.ApiKey = _configuration["StripeOptions:SecretKey"];

            PaymentIntentService paymentIntentService = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (basket.PaymentIntentId is null)
            {
                // Create
                var options = new PaymentIntentCreateOptions()
                {
                    Amount = (long)amount * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>() { "card" }
                };

                paymentIntent = await paymentIntentService.CreateAsync(options);
            }
            else
            {
                // Update
                var options = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)amount * 100,
                };

                paymentIntent = await paymentIntentService.UpdateAsync(basket.PaymentIntentId, options);
            }

            basket.PaymentIntentId = paymentIntent.Id;
            basket.ClientSecret = paymentIntent.ClientSecret;

            basket = await _basketRepositry.SetBasketAsync(basket, TimeSpan.FromDays(1));

            return _mapper.Map<BasketDto>(basket);
        }
    }
}
