using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectPRN.Models;

namespace ProjectPRN.Business
{
    public class RoleBusiness  // Changed to public class
    {
        private readonly MilkTeaContext _context;

        public RoleBusiness()
        {
            _context = MilkTeaContext.Ins;
        }

        public List<Role> GetRoles()
        {
            try
            {
                return _context.Roles
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}