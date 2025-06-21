using Microsoft.ILP2025.EmployeeCRUD.Entities;
using Microsoft.ILP2025.EmployeeCRUD.Repositores;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Microsoft.ILP2025.EmployeeCRUD.Servcies
{
    public class ProductService : IProductService
    {
        public IProductRepository productRepository { get; set; }

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<List<ProductEntity>> GetAllProducts()
        {
            return await productRepository.GetAllProducts();
        }

        public async Task CreateProduct(ProductEntity product)
        {
            await productRepository.CreateProduct(product);
        }

        public async Task UpdateProduct(ProductEntity product)
        {
            await productRepository.UpdateProduct(product);
        }

        public async Task DeleteProduct(int id)
        {
            await productRepository.DeleteProduct(id);
        }
    }
}
