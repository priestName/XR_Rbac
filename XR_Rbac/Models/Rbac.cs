namespace XR_Rbac.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Rbac : DbContext
    {
        public Rbac()
            : base("name=Rbac")
        {
        }

        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Module>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Module>()
                .HasMany(e => e.Roles)
                .WithMany(e => e.Modules)
                .Map(m => m.ToTable("RoleModule").MapLeftKey("ModuleID").MapRightKey("RoleID"));

            modelBuilder.Entity<Role>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserRole").MapLeftKey("RoleID").MapRightKey("UserID"));
        }
    }
}
