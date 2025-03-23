using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectPRN.Models;

namespace ProjectPRN.Business
{
    public class StaffBusiness
    {
        private readonly MilkTeaContext _context;

        public StaffBusiness()
        {
            _context = MilkTeaContext.Ins;
        }

        public List<Staff> GetStaffById(string staffId)
        {
            try
            {
                return _context.Staff
                    .Include(s => s.Account)
                    .Where(s => s.StaffId.ToString() == staffId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin nhân viên: {ex.Message}", ex);
            }
        }

        public void DeleteStaff(int accountId)
        {
            try
            {
                var staff = _context.Staff.FirstOrDefault(s => s.AccountId == accountId);
                if (staff != null)
                {
                    _context.Staff.Remove(staff);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa nhân viên: {ex.Message}", ex);
            }
        }

        public string GetManagerId(string staffId)
        {
            try
            {
                // Model không có ManagerId, trả về StaffId làm placeholder
                return _context.Staff
                    .Where(s => s.StaffId.ToString() == staffId)
                    .Select(s => s.StaffId.ToString())
                    .FirstOrDefault() ?? "";
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy ManagerId: {ex.Message}", ex);
            }
        }

        public void InsertStaff(Staff staff)
        {
            try
            {
                _context.Staff.Add(staff);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm nhân viên: {ex.Message}", ex);
            }
        }

        public List<int> GetStaffIds()
        {
            try
            {
                return _context.Staff
                    .Select(s => s.StaffId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách StaffId: {ex.Message}", ex);
            }
        }

        public string GetStaffIdByAccountId(string accountId)
        {
            try
            {
                return _context.Staff
                    .Where(s => s.AccountId.ToString() == accountId)
                    .Select(s => s.StaffId.ToString())
                    .FirstOrDefault() ?? "";
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy StaffId theo AccountId: {ex.Message}", ex);
            }
        }

        public void UpdateStaff(Staff staff)
        {
            try
            {
                var existingStaff = _context.Staff
                    .Include(s => s.Account)
                    .FirstOrDefault(s => s.StaffId == staff.StaffId);

                if (existingStaff != null)
                {
                    existingStaff.HireDate = staff.HireDate;
                    existingStaff.Gender = staff.Gender;
                    existingStaff.DateOfBirth = staff.DateOfBirth;
                    existingStaff.Email = staff.Email;
                    existingStaff.AccountId = staff.AccountId;
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật nhân viên: {ex.Message}", ex);
            }
        }

        public bool GetGenderById(string staffId)
        {
            try
            {
                return _context.Staff
                    .Where(s => s.StaffId.ToString() == staffId)
                    .Select(s => s.Gender == "Male")
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy giới tính nhân viên: {ex.Message}", ex);
            }
        }

        public Staff GetStaffByAccountID(int accountId)
        {
            try
            {
                return _context.Staff
                    .Include(s => s.Account)
                    .FirstOrDefault(s => s.AccountId == accountId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy nhân viên theo AccountId: {ex.Message}", ex);
            }
        }
    }
}