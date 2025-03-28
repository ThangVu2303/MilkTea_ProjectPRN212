using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ProjectPRN.Models;

public partial class MilkTeaContext : DbContext
{
    public MilkTeaContext()
    {
    }

    public MilkTeaContext(DbContextOptions<MilkTeaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<DisposedMaterial> DisposedMaterials { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrdersDetail> OrdersDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RawMaterial> RawMaterials { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Staff> Staff { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=MilkTea; Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__F267251E0A8C105A");

            entity.ToTable("Account");

            entity.HasIndex(e => e.Email, "UQ__Account__AB6E61649B0CDC5E").IsUnique();

            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("fullName");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId).HasColumnName("roleId");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Account__roleId__4CA06362");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AdminId).HasName("PK__admin__AD0500A664AE07F3");

            entity.ToTable("admin");

            entity.HasIndex(e => e.AccountId, "UQ__admin__F267251F0AD9F8B9").IsUnique();

            entity.Property(e => e.AdminId).HasColumnName("adminId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");

            entity.HasOne(d => d.Account).WithOne(p => p.Admin)
                .HasForeignKey<Admin>(d => d.AccountId)
                .HasConstraintName("FK__admin__accountId__59FA5E80");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B6AB0FAF2");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).ValueGeneratedNever();
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__B611CB7D38AA4417");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("customerId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.Point)
                .HasDefaultValue(0)
                .HasColumnName("point");

            entity.HasOne(d => d.Account).WithMany(p => p.Customers)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__accoun__5535A963");
        });

        modelBuilder.Entity<DisposedMaterial>(entity =>
        {
            entity.HasKey(e => e.DisposedId).HasName("PK__Disposed__CE4A3AB6F5366569");

            entity.Property(e => e.DateDisposed).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Material).WithMany(p => p.DisposedMaterials)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DisposedM__Mater__797309D9");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCFCE7B748E");

            entity.Property(e => e.OriginalTotal).HasColumnType("money");
            entity.Property(e => e.Total).HasColumnType("money");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__Customer__66603565");

            entity.HasOne(d => d.Staff).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__StaffId__656C112C");
        });

        modelBuilder.Entity<OrdersDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PK__OrdersDe__08D097A33E820EA6");

            entity.ToTable("OrdersDetail");

            entity.Property(e => e.ProductId).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrdersDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdersDet__Order__6B24EA82");

            entity.HasOne(d => d.Product).WithMany(p => p.OrdersDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrdersDet__Produ__6C190EBB");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD8F0CAED4");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasMaxLength(50);
            entity.Property(e => e.Image).HasMaxLength(250);
            entity.Property(e => e.Origin).HasMaxLength(50);
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Categor__5FB337D6");
        });

        modelBuilder.Entity<RawMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PK__RawMater__C50610F78E390BBE");

            entity.ToTable("RawMaterial");

            entity.Property(e => e.CostPerUnit).HasColumnType("money");
            entity.Property(e => e.MaterialName).HasMaxLength(100);
            entity.Property(e => e.Unit).HasMaxLength(20);
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("PK__Recipe__FDD988B0B89CA889");

            entity.ToTable("Recipe", tb => tb.HasTrigger("trg_UpdateProductQuantity_AfterRecipe"));

            entity.Property(e => e.ProductId).HasMaxLength(50);

            entity.HasOne(d => d.Material).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipe__Material__74AE54BC");

            entity.HasOne(d => d.Product).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Recipe__ProductI__73BA3083");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__CD98462A96437C26");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId)
                .ValueGeneratedNever()
                .HasColumnName("roleId");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("roleName");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__Staff__6465E07EC56CD1BA");

            entity.HasIndex(e => e.AccountId, "UQ__Staff__F267251F187D49D2").IsUnique();

            entity.Property(e => e.StaffId).HasColumnName("staffId");
            entity.Property(e => e.AccountId).HasColumnName("accountId");
            entity.Property(e => e.DateOfBirth).HasColumnName("dateOfBirth");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.HireDate).HasColumnName("hireDate");

            entity.HasOne(d => d.Account).WithOne(p => p.Staff)
                .HasForeignKey<Staff>(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Staff__accountId__5165187F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
