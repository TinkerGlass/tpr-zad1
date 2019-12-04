﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LingDatabase
{
    public static class LinqToolsStage4
    {
        public static List<Product> GetProductsWithoutCategory(this List<Product> products)
        {
            return products.Where(p => p.ProductSubcategory == null).ToList();
        }



        public static List<Product> GetProductsAsPage(this List<Product> products, int pageNumber, int productsNumber)
        {
            return products.Skip(productsNumber * (pageNumber-1)).Take(productsNumber).ToList();
        }



        public static string GetVendorProductList(this List<Product> products, List<ProductVendor> vendors)
        {
            string result = "";
            var answer = (from product in products
                          from vendor in vendors
                          where vendor.ProductID.Equals(product.ProductID)
                          select product.Name + "-" + vendor.Vendor.Name).ToList();
            foreach(Object ans in answer)
            {
                result += ans + Environment.NewLine;
            }
            return result;

        }
    }
}