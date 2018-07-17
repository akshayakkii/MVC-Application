using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class StoreViewModel
    {
        public int StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
    }
}