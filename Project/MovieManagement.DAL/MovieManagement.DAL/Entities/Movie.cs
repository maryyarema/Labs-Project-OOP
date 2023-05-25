using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.DAL.Entities
{
    public class Movie
    {
        public int movie_id { get; set; }
        public string? title { get; set; }

        public int release_year { get; set; }

        public string? director { get; set; }

        public string? description { get; set; }
    }
}



