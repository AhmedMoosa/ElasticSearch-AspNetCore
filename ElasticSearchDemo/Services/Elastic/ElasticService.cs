using ElasticSearchDemo.Data;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchDemo.Services.Elastic
{
    public class ElasticService : IElasticService
    {
        private readonly ElasticClient esClient;
        public ElasticService(ElasticClient client)
        {
            this.esClient = client;
        }

        public async Task Bulk(Book book)
        {
            await esClient.BulkAsync(b => b.Index(BooksIndexInitializer.IndexName).CreateMany(new[] { book }));
        }

        public ISearchResponse<Book> GetAll()
        {
            var result = esClient.Search<Book>(b => b.Query(q => q.MatchAll())
            .From(0)
            .Size(100));
            return result;
        }

        public ISearchResponse<Book> Search(string term)
        {
            var result = esClient.Search<Book>(b => b.Query(q =>
                q.MultiMatch(m => m.Fields(d => d.Field(f => f.Title).Field(f => f.ShortDescription)).Query(term.ToLower()))));
            //q.Match(t =>
            //        t.Field(f => f.Title)
            //          .Query(term.ToLower()))));
            return result;
        }
    }
}
