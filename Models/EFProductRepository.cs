﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
    public class EFProductRepository:IProductRepository
    {
        private ApplicationDbContext context;
        public EFProductRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Product> Products => context.Products;
        public IEnumerable<StackInfo> StackInfos => context.StackInfos;
        public void SaveProduct(Product product)
        {
            if (product.ProductId == 0)
            {
                context.Products.Add(product);
            }
            else
            {
                Product dbEntry = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if(dbEntry != null)
                {
                    dbEntry.Name = product.Name;
                    dbEntry.Description = product.Description;
                    dbEntry.Price = product.Price;
                    dbEntry.Category = product.Category;
                }
            }
            context.SaveChanges();

        }
        public Product DeleteProduct(int productId)
        {
            
                Product dbEntry = context.Products.FirstOrDefault(p => p.ProductId == productId);
                if (dbEntry != null)
                {
                    context.Products.Remove(dbEntry);
                    context.SaveChanges();
                }

            return dbEntry;
        }

    }
}
