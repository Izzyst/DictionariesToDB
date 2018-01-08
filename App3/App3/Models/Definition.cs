

namespace App3.Models
{
    public class Definition
    {
        public virtual int Id { get; protected set; }
        public virtual string Def { get; set; }
        public virtual Word WordObj { get; set; } // potrzebne do referencji słowa, ktre zawera tą definicje
    }
}