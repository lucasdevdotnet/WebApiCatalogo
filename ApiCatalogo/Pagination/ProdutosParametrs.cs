using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogo.Pagination
{
    public class ProdutosParametrs
    {
        const int mxPageSize = 3;
        public int PageNumber { get; set; } = 1;
        public int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > mxPageSize) ? mxPageSize : value;
            }
        }
    }
    
    }
