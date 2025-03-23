using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectPRN.Models;

namespace ProjectPRN.Business
{
    public class OrdersDetailBusiness
    {
        private readonly MilkTeaContext _context;

        public OrdersDetailBusiness()
        {
            _context = MilkTeaContext.Ins;
        }

        public List<OrdersDetail> GetOrdersDetailsById(int orderId)
        {
            try
            {
                return _context.OrdersDetails
                    .Include(od => od.Product) // Đảm bảo load Product để lấy ProductName
                    .Include(od => od.Order)
                    .Where(od => od.OrderId == orderId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy chi tiết đơn hàng: {ex.Message}", ex);
            }
        }

        public void InsertOrderDetail(OrdersDetail orderDetail)
        {
            try
            {
                _context.OrdersDetails.Add(orderDetail);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm chi tiết đơn hàng: {ex.Message}", ex);
            }
        }
    }
}