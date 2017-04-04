﻿using HierarchicalTree.Interfaces;

namespace HierarchicalTree.Entities
{
    public class Country : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
