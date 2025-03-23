using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectPRN.Models;
using System.Windows;

namespace ProjectPRN.Business
{
    public class ProductBusiness  // Changed to public class
    {
        private readonly MilkTeaContext _context;

        public ProductBusiness()
        {
            _context = MilkTeaContext.Ins;
        }

        public List<Product> GetProducts()
        {
            try
            {
                return _context.Products
                    .Include(p => p.Category)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                var existingProduct = _context.Products
                    .FirstOrDefault(p => p.ProductId == product.ProductId);

                if (existingProduct != null)
                {
                    existingProduct.ProductName = product.ProductName;
                    existingProduct.Quantity = 10; // Hardcoded as per original
                    existingProduct.Price = product.Price;
                    existingProduct.Origin = product.Origin;
                    existingProduct.CategoryId = product.CategoryId;
                    existingProduct.Image = product.Image;

                    _context.SaveChanges();
                    MessageBox.Show("Data has been updated!");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DeleteProduct(Product product)
        {
            try
            {
                var productToDelete = _context.Products
                    .FirstOrDefault(p => p.ProductId == product.ProductId);

                if (productToDelete != null)
                {
                    _context.Products.Remove(productToDelete);
                    _context.SaveChanges();
                    MessageBox.Show("The data has been deleted");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Product> GetProductsWithCatID(Product product)
        {
            try
            {
                return _context.Products
                    .Include(p => p.Category)
                    .Where(p => p.CategoryId == product.CategoryId)
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal void InsertProduct(Product product)
        {
             
            try
            {
                if (_context.Products.Any(p => p.ProductId == product.ProductId))
                {
                    throw new Exception("Product ID đã tồn tại.");
                }

                if (!_context.Categories.Any(c => c.CategoryId == product.CategoryId))
                {
                    throw new Exception("Category ID không hợp lệ.");
                }

                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể thêm sản phẩm: {ex.Message}", ex);
            }
        }
    }
    }
