using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.API.Models;
using OrderManagementSystem.DataAccess.Repository;
using OrderManagementSystem.Entity;

namespace OrderManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<Product> _productRepository;

        public OrdersController(IGenericRepository<Order> orderRepository, IGenericRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderDto orderDto)
        {
            try
            {
                if (orderDto == null)
                {
                    return BadRequest("Lütfen sipariş bilgilerini giriniz");
                }

                var product = _productRepository.GetById(orderDto.ProductId);
                if (product == null || product.Stock == 0)
                {
                    return BadRequest("Bu ürün bulunamadı");
                }

                var orderEntity = new Order
                {
                    ProductId = orderDto.ProductId,
                    UserId = orderDto.UserId
                };

                _orderRepository.Insert(orderEntity);

                return Ok("Sipariş alındı");
            }
            catch (Exception ex)
            {
                throw new Exception($"Beklenmeyen hata! Hata Mesajı: {ex.Message}");
            }
        }

        [HttpGet]
        public IActionResult GetByUser(int userId)
        {
            try
            {
                if (userId == 0)
                {
                    return BadRequest("Lütfen kullanıcı bilgisi giriniz");
                }

                var userOrderListEntity = _orderRepository.GetByFilter(x => x.UserId == userId);

                var userOrderListDto = userOrderListEntity.Select(x => new OrderDto
                {
                    ProductId = x.ProductId,
                    UserId = x.UserId
                });

                return Ok(userOrderListDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Beklenmeyen hata! Hata Mesajı: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderDetails(int id)
        {
            try
            {
                var orderEntity = _orderRepository.GetById(id);
                if (orderEntity is null)
                {
                    return NotFound("Sipariş bulunamadı");
                }

                var orderDto = new OrderDto
                {
                    ProductId = orderEntity.ProductId,
                    UserId = orderEntity.UserId
                };

                return Ok(orderDto);
            }
            catch (Exception ex)
            {
                throw new Exception($"Beklenmeyen hata! Hata Mesajı: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var orderEntity = _orderRepository.GetById(id);
                if (orderEntity is null)
                {
                    return NotFound("Sipariş bulunamadı");
                }

                _orderRepository.Delete(orderEntity);

                return Ok("Sipariş silindi");
            }
            catch (Exception ex)
            {
                throw new Exception($"Beklenmeyen hata! Hata Mesajı: {ex.Message}");
            }
        }
    }
}
