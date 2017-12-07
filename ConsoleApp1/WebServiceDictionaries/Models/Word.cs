using System.Collections.Generic;

namespace WebServiceDictionaries.Models
{
    public class Word
    {
        public virtual int Id { get; protected set; }
        public virtual string W { get; set; }
        public virtual string Lang { get; set; }
        public virtual List<Definition> Defs { get; set; }

        public Word()
        {
            Defs = new List<Definition>();
        }

        public virtual void AddDefinition(Definition definition)
        {
            definition.WordObj = this;
            Defs.Add(definition);
        }
    }
}
