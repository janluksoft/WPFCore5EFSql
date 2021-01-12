using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace WpfApp.Model
{
    //POCO class
    //Below: good declaration from (System.ComponentModel.DataAnnotations.Schema)
    //[Table(name: "Sprinters", Schema = "public")] 
    public class CPerson
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public float age { get; set; }
        public string city { get; set; }
        public float height { get; set; }
    }
}
