﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YuceYazilim.DebuggingTips
{
    public class ProductCategory
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public Category Category { get; set; }
        public Product Product { get; set; }
    }
}
