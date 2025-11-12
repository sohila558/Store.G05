using AutoMapper;
using Store.G02.Shared.Dtos.Orders;
using Store.G05.Domain.Contracts;
using Store.G05.Domain.Entities.Orders;
using Store.G05.Domain.Entities.Products;
using Store.G05.Domain.Exceptions.BadRequest;
using Store.G05.Domain.Exceptions.NotFound;
using Store.G05.Services.Abstractions.Orders;
using Store.G05.Services.Specifications.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G05.Services.Orders
{
    public class OrderServices(IUnitOfWork _unitOfWork, IMapper mapper, IBasketRepositry _basketRepositry) : IOrderServices
    {
        public async Task<OrderResponse?> CreateOrderAsync(OrderRequest request, string userEmail)
        {
            // 1. Get Order Address
            var orderAddress = mapper.Map<OrderAddress>(request.ShipToAddress);

            // 2. Get Delivery Method By Id
            var deliveryMethod = await _unitOfWork.GetRepositry<int, DeliveryMethod>().GetAsync(request.DeliveryMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFound(request.DeliveryMethodId);

            // 3. Get Order Items
            var basket = await _basketRepositry.GetBasketAsync(request.BasketId);
            if (basket is null) throw new BasketNotFoundException(request.BasketId);

            // 3.1. Convert Every BasketItem To OrderItem
            var orderItems = new List<OrderItem>();

            foreach (var item in basket.Items)
            {
                // Check Price
                // Get Product From DB
                var product = await _unitOfWork.GetRepositry<int, Product>().GetAsync(item.Id);
                if (product is null) throw new ProductNotFoundExceptions(item.Id);

                if (product.Price != item.Price) item.Price = product.Price;

                var productInOrderItem = new ProductInOrderItem(item.Id, item.ProductName, item.PictureUrl);
                var orderItem = new OrderItem(productInOrderItem, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            // 4. Calculate SubTotal
            var subTotal = orderItems.Sum(OI => OI.Price * OI.Quantity);

            // Create Order
            var order = new Order(userEmail, orderAddress, deliveryMethod, orderItems, subTotal);

            // Create Order In DB
            _unitOfWork.GetRepositry<Guid, Order>().AddAsync(order);
            var count = await _unitOfWork.SaveChangesAsync();
            if (count <= 0) throw new CraeteOrderBadRequestException();

            return mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<DeliveryMethodResponse>> GetAllDeliveryMethodAsync()
        {
            var deliveryMethod = await _unitOfWork.GetRepositry<int, DeliveryMethod>().GetAllAsync();
            return mapper.Map<IEnumerable<DeliveryMethodResponse>>(deliveryMethod);
        }

        public async Task<OrderResponse?> GetOrderByIdForSpecificUserAsync(Guid id, string userEmail)
        {
            var spec = new OrderSpecifications(id, userEmail);
            var order = await _unitOfWork.GetRepositry<Guid, Order>().GetByIdAsync(spec);
            return mapper.Map<OrderResponse>(order);
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersForSpecificUserAsync(string userEmail)
        {
            var spec = new OrderSpecifications(userEmail);
            var orders = await _unitOfWork.GetRepositry<Guid, Order>().GetAllAsync(spec);
            return mapper.Map<IEnumerable<OrderResponse>>(orders);
        }
    }
}
