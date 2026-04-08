using MySql.Data.MySqlClient;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using core8_vue_mysql.Entities;
using core8_vue_mysql.Helpers;
using core8_vue_mysql.Models;

namespace core8_vue_mysql.Services
{
    public interface IProductService {
        Task<IEnumerable<Product>> ListAllAsync(int pg);
        Task<IEnumerable<Product>> SearchAllAsync(int pg, string key);
        Task<IEnumerable<Product>> DatasetAsync();
        Task<int> TotPageSearchAsync(int pg, string key);
        Task<int> TotPageAsync();
        Task CreateProductAsync(Product prod);
        Task ProductUpdateAsync(Product prod);
        Task ProductDeleteAsync(int id);
        Task UpdateProdPictureAsync(int id, string file);
        Task<Product> GetProductByIdAsync(int id);        
        // IEnumerable<Product> ListAll(int pg);
        // IEnumerable<Product> SearchAll(int pg, string key);
        // IEnumerable<Product> Dataset();
        // int TotPageSearch(int pg, string key);
        // int TotPage();
        // void CreateProduct(Product prod);
        // void ProductUpdate(Product prod);
        // void ProductDelete(int id);
        // void UpdateProdPicture(int id, string file);
        // Product GetProductById(int id);
    }

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly AppSettings _appSettings;

    public ProductService(ApplicationDbContext context, IOptions<AppSettings> appSettings)
    {
        _context = context;
        _appSettings = appSettings.Value;
    }

    public async Task<int> TotPageAsync() {
        var perpage = 5;
        var totrecs = await _context.Products.CountAsync();
        return (int)Math.Ceiling((float)totrecs / perpage);
    }

    public async Task<IEnumerable<Product>> ListAllAsync(int pg)
    {
        try {
            var perpage = 5;
            var offset = (pg - 1) * perpage;
            return await _context.Products                                
                .OrderBy(b => b.Id)
                .Skip(offset)
                .Take(perpage)
                .ToListAsync();
        } catch(Exception ex) {
            throw new AppException(ex.Message);              
        }
    }

    public async Task<int> TotPageSearchAsync(int pg, string key) {
        var perpage = 5;
        var totrecs = await _context.Products
            .Where(m => EF.Functions.Like(m.Descriptions, $"%{key}%"))
            .CountAsync();                
        return (int)Math.Ceiling((float)totrecs / perpage);
    }

    public async Task<IEnumerable<Product>> SearchAllAsync(int pg, string key)
    {       
        try {
            var perpage = 5;
            var offset = (pg - 1) * perpage;     
            // Using interpolated string for safety or FromSql
            return await _context.Products
                .FromSqlInterpolated($"SELECT * FROM products WHERE descriptions LIKE {"%" + key + "%"}")
                .OrderBy(b => b.Id)
                .Skip(offset)
                .Take(perpage)
                .ToListAsync();
        } catch(Exception ex) {
            throw new AppException(ex.Message);              
        }
    }

    public async Task<IEnumerable<Product>> DatasetAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task CreateProductAsync(Product prod) {
        var exists = await _context.Products.AnyAsync(c => c.Descriptions == prod.Descriptions);
        if (exists) {
            throw new AppException("Product Description already exists...");
        }

        try {
            _context.Products.Add(prod);
            await _context.SaveChangesAsync();
        } catch(Exception ex){
            throw new AppException(ex.Message);              
        }
    }

    public async Task ProductUpdateAsync(Product prods) {
        var prod = await _context.Products.FindAsync(prods.Id);
        if (prod is null) throw new AppException("Product not found");
        
        if (!string.IsNullOrWhiteSpace(prods.Category)) prod.Category = prods.Category;
        if (!string.IsNullOrWhiteSpace(prods.Descriptions)) prod.Descriptions = prods.Descriptions;
        if (!string.IsNullOrWhiteSpace(prods.Unit)) prod.Unit = prods.Unit;

        prod.UpdatedAt = DateTime.Now;
        await _context.SaveChangesAsync();                    
    }

    public async Task ProductDeleteAsync(int id) {
        var prod = await _context.Products.FindAsync(id);
        if (prod is null) throw new AppException("Product not found");

        _context.Products.Remove(prod);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateProdPictureAsync(int id, string file) {
        var prod = await _context.Products.FindAsync(id);
        if (prod is null) throw new AppException("Product not found");

        prod.ProductPicture = file;
        await _context.SaveChangesAsync();                    
    }

    public async Task<Product> GetProductByIdAsync(int id) {
        var prod = await _context.Products.FindAsync(id);
        if (prod == null) throw new AppException("Product does not exist....");
        return prod;
    }
  }
}