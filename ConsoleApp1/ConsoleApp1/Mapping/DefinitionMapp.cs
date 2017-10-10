using ConsoleApp1.Entities;
using FluentNHibernate.Mapping;

namespace ConsoleApp1.Mapping
{
    public class DefinitionMapp : ClassMap<Definition>
    {
        public DefinitionMapp()
        {
            Id(c => c.Id);
            Map(c => c.Def);
            References(c => c.WordObj);// References creates a many-to-one relationship between two entities
        }
    }
}
