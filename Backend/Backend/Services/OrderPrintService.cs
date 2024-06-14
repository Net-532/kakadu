using Kakadu.Backend.Entities;
using Kakadu.Backend.Services;

namespace Kakadu.Backend.Services
{
    public class OrderPrintService
    {
        private readonly OrderService _orderService;
        private readonly OrderToPlainTextConverter _orderToPlainTextConverter;
        private readonly PrintService _printService;

        public OrderPrintService(OrderService orderService, OrderToPlainTextConverter orderToPlainTextConverter, PrintService printService)
        {
            _orderService = orderService;
            _orderToPlainTextConverter = orderToPlainTextConverter;
            _printService = printService;
        }

        public void Print(int orderId)
        {
            Order order = _orderService.GetById(orderId);
                string text = _orderToPlainTextConverter.Convert(order);
                _printService.Print(text);
        }
    }
}
