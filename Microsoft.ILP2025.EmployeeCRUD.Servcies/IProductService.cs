using Microsoft.ILP2025.EmployeeCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ILP2025.EmployeeCRUD.Servcies
{
    public interface IProductService
    {
        Task<List<ProductEntity>> GetAllProducts();

        Task CreateProduct(ProductEntity product);
        Task UpdateProduct(ProductEntity product);
        Task DeleteProduct(int id);
    }
}
