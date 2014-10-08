using System.Data.Entity;
using KMorcinek.YetAnotherTodo.DomainClasses;

namespace KMorcinek.YetAnotherTodo.DataLayer
{
    public class TodoModelContext : DbContext
    {
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}