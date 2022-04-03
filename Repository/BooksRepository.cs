using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using userManagement_API.Models;

namespace userManagement_API.Repository
{
    
    public class BooksRepository : IBooksRepository,IDisposable    
    {
        private userManagementDBContext dbContext;

        public BooksRepository(userManagementDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IEnumerable<Book> GetBooks()
        {
            return dbContext.Books.ToList();
        }

        public Book GetBooksByID(int id)
        {
            return dbContext.Books.Find(id);
        }

        public UserInfo GetUserInfo(string email, string password)
        {
            return dbContext.UserInfos.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public void InsertBooks(Book book)
        {
            dbContext.Books.Add(book);
        }

        public void DeleteBooks(int bookID)
        {
            Book book = dbContext.Books.Find(bookID);
            dbContext.Books.Remove(book);
        }

        public void UpdateBooks(Book book)
        {
            dbContext.Entry(book).State = EntityState.Modified;
        }

        public bool ExistBooks(int bookID)
        {
            return dbContext.Books.Any(e => e.BookId == bookID);
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

      
    }
}
