using Console.IntroApp.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Console.IntroApp
{
    class Program
    {
        private static string _connectionString = "Server=localhost\\MSSQLSERVER01;Database=DapperSample;integrated security=True;MultipleActiveResultSets=True;";

        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
        }

        static void BasitInsert(SqlConnection connection)
        { 
            //basit olarak sorgu atmak için
            connection.Execute("insert into Product values('Klavye',100,50)");
        }

        static void ParametreliInsert(SqlConnection connection)
        {
            #region Parametreli Insert
            //dışardan parametre alarak sorgu atmak new ile isimsiz değişken yaratırız.
            connection.Execute("insert into Product values(@A,@B,@C)", new
            {
                A = "Laptop",
                B = 100,
                C = 50
            });
            #endregion
        }

        static void BirdenFazlaInsert(SqlConnection connection)
        {
            #region Birden Fazla Insert
            //birden fazla kayıt atmak için array kullanırız.
            connection.Execute("insert into Product values(@A,@B,@C)", new[]
                {
                    new { A = "Laptop", B = 100, C = 50 },
                    new { A = "Telefon", B = 200, C = 250 }
                }
            );
            #endregion
        }

        static void ExecuteScalarMethodu(SqlConnection connection)
        {
            #region ExecuteScalarMethodu
            //ilk kaydı seçili değerini gönderir. * ise id'yi gönderir kolon seçilirse onu gönderir.
            connection.ExecuteScalar("select * from Product");
            #endregion
        }

        static void QueryMethodlari(SqlConnection connection)
        {
            #region Query Methodları

            IEnumerable<dynamic> query = connection.Query("select * from Product");

            IEnumerable<Product> productList = connection.Query<Product>("select * from Product");


            #endregion
        }

        static void Join(SqlConnection connection)
        {
            var query = @"select * from product p inner join ProductCategory pc on p.Id = pc.ProductId";

            var productsDictionary = new Dictionary<int, Product>();

            var products = connection.Query<Product, ProductCategory, Product>(query, (product, productCategory) =>
              {
                  Product otherProduct;

                  if (!productsDictionary.TryGetValue(product.Id, out otherProduct))
                  {
                      otherProduct = product;
                      otherProduct.ProductCategory = new List<ProductCategory>();
                      productsDictionary.Add(product.Id, product);
                  }

                  otherProduct.ProductCategory.Add(productCategory);

                  return otherProduct;
              }).Distinct().ToList();


        }

        static void Transaction(SqlConnection connection)
        {
            var transaction = connection.BeginTransaction();
            transaction.Commit();
            transaction.Rollback();
        }

        static void SPKullanimi(SqlConnection connection)
        {
            connection.Execute("sp_InsertProduct", new { name = "Kitap", Price = 30, Stock = 10 }, commandType: CommandType.StoredProcedure);
        }


    }
}
