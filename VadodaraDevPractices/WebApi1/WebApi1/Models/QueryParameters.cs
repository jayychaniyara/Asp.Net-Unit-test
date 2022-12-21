using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingApi.Models
{
    public class QueryParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10; //default value

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                if (value > maxPageSize)
                {
                    throw new ArgumentOutOfRangeException("PageSize", $"Page Size should be from 1 to {maxPageSize}");
                }
                _pageSize = value;
            }
        }
    }
}
