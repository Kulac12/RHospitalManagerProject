using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

//
namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> {
                new Product{ProductId = 1, CategoryId = 1, ProductName="Bardak", UnitPrice=15,UnitsInStock=15},
                new Product{ProductId = 2, CategoryId = 1, ProductName="Kamera", UnitPrice=500,UnitsInStock=3},
                new Product{ProductId = 3, CategoryId = 2, ProductName="Telefon", UnitPrice=1500,UnitsInStock=2},
                new Product{ProductId = 4, CategoryId = 2, ProductName="Klavye", UnitPrice=150,UnitsInStock=65},
                new Product{ProductId = 5, CategoryId = 2, ProductName="Fare", UnitPrice=85,UnitsInStock=1},
            };
        }


        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //Ben bu "_products.Remove(product);" kodu yazdığımda neden eleman silemem? 
            /// Cevap: referans tip olduğu için silemeyiz.
            // Product productToDelete = new Product(); ----> bu çok hatalı bir kod gereksiz yere referans kodunu aklımızda tuttuk
            //Product productToDelete = null;

            //foreach (var item in _products)
            //{
            //    if (item.ProductId == product.ProductId)
            //    {
            //        productToDelete = item;
            //    }
            //}
            //  productToDelete = _products.SingleOrDefault(p =>);  bu kod forech işlemini yapar
            // Lamba
            /// Lınq Language Intergrate Query
            Product productToDelete = _products.SingleOrDefault(p => p.ProductId == product.ProductId); // Tek bir ürün bulmaya yarar

            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        //veri tabanındaki datayı business katmanına vermem lazım.
        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
            //Where şunu yapar; içindeki şarta uyan tüm elemanları yeni bir listeye ekler ve geri döndürür.
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Product product)
        {

            Product productToUpdate = null;

            //Gönderdiğim ürün Idsine sahip olan listedeki ürünü bul
            productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;
            productToUpdate.CategoryId = product.CategoryId;

        }
    }
}
