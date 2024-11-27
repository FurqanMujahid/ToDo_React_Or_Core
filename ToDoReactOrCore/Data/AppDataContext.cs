using Microsoft.EntityFrameworkCore;
using ToDoReactOrCore.Models;

namespace ToDoReactOrCore.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions<AppDataContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }
    }
}
