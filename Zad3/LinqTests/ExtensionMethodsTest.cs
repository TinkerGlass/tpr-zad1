﻿using System;
using System.Collections.Generic;
using LingDatabase;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Data.Linq;

namespace LinqTests
{
    [TestClass]
    public class ExtensionMethodsTest
    {
        [TestMethod]
        public void ListProducts()
        {
            using (DataClasses1DataContext dataContext = new DataClasses1DataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                List<Product> answer = products.GetProductsWithoutCategory();
                Assert.AreEqual(answer[0].ProductSubcategory, null);
                Assert.AreEqual(answer.Count, 209);
            }
        }


        [TestMethod]
        public void ListProductsQuery()
        {
            using (DataClasses1DataContext dataContext = new DataClasses1DataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                List<Product> answer = products.GetProductsWithoutCategoryQuery();
                Assert.AreEqual(answer[0].ProductSubcategory, null);
                Assert.AreEqual(answer.Count, 209);
            }
        }

        [TestMethod]
        public void ListProductVendor()
        {
            using (DataClasses1DataContext dataContext = new DataClasses1DataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                List<ProductVendor> vendors = dataContext.GetTable<ProductVendor>().ToList();
                string answer = products.GetVendorProductList(vendors);
                string[] lines = answer.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual(lines.Length, 460);
                Assert.IsTrue(lines.Contains("Touring Rim-Premier Sport, Inc."));
                Assert.IsTrue(lines.Contains("Paint - Silver-Carlson Specialties"));
                Assert.IsTrue(lines.Contains("HL Shell-First National Sport Co."));
                Assert.IsTrue(lines.Contains("Chain-Varsity Sport Co."));
            }
        }

        [TestMethod]
        public void ListProductVendorLambda()
        {
            using (DataClasses1DataContext dataContext = new DataClasses1DataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                List<ProductVendor> vendors = dataContext.GetTable<ProductVendor>().ToList();
                string answer = products.GetVendorProductListLambda(vendors);
                string[] lines = answer.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                Assert.AreEqual(lines.Length, 460);
                Assert.IsTrue(lines.Contains("Touring Rim-Premier Sport, Inc."));
                Assert.IsTrue(lines.Contains("Paint - Silver-Carlson Specialties"));
                Assert.IsTrue(lines.Contains("HL Shell-First National Sport Co."));
                Assert.IsTrue(lines.Contains("Chain-Varsity Sport Co."));
            }
        }

        [TestMethod]
        public void ProductsPagination()
        {
            using (DataClasses1DataContext dataContext = new DataClasses1DataContext())
            {
                List<Product> products = dataContext.GetTable<Product>().ToList();
                List<Product> answer = products.GetProductsAsPage(1, 10);
                Assert.AreEqual(answer.Count, 10);
                answer = products.GetProductsAsPage(3, 10);
                Assert.AreEqual(answer.Count, 10);
                int count = products.Count;
                answer = products.GetProductsAsPage(count / 10 + 1, 10);
                Assert.AreEqual(answer.Count, count % 10);
            }
        }
    }
}