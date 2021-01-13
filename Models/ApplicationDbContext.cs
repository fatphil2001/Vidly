using System;
using System.Data.Entity;
using System.Text.RegularExpressions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Vidly.Models
{
    //[DbConfigurationType(typeof(NpgsqlConfiguration))]
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rental> Rentals{ get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties().Configure(c =>
            {
                var name = c.ClrPropertyInfo.Name;
                var newName = snakeCase(name);
                c.HasColumnName(newName);
            });


            modelBuilder.Types()
                .Configure(c => c.ToTable(GetTableName(c.ClrType)));

            base.OnModelCreating(modelBuilder);

        }

        private string snakeCase(string source)
        {
            var result = Regex.Replace(source, ".[A-Z]", m => m.Value[0] + "_" + m.Value[1]);
            return result.ToLower();
        }

        private string GetTableName(Type type)
        {
            return snakeCase(type.Name);
        }
    }

    //static class ModelBuilderExtension
    //{
    //    public static void NamesToSnakeCase(this DbModelBuilder modelBuilder)
    //    {
    //        //modelBuilder.Properties().Where(p => p.N)

    //        //modelBuilder.Properties()

    //        //foreach (var entity in modelBuilder.Model.GetEntityTypes())
    //        //{
    //        //    // Replace table names
    //        //    entity.Relational().TableName = entity.Relational().TableName.ToSnakeCase();

    //        //    // Replace column names            
    //        //    foreach (var property in entity.GetProperties())
    //        //    {
    //        //        property.Relational().ColumnName = property.Name.ToSnakeCase();
    //        //    }

    //        //    foreach (var key in entity.GetKeys())
    //        //    {
    //        //        key.Relational().Name = key.Relational().Name.ToSnakeCase();
    //        //    }

    //        //    foreach (var key in entity.GetForeignKeys())
    //        //    {
    //        //        key.Relational().Name = key.Relational().Name.ToSnakeCase();
    //        //    }

    //        //    foreach (var index in entity.GetIndexes())
    //        //    {
    //        //        index.Relational().Name = index.Relational().Name.ToSnakeCase();
    //        //    }
    //        //}
    //    }
    //}
}