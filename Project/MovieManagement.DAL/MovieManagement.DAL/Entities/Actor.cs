using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManagement.DAL.Entities
{
    public class Actor{
    public int actor_id { get; set; }
    public string? name { get; set; }

    public DateTime birthdate { get; set; }

    public string? nationality { get; set; }

   
}
}
