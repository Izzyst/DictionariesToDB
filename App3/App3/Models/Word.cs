using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App3.Models
{
    public class Word
    {
        public virtual int Id { get; protected set; }
        public virtual string W { get; set; }
        public virtual string Lang { get; set; }
        public virtual IList<Definition> Defs { get; set; }

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