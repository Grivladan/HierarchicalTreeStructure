﻿using HierarchicalTree.Interfaces;
using System.Collections.Generic;

namespace HierarchicalTree.Entities
{
    public class Business : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Country LocationCountry { get; set; }
        public int LocationCountryId { get; set; }
        public virtual ICollection<Family> Families { get; set; }
    }
}
