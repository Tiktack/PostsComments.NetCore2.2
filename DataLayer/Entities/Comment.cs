using System;
using DataLayer.Interfaces;

namespace DataLayer.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }

        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime DateTime { get; set; }
    }
}