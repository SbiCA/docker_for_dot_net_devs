using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Home.Models
{
    public class OrderForm
    {
        [EmailAddress] 
        public string Email { get; set; }

        [Display(Description = "Number of units that will be created")]
        public int NumberOfUnits { get; set; }
    }

    public class Order
    {
        public int CustomerId { get; set; }
        public int NumberOfContainers { get; set; }
        public int Id { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }

    public class Container
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public DateTime ProductionDate { get; set; }
    }

    public class ContainerContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Container> Conatiners { get; set; }

        public ContainerContext() : base("ContainerContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}