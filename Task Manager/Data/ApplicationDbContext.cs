namespace Task_Manager.Data;
    using Microsoft.EntityFrameworkCore;
using Models;
public class ApplicationDbContext : DbContext
    {
    public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) {  }
    public DbSet<User> Users { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }  

}

