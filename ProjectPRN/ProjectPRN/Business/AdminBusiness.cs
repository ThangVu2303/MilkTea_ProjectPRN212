using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectPRN.Models;

namespace ProjectPRN.Business
{
    public class AdminBusiness
    {
        private readonly MilkTeaContext _context;

        public AdminBusiness()
        {
            _context = MilkTeaContext.Ins;
        }

        public List<Admin> GetAdmins()
        {
            try
            {
                return _context.Admins
                    .Include(a => a.Account)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách quản lý: {ex.Message}", ex);
            }
        }

        public Admin GetAdminById(int adminId)
        {
            try
            {
                return _context.Admins
                    .Include(a => a.Account)
                    .FirstOrDefault(a => a.AdminId == adminId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy quản lý theo ID: {ex.Message}", ex);
            }
        }

        public Admin GetAdminByAccountId(int accountId)
        {
            try
            {
                return _context.Admins
                    .Include(a => a.Account)
                    .FirstOrDefault(a => a.AccountId == accountId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy quản lý theo AccountId: {ex.Message}", ex);
            }
        }

        public void InsertAdmin(Admin admin)
        {
            try
            {
                _context.Admins.Add(admin);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm quản lý: {ex.Message}", ex);
            }
        }

        public void UpdateAdmin(Admin admin)
        {
            try
            {
                var existingAdmin = _context.Admins
                    .Include(a => a.Account)
                    .FirstOrDefault(a => a.AdminId == admin.AdminId);

                if (existingAdmin != null)
                {
                    existingAdmin.Gender = admin.Gender;
                    existingAdmin.Dob = admin.Dob;
                    existingAdmin.Account.Email = admin.Account.Email;
                    existingAdmin.AccountId = admin.AccountId;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật quản lý: {ex.Message}", ex);
            }
        }

        public void DeleteAdmin(Admin admin)
        {
            try
            {
                var adminToDelete = _context.Admins
                    .FirstOrDefault(a => a.AdminId == admin.AdminId);

                if (adminToDelete != null)
                {
                    _context.Admins.Remove(adminToDelete);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa quản lý: {ex.Message}", ex);
            }
        }
    }
}