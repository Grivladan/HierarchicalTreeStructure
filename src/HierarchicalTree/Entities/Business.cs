﻿using HierarchicalTree.Interfaces;

namespace HierarchicalTree.Entities
{
    public class Business : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}