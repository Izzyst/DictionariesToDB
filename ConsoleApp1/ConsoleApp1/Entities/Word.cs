using System.Collections.Generic;

namespace ConsoleApp1.Entities
{
    public class Word
    {
        public virtual int Id { get; protected set; }
        public virtual string W { get; set; }
        public virtual IList<Definition> Defs { get; set; }

        public Word()
        {
            Defs = new List<Definition>();
        }

        public virtual void AddProduct(Definition definition)
        {
            definition.WordObj = this;
            Defs.Add(definition);
        }
    }
}
