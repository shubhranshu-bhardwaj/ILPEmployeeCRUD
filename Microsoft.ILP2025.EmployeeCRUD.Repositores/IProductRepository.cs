using Microsoft.ILP2025.EmployeeCRUD.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.ILP2025.EmployeeCRUD.Repositores
{
    public interface IProductRepository
    {
        // get all the products
        Task<List<ProductEntity>> GetAllProducts();

        // create product
        Task CreateProduct(ProductEntity product);

        // update product
        Task UpdateProduct(ProductEntity product);

        // delete product
        Task DeleteProduct(int id);
    }
}