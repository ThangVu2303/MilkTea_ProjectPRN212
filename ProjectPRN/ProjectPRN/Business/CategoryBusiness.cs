using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProjectPRN.Models;

namespace ProjectPRN.Business
{
    public class CategoryBusiness
    {
        private readonly MilkTeaContext _context;

        public CategoryBusiness()
        {
            _context = MilkTeaContext.Ins; // Using the singleton instance from your context
        }

        public List<Category> GetCategories()
        {
            try
            {
                return _context.Categories
                    .Select(c => new Category
                    {
                        CategoryId = c.CategoryId,    // Matching property name from context
                        CategoryName = c.CategoryName
                    })
                    .ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int InsertCategory(Category category)
        {
            try
            {
                var newCategory = new Category
                {
                    CategoryName = category.CategoryName
                    // CategoryId is set to ValueGeneratedNever in context, so we don't set it here
                };

                _context.Categories.Add(newCategory);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int UpdateCategory(Category category)
        {
            try
            {
                var categoryToUpdate = _context.Categories
                    .FirstOrDefault(c => c.CategoryId == category.CategoryId);

                if (categoryToUpdate == null)
                    return 0;

                categoryToUpdate.CategoryName = category.CategoryName;
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int DeleteCategory(Category category)
        {
            try
            {
                var categoryToDelete = _context.Categories
                    .FirstOrDefault(c => c.CategoryId == category.CategoryId);

                if (categoryToDelete == null)
                    return 0;

                _context.Categories.Remove(categoryToDelete);
                return _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}