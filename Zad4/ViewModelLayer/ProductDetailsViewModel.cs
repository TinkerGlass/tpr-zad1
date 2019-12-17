﻿using LogicLayer.Interfaces;
using LogicLayer.Service;
using LogicLayer.Validators;
using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModelLayer.Commands;

namespace ViewModelLayer
{
    public class ProductDetailsViewModel : IViewModel
    {
        //Messages
        public string MessageEmptyFields { get; set; } = "";

        //Static Data
        private int _productID { get; set; } 
        public string ProductName { get; set; } = null;
        public string ProductNumber { get; set; } = null;
        public bool MakeFlag { get; set; }
        public bool FinishedGoodsFlag { get; set; }
        public string Color { get; set; } = null;
        public short SafetyStockLevel { get; set; } = 0;
        public short ReorderPoint { get; set; } = 0;
        public decimal StandardCost { get; set; } = 0;
        public decimal ListPrice { get; set; } = 0;
        public string Size { get; set; } = null;
        public string SizeUnitMeasureCode { get; set; } = null;
        public string WeightUnitMeasureCode { get; set; } = null;
        public decimal? Weight { get; set; } = 0;
        public int DaysToManufacture { get; set; }
        public string ProductLine { get; set; } = null;
        public string Class { get; set; } = null;
        public string Style { get; set; } = null;
        public string ProductSubcategoryID { get; set; } 
        public string ModelId { get; set; } 
        public DateTime? SellEndDate { get; set; } = DateTime.Today;
        public DateTime SellStartDate { get; set; } = DateTime.Today;
        public DateTime? DiscontinuedDate { get; set; } = DateTime.Today;

        //Display Data
        public List<string> Colors { get; set; }
        public List<bool> Flags { get; set; }
        public List<string> Sizes { get; set; }
        public List<string> SizesUnits { get; set; }
        public List<string> WeightUnits { get; set; }
        public List<string> ProductLines { get; set; }
        public List<string> Classes { get; set; }
        public List<string> Styles { get; set; }
        public List<string> ProductSubCategories { get; set; }
        public List<string> ModelIds { get; set; }

        //Actions
        public Action CloseWindow { get; set; }
        public Action<string> DisplayErrorMessage { get; set; }
        //CommandsData
        public string ActionText { get; set; }

        //Commands
        public OwnCommand DisplayMessage { get; set; }
        public OwnCommand DisplayAddWindow { get; set; }
        public OwnCommand AddItemToDataBase { get; set; }

        public IProductService ProductService { get; set; }

        public ProductDetailsViewModel()
        {
            this.ProductService = ServiceProvider.ProductService;
            this.DisplayMessage = new OwnCommand(ShowPopupWindow);
            this.AddItemToDataBase = new OwnCommand(SaveProduct);
            this.Colors = ProductService.GetProductColors();
            this.Sizes = ProductService.GetProductSizes();
            this.SizesUnits = ProductService.GetSizeUnits();
            this.WeightUnits = ProductService.GetWeightUnits();
            this.Flags = new List<bool> { true, false };
            this.ProductLines = ProductService.GetProductLines();
            this.Classes = ProductService.GetProductClasses();
            this.Styles = ProductService.GetProductStyles();
            this.ProductSubCategories = ProductService.GetProductSubcategories();
            this.ModelIds = ProductService.GetProductModels();
        }

        public ProductDetailsViewModel(Product product) : this()
        {
            ProductName = product.Name;
            ProductNumber = product.ProductNumber;
            MakeFlag = product.MakeFlag;
            FinishedGoodsFlag = product.FinishedGoodsFlag;
            Color = product.Color;
            SafetyStockLevel = product.SafetyStockLevel;
            ReorderPoint = product.ReorderPoint;
            StandardCost = product.StandardCost;
            ListPrice = product.ListPrice;
            Size = product.Size;
            SizeUnitMeasureCode = product.SizeUnitMeasureCode;
            WeightUnitMeasureCode = product.WeightUnitMeasureCode;
            Weight = product.Weight;
            DaysToManufacture = product.DaysToManufacture;
            ProductLine = product.ProductLine;
            Class = product.Class;
            Style = product.Style;
            ProductSubcategoryID = "";
            ModelId = "";
            _productID = product.ProductID;
            this.SellStartDate = product.SellStartDate;
            this.SellEndDate = product.SellEndDate;
            this.DiscontinuedDate = product.DiscontinuedDate;
        }
        

        private void ShowPopupWindow()
        {
            DisplayErrorMessage(this.MessageEmptyFields);
        }

        private void SaveProduct()
        {
            this.MessageEmptyFields = "";
            Product product = GetProduct();
            if (ValidateProduct.CheckProduct(product))
            {
                if (ValidateProduct.CheckDate(product))
                {
                    ProductService.Upsert(product);
                }
                else
                {
                    this.MessageEmptyFields = "złe daty gałganie + \n";
                    ShowPopupWindow();
                }
            }
            else
            {
                this.MessageEmptyFields = "pusto w polach "+ "\n" ;
                ShowPopupWindow();
            }
        }

        private Product GetProduct()
        {
            Product product = new Product();
            product.rowguid = new Guid();
            product.Name = this.ProductName;
            product.ProductNumber = this.ProductNumber;
            product.MakeFlag = this.MakeFlag;
            product.FinishedGoodsFlag = this.FinishedGoodsFlag;
            product.Color = this.Color;
            product.SafetyStockLevel = this.SafetyStockLevel;
            product.ReorderPoint = this.ReorderPoint;
            product.StandardCost = this.StandardCost;
            product.ListPrice = this.ListPrice;
            product.Size = this.Size;
            product.SizeUnitMeasureCode = this.SizeUnitMeasureCode;
            product.WeightUnitMeasureCode = this.WeightUnitMeasureCode;
            product.Weight = this.Weight;
            product.DaysToManufacture = this.DaysToManufacture;
            product.ProductLine = this.ProductLine;
            product.Class = this.Class;
            product.Style = this.Style;
            product.ProductSubcategoryID = LinqTools.SelectSubcategoryId(this.ProductSubcategoryID);
            product.ProductModelID = LinqTools.SelectModelId(this.ModelId);
            product.SellStartDate = this.SellStartDate;//this.SellStartDate;
            product.SellEndDate = this.SellEndDate;// this.SellEndDate;
            product.DiscontinuedDate = this.DiscontinuedDate;// this.DiscontinuedDate;
            product.ModifiedDate = DateTime.Today;
            product.ProductID = _productID;
            return product;
        }
    }
}