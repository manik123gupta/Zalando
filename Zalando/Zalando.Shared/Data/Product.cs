using System;
using System.Collections.Generic;
using System.Text;

namespace Zalando.Data
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string ProductImage { get; set; }

        private string _productSize;
        public string ProductSize
        {
            get
            {
                return "Size: " + _productSize;
            }
            set
            {
                _productSize = value;
            }
        }

        private string _price;
        public string Price
        {
            get
            {
                return _price+" EUR";
            }
            set
            {
                _price = value;
            }
        }
    }
}
