﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models.Product
{
    public class GetProductsModel
    {
        
		public const int MaxPageSize = 50;
        public int PageIndex { get; set; } = 1;
        private int _pageSize = 5;
        public int PageSize {
            get { return _pageSize; }
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string Sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string? Search { get; set; }
    }

}
