using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class ProductSoldViewModel
    {
        public int ProductSoldID { get; set; }
        public int StoreId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        

        public string CustomerName { get; set; }
        public string  ProductName { get; set; }
        public string StoreName { get; set; }
        public System.DateTime DateSold { get; set; }
    }
}