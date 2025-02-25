
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID
    //Open Closed Principle
    class Program
    {
        //SOLID
        //Open Closed Principle
        static void Main(string[] args)
        {
            ProductTest();
            //ProductTest2();
            //ProductTest3();
            CategoryTest();
        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            var result = categoryManager.GetAll();
            if (result.Success)
            {
                foreach (var category in result.Data)
                {
                    Console.WriteLine(category.CategoryId + "/ İlgili Kategori ismi :  " + category.CategoryName);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }

           
        }

        //private static void ProductTest3()
        //{
        //    ProductManager productManager2 = new ProductManager(new EfProductDal());
        //    foreach (var product in productManager2.GetByUnitPrice(50, 100))
        //    {
        //        Console.WriteLine(product.ProductName);
        //    }
        //}

        //private static void ProductTest2()
        //{
        //    ProductManager productManager = new ProductManager(new EfProductDal());
        //    foreach (var product in productManager.GetAllByCategoryId(2))
        //    {
        //        Console.WriteLine(product.ProductName);
        //    }
        //}

        public static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal(),new CategoryManager(new EfCategoryDal()));

            var result = productManager.GetProductDetails();

            if (result.Success)
            {
                foreach (var product in result.Data)
                {
                    Console.WriteLine(product.ProductName + "/" + product.CategoryName);
                }

            }
            else
            {
                Console.WriteLine(result.Message);
            }

         
          

        }




    }
}

