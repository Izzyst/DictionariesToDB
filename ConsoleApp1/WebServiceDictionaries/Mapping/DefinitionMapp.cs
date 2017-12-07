using FluentNHibernate.Mapping;
using WebServiceDictionaries.Models;

namespace WebServiceDictionaries.Mapping
{
    public class DefinitionMapp : ClassMap<Definition>
    {
        public DefinitionMapp()
        {
            Id(c => c.Id);
            Map(c => c.Def).Length(355);
            References(c => c.WordObj);// References creates a many-to-one relationship between two entities
        }
    }
}
