using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.DAL.Entities
{
    public class Category
    {
        public int category_id { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }

    }
}
