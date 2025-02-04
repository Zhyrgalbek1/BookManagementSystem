﻿using Domain.Entities;
using Domain.Shared;

namespace Domain.Repositories
{
    /// <summary>
    /// Represents a book repository and inherits from the IRepository<Book> interface.
    /// Представляет репозиторий книг и наследуется от интерфейса IRepository<Book>.
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Returns the book by the specified title.
        /// Возвращает книгу по указанному названию.
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        Task<Book?> GetByTitle(string title);
        /// <summary>
        /// Returns a collection of books with the specified titles.
        /// Возвращает коллекцию книг по указанным названиям.
        /// </summary>
        /// <param name="titles"></param>
        /// <returns></returns>
        Task<IEnumerable<Book>> GetSomeByTitles(params string[] titles);
    }
}
