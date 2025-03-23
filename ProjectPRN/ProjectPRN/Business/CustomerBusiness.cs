using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectPRN.Models;

namespace ProjectPRN.Business
{
    public class CustomerBusiness
    {
        private readonly MilkTeaContext _context;

        public CustomerBusiness()
        {
            _context = MilkTeaContext.Ins;
        }

        // Get all customers
        public List<Customer> GetCustomers()
        {
            try
            {
                return _context.Customers
                    .Include(c => c.Account)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Get customer by ID
        public Customer GetCustomerById(int customerId)
        {
            try
            {
                return _context.Customers
                    .Include(c => c.Account)
                    .FirstOrDefault(c => c.CustomerId == customerId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Get customer by Account ID
        public Customer GetCustomerByAccountId(int accountId)
        {
            try
            {
                return _context.Customers
                    .Include(c => c.Account)
                    .FirstOrDefault(c => c.AccountId == accountId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Insert new customer
        public void InsertCustomer(Customer customer)
        {
            try
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Update customer
        public void UpdateCustomer(Customer customer)
        {
            try
            {
                var existingCustomer = _context.Customers
                    .FirstOrDefault(c => c.CustomerId == customer.CustomerId);

                if (existingCustomer != null)
                {
                    existingCustomer.Point = customer.Point;
                    existingCustomer.AccountId = customer.AccountId;
                    // Note: Account properties would need to be updated separately if changed

                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Delete customer
        public void DeleteCustomer(Customer customer)
        {
            try
            {
                var customerToDelete = _context.Customers
                    .Include(c => c.Orders)
                    .FirstOrDefault(c => c.CustomerId == customer.CustomerId);

                if (customerToDelete != null)
                {
                    if (customerToDelete.Orders.Any())
                        throw new InvalidOperationException("Cannot delete customer with existing orders");

                    _context.Customers.Remove(customerToDelete);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Get total points for a customer
        public int GetCustomerPoints(int customerId)
        {
            try
            {
                return _context.Customers
                    .Where(c => c.CustomerId == customerId)
                    .Select(c => c.Point ?? 0)
                    .FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}