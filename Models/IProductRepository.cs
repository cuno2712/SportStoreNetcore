using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication7.Models
{
   
        public interface IProductRepository
        {
            IEnumerable<Product> Products { get; }
        IEnumerable<StackInfo> StackInfos { get; }
    }
   
}
