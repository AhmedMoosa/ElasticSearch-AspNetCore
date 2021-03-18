using ElasticSearchDemo.Data;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchDemo.Services.Elastic
{
    public class BooksIndexInitializer
    {
        public const string IndexName = "books";
        //public const string IndexAnalyzerName = "autocomplete";
        private readonly ElasticClient esClient;
        private readonly ApplicationDbContext db;

        public BooksIndexInitializer(ElasticClient client, ApplicationDbContext db)
        {
            this.esClient = client;
            this.db = db;
        }

        public async Task RunAsync()
        {
            var index = await esClient.Indices.ExistsAsync(IndexName);

            if (index.Exists)
            {
                await esClient.Indices.DeleteAsync(IndexName);
            }

            var createResult = await esClient.Indices.CreateAsync(IndexName, d => d
            //d.Settings(s=> s.Analysis(ad=> ad.CharFilters(c=> c.
              //.Settings(s => s.Analysis(ad => ad.Analyzers(a => a.Custom(IndexAnalyzerName, c => c.Filters("lowercase")))))
              .Map<BookSearchDocument>(m => m.AutoMap()));

            var books = await db.Books.ToListAsync();
            if (books.Any())
            {
                var bulkResult = await esClient.BulkAsync(b => b.Index(IndexName).CreateMany(books));
            }
        }
    }
}
