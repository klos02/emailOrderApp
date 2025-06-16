using System;
using EmailOrderApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmailOrderApp.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<EmailMessage> EmailMessages { get; set; }
}
