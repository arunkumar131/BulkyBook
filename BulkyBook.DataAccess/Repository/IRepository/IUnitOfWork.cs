using System;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        ICompanyRepository Company { get; }
        ISP_Call SP_Call { get; }
        IApplicationUserRepository ApplicationUser { get; }
        void Save();
    }
}
