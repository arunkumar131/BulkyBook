using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System.Linq;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product product)
        {
            var objFromDb = _db.Products.FirstOrDefault(c => c.Id == product.Id);
            if(objFromDb != null)
            {
                if(product.ImageUrl !=null)
                {
                    objFromDb.ImageUrl = product.ImageUrl;
                }

                objFromDb.ISBN = product.ISBN;
                objFromDb.ListPrice = product.ListPrice;
                objFromDb.Price = product.Price;
                objFromDb.Price100 = product.Price100;
                objFromDb.Price50 = product.Price50;
                objFromDb.Title = product.Title;
                objFromDb.CategoryId = product.CategoryId;
                objFromDb.CoverTypeId = product.CoverTypeId;
                objFromDb.Description = product.Description;
                objFromDb.Author = product.Author;
            }
        }
    }
}
