using System;
using System.Collections.Generic;
using System.Text;

namespace AbstractFactoryServiceDAL.ViewModel
{
    public class StoragesLoadViewModel
    {
        public string StorageName { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<Tuple<string, int>> Materials { get; set; }
    }
}
