using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Model
{
    /// <summary>The Class returns an example data table </summary>
    class CPersonExample
    {
        public CPersonExample()
        {
        }

        ///<summary>This method returns an example data table </summary>
        public List<CPerson> ListExampleRows()
        {
            List<CPerson> lPersons = new List<CPerson>
            {
                new CPerson { Id = 1, name="Patrick", surname="Johnson", age =25, city="Graz", height=175 },
                new CPerson { Id = 2, name="Marian", surname="Woronin", age =52, city="Grodzisk Maz", height=181 },
                new CPerson { Id = 3, name="Usain", surname="Bolt", age =37, city="Kingston", height=188 },
                new CPerson { Id = 4, name="Marcell", surname="Jacobs", age =31, city="Trydent", height=183 },
                new CPerson { Id = 5, name="Donovan", surname="Bailey", age =42, city="Ottawa", height=178 },

                new CPerson { Id = 6, name="Frankie", surname="Fredericks", age =52, city="Windhuk", height=192 },
                new CPerson { Id = 7, name="Ato", surname="Boldon", age =42, city="Port-of-Spain", height=181 },
                new CPerson { Id = 8, name="Asafa", surname="Powell", age =32, city="Lozanna", height=177 },
                //new CPerson { Id = 9, name="Silvio", surname="Leonard", age =55, city="Hawana", height=188 },
                //new CPerson { Id =10, name="Patrick", surname="Johnson", age =32, city="Sydney", height=184 },
            };
            return (lPersons);
        }
    }
}
