using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServiceDictionaries.Models
{
    public class Definition
    {
        public virtual int Id { get; protected set; }
        public virtual string Def { get; set; }
        public virtual Word WordObj { get; set; } // potrzebne do referencji słowa, ktre zawera tą definicje
    }
}
