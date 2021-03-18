using ElasticSearchDemo.Data;
using ElasticSearchDemo.Models;
using System.Threading.Tasks;

namespace ElasticSearchDemo.Services
{
    public interface IBookService
    {
        Task Create(InputBook book);
    }
}