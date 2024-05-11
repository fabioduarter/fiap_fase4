using GerenciadorDeTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorDeTarefas.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options)
    {
    }

    public DbSet<Tarefa> Tarefas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.HasDefaultSchema("App");

        modelBuilder.Entity<Tarefa>(t =>
        {
            t.HasKey(t => t.Id);
            t.Property(t => t.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();
        });
    }
}
