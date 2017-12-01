using ConsoleApp1.Domain;
using ConsoleApp1.Entities;
using FluentNHibernate.Mapping;

namespace ConsoleApp1.Mapping
{
    public class WordMapp : ClassMap<Word>
    {
        public WordMapp()
        {
            Id(c => c.Id);
            Map(c => c.W).Length(150);
            Map(c => c.Lang).Length(15);
            HasMany(x => x.Defs) //HasMany is creating a one-to-many relationship with Defs (one Word to many Defs)
                .Inverse() //Inverse on HasMany is an NHibernate term, and it means that the other end of the relationship is responsible for saving.
                .Cascade
                .All(); //Cascade.All on HasManyToMany tells NHibernate to cascade events down to the entities in the collection (so when you save the Store, all the Products are saved too).
            // Table("WordDefs"); Table sets the many-to - many join table name.
        }
    }
}
