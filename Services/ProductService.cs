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
        IEnumerable<Product> ListAll(int pg);
        IEnumerable<Product> SearchAll(int pg, string key);
        IEnumerable<Product> Dataset();
        int TotPageSearch(int pg, string key);
        int TotPage();
        void CreateProduct(Product prod);
        void ProductUpdate(Product prod);
        void ProductDelete(int id);
        void UpdateProdPicture(int id, string file);
        Product GetProductById(int id);
    }

    public class ProductService : IProductService
    {

        private ApplicationDbContext _context;
        private readonly AppSettings _appSettings;


        public ProductService(ApplicationDbContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }        

        public int TotPage() {
            var perpage = 5;
            var totrecs = _context.Products.Count();
            int totpage = (int)Math.Ceiling((float)(totrecs) / perpage);
            return totpage;
        }

        public IEnumerable<Product> ListAll(int pg)
        {
            try {
                var perpage = 5;
                var offset = (pg -1) * perpage;
                var products = _context.Products                                
                    .OrderBy(b => b.Id)
                    .Skip(offset)
                    .Take(perpage)
                    .ToList();
                return products;
            } catch(Exception ex) {
                throw new AppException(ex.Message);              
            }
        }

        public int TotPageSearch(int pg, string key) {
            var perpage = 5;
            var offset = (pg -1) * perpage;     
            var totrecs = _context.Products
                .Where(m => EF.Functions.Like(m.Descriptions, $"%{key}%")).Count();                
            int totpage = (int)Math.Ceiling((float)(totrecs) / perpage);
            return totpage;
        }

        public IEnumerable<Product> SearchAll(int pg, string key)
        {       
            try {
                var perpage = 5;
                var offset = (pg -1) * perpage;     
                var products = _context.Products.FromSqlRaw("SELECT * FROM products WHERE descriptions LIKE '%" + key + "%'")
                .OrderBy(b => b.Id)
                .Skip(offset)
                .Take(perpage)
                .ToList();
                return products;
            } catch(Exception ex) {
                throw new AppException(ex.Message);              
            }
        }

        public IEnumerable<Product> Dataset()
        {
            var products = _context.Products.ToList();
            return products;
        }

        public void CreateProduct(Product prod) {
            Product prodDesc = _context.Products.Where(c => c.Descriptions == prod.Descriptions).FirstOrDefault();
            if (prodDesc is not null) {
                throw new AppException("Product Description is already exists...");
            }

            try {
                _context.Products.Add(prod);
                _context.SaveChanges();
            } catch(Exception ex){
                throw new AppException(ex.Message);              
            }
        }

        public void ProductUpdate(Product prods) {
            var prod = _context.Products.Find(prods.Id);
            if (prod is null) {
                throw new AppException("Product not found");
            }
            
            if (!string.IsNullOrWhiteSpace(prods.Category)) {
                prod.Category = prods.Category;
            }

            if (!string.IsNullOrWhiteSpace(prods.Descriptions)) {
                prod.Descriptions = prods.Descriptions;
            }

            if (!string.IsNullOrWhiteSpace(prods.Unit)) {
                prod.Unit = prods.Unit;
            }

            DateTime now = DateTime.Now;
            prod.UpdatedAt = now;
            _context.Products.Update(prod);
            _context.SaveChanges();                    
        }

        public void ProductDelete(int id) {
            var prod = _context.Products.Find(id);
            if (prod is not null)
            {
                _context.Products.Remove(prod);
                _context.SaveChanges();
            }
            else {
               throw new AppException("Product not found");
            }   
        }

        public void UpdateProdPicture(int id, string file) {
            var prod = _context.Products.Find(id);
            if (prod is not null)
            {
                prod.ProductPicture = file;
                _context.Products.Update(prod);
                _context.SaveChanges();
            }
            else {
               throw new AppException("Product not found");
            }                    
        }

        public Product GetProductById(int id) {
                var prod = _context.Products.Find(id);
                if (prod == null) {
                    throw new AppException("Product does'not exists....");
                }
                return prod;
        }
    }
}