using ElasticSearchDemo.Data;
using ElasticSearchDemo.Models;
using ElasticSearchDemo.Services.Elastic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchDemo.Services
{
    public class BookService : IBookService
    {
        public BookService(ApplicationDbContext dbContext, IElasticService esService)
        {
            DbContext = dbContext;
            EsService = esService;
        }

        public ApplicationDbContext DbContext { get; }
        public IElasticService EsService { get; }

        public async Task Create(InputBook book)
        {
            var bookToAdd = new Book
            {
                PagesCount = book.PagesCount,
                ShortDescription = book.ShortDescription,
                Title = book.Title
            };
            await DbContext.AddAsync(bookToAdd);
            var result = await DbContext.SaveChangesAsync();
            if (result > 0)
            {
                await EsService.Bulk(bookToAdd);
            }
        }
    }
}
