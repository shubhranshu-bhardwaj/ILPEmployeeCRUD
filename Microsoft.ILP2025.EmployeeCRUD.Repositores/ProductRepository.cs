using Microsoft.ILP2025.EmployeeCRUD.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Microsoft.ILP2025.EmployeeCRUD.Repositores
{
    public class ProductRepository : IProductRepository
    {
        private readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "product.json");

        private async Task<List<ProductEntity>> ReadFromFileAsync()
        {
            if (!File.Exists(filePath))
            {
                return new List<ProductEntity>();
            }
            string json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<ProductEntity>>(json) ?? new List<ProductEntity>();
        }

        private async Task WriteToFileAsync(List<ProductEntity> products)
        {
            string json = JsonSerializer.Serialize(products, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }

        public async Task<List<ProductEntity>> GetAllProducts()
        {
            return await ReadFromFileAsync();
        }

        public async Task CreateProduct(ProductEntity product)
        {
            var products = await ReadFromFileAsync();
            product.ProductId = products.Any() ? products.Max(p => p.ProductId) + 1 : 1;
            products.Add(product);
            await WriteToFileAsync(products);
        }

        public async Task UpdateProduct(ProductEntity updatedProduct)
        {
            var products = await ReadFromFileAsync();
            var index = products.FindIndex(p => p.ProductId == updatedProduct.ProductId);
            if (index != -1)
            {
                products[index] = updatedProduct;
                await WriteToFileAsync(products);
            }
        }

        public async Task DeleteProduct(int id)
        {
            var products = await ReadFromFileAsync();
            var productToRemove = products.FirstOrDefault(p => p.ProductId == id);
            if (productToRemove != null)
            {
                products.Remove(productToRemove);
                await WriteToFileAsync(products);
            }
        }
    }
}