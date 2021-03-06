﻿using System;
using System.Collections.Generic;
using DataLayer.Interfaces;

namespace DataLayer.Entities
{
    [Serializable]
    public class Post : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public DateTime DateTime { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}