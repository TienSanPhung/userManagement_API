using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using userManagement_API.Models;

namespace userManagement_API.Repository
{
    public interface IBooksRepository : IDisposable
    {
        IEnumerable<Book> GetBooks();
        Book GetBooksByID(int bookId);
        UserInfo GetUserInfo(string email, string password);
        void InsertBooks(Book book);
        void DeleteBooks(int bookId);
        void UpdateBooks(Book book);
        bool ExistBooks(int bookId);
        void Save();
       
    }
}
