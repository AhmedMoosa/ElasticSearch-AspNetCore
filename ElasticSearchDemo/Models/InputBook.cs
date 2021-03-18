using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchDemo.Models
{
    public class InputBook
    {
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public int PagesCount { get; set; }
    }
}
