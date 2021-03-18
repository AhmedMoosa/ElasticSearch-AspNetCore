using ElasticSearchDemo.Data;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchDemo.Services.Elastic
{
    public interface IElasticService
    {
        Task Bulk(Book book);
        ISearchResponse<Book> Search(string term);

        ISearchResponse<Book> GetAll();
    }
}
