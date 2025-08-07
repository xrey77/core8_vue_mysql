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
        IEnumerable<Product> ListAll(int page);
        IEnumerable<Product> SearchAll(int page, string key);
        IEnumerable<Product> Dataset();
        int TotPageSearch(int page, string key);
        int TotPage();
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

        public IEnumerable<Product> ListAll(int page)
        {
            var perpage = 5;
            var offset = (page -1) * perpage;

            var products = _context.Products                                
                .OrderBy(b => b.Id)
                .Skip(offset)
                .Take(perpage)
                .ToList();

            return products;
        }

        public int TotPageSearch(int page, string key) {
            var perpage = 5;
            var offset = (page -1) * perpage;     
            var totrecs = _context.Products
                .Where(m => EF.Functions.Like(m.Descriptions, $"%{key}%")).Count();
                
            // var totrecs = _context.Products.FromSqlRaw("SELECT * FROM products WHERE descriptions LIKE '%" + key + "%'")
            // .OrderBy(b => b.Id)
            // .Skip(offset)
            // .Take(perpage)
            // .ToList();
            int totpage = (int)Math.Ceiling((float)(totrecs) / perpage);
            return totpage;
        }

        public IEnumerable<Product> SearchAll(int page, string key)
        {       
            var perpage = 5;
            var offset = (page -1) * perpage;     
            var products = _context.Products.FromSqlRaw("SELECT * FROM products WHERE descriptions LIKE '%" + key + "%'")
            .OrderBy(b => b.Id)
            .Skip(offset)
            .Take(perpage)
            .ToList();

            return products;
        }

        public IEnumerable<Product> Dataset()
        {
            var products = _context.Products.ToList();
            return products;
        }
    }
}