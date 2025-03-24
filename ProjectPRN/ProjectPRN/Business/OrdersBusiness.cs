using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectPRN.Models;

namespace ProjectPRN.Business
{
    public class OrdersBusiness
    {
        private readonly MilkTeaContext _context;

        public OrdersBusiness()
        {
            _context = MilkTeaContext.Ins;
        }

        public List<Order> GetOrders()
        {
            try
            {
                return _context.Orders
                    .Include(o => o.Staff)
                    .Include(o => o.Customer)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách đơn hàng: {ex.Message}", ex);
            }
        }

        public List<Order> GetOrdersByEmployeeIdAndDate(string staffId, string dateFrom, string dateTo)
        {
            try
            {
                DateOnly fromDate = DateOnly.Parse(dateFrom);
                DateOnly toDate = DateOnly.Parse(dateTo);
                var query = _context.Orders
                    .Include(o => o.Staff)
                    .Include(o => o.Customer)
                    .Where(o => o.DateCreate >= fromDate && o.DateCreate <= toDate);

                if (!string.IsNullOrEmpty(staffId) && int.TryParse(staffId, out int id))
                {
                    query = query.Where(o => o.StaffId == id);
                }

                return query.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lọc đơn hàng: {ex.Message}", ex);
            }
        }

        public List<Order> GetOrdersByDate(string dateFrom, string dateTo)
        {
            try
            {
                DateOnly fromDate = DateOnly.Parse(dateFrom);
                DateOnly toDate = DateOnly.Parse(dateTo);

                return _context.Orders
                    .Include(o => o.Staff)
                    .Include(o => o.Customer)
                    .Where(o => o.DateCreate >= fromDate && o.DateCreate <= toDate)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy đơn hàng theo ngày: {ex.Message}", ex);
            }
        }

        public Order Get1Orders()
        {
            try
            {
                return _context.Orders
                    .Include(o => o.Staff)
                    .Include(o => o.Customer)
                    .OrderByDescending(o => o.OrderId)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy đơn hàng mới nhất: {ex.Message}", ex);
            }
        }

        public void InsertOrder(Order order)
        {
            try
            {
                _context.Orders.Add(order);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm đơn hàng: {ex.Message}", ex);
            }
        }

        public decimal GetTotalRevByDate(string dateFrom, string dateTo)
        {
            try
            {
                DateOnly fromDate = DateOnly.Parse(dateFrom);
                DateOnly toDate = DateOnly.Parse(dateTo);

                return _context.Orders
                    .Where(o => o.DateCreate >= fromDate && o.DateCreate <= toDate)
                    .Sum(o => o.Total);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tính tổng doanh thu theo ngày: {ex.Message}", ex);
            }
        }

        public decimal GetTotalRev()
        {
            try
            {
                return _context.Orders.Sum(o => o.Total);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tính tổng doanh thu: {ex.Message}", ex);
            }
        }
    }
}