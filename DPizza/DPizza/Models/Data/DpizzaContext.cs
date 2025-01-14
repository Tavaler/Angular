﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DPizza.Models.Data
{
    public partial class DpizzaContext : DbContext
    {
        public DpizzaContext()
        {
        }

        public DpizzaContext(DbContextOptions<DpizzaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Delivery> Delivery { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<PizzaRm> PizzaRm { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Ptype> Ptype { get; set; }
        public virtual DbSet<Purchse> Purchse { get; set; }
        public virtual DbSet<Rm> Rm { get; set; }
        public virtual DbSet<StatusDelivery> StatusDelivery { get; set; }
        public virtual DbSet<Title> Title { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Thai_CI_AS");

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.AdminId).HasColumnName("Admin_id");

                entity.Property(e => e.AdminEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Admin_email");

                entity.Property(e => e.AdminName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Admin_name");

                entity.Property(e => e.AdminPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Admin_password");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.Property(e => e.CartId).HasColumnName("Cart_id");

                entity.Property(e => e.CartAmount).HasColumnName("Cart_Amount");

                entity.Property(e => e.ProductId).HasColumnName("Product_ID");

                entity.Property(e => e.PurchaseId).HasColumnName("Purchase_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Purchase_Product");

                entity.HasOne(d => d.Purchase)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.PurchaseId)
                    .HasConstraintName("FK_Cart_Purchse");
            });

            modelBuilder.Entity<Delivery>(entity =>
            {
                entity.HasKey(e => e.DelId);

                entity.Property(e => e.DelId).HasColumnName("Del_id");

                entity.Property(e => e.DelDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Del_Date");

                entity.Property(e => e.PurchseId).HasColumnName("Purchse_ID");

                entity.Property(e => e.SdelId).HasColumnName("SDel_id");

                entity.HasOne(d => d.Sdel)
                    .WithMany(p => p.Delivery)
                    .HasForeignKey(d => d.SdelId)
                    .HasConstraintName("FK_Delivery_Status Delivery");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId).HasColumnName("Payment_id");

                entity.Property(e => e.PaymentDate).HasColumnName("Payment_Date");

                entity.Property(e => e.PaymentImage)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Payment_Image");

                entity.Property(e => e.PurchseId).HasColumnName("Purchse_ID");

                entity.HasOne(d => d.Purchse)
                    .WithMany(p => p.Payment)
                    .HasForeignKey(d => d.PurchseId)
                    .HasConstraintName("FK_Payment_Purchse");
            });

            modelBuilder.Entity<PizzaRm>(entity =>
            {
                entity.HasKey(e => e.PrId)
                    .HasName("PK_PIZZA_RW");

                entity.ToTable("PIZZA_RM");

                entity.Property(e => e.PrId)
                    .ValueGeneratedNever()
                    .HasColumnName("pr_id");

                entity.Property(e => e.CartId).HasColumnName("CART_ID");

                entity.Property(e => e.PrAmount).HasColumnName("pr_amount");

                entity.Property(e => e.PrName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("pr_name");

                entity.Property(e => e.RmId).HasColumnName("RM_ID");

                entity.HasOne(d => d.Rm)
                    .WithMany(p => p.PizzaRm)
                    .HasForeignKey(d => d.RmId)
                    .HasConstraintName("FK_PIZZA_RM_RM");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PK_Product");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedNever()
                    .HasColumnName("Product_id");

                entity.Property(e => e.PrId).HasColumnName("pr_ID");

                entity.Property(e => e.ProductImage)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Product_image");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Product_name");

                entity.Property(e => e.ProductPrice).HasColumnName("Product_price");

                entity.Property(e => e.PtypeId).HasColumnName("PTYPE_ID");

                entity.HasOne(d => d.Pr)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PrId)
                    .HasConstraintName("FK_Products_PIZZA_RM");

                entity.HasOne(d => d.Ptype)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.PtypeId)
                    .HasConstraintName("FK_Product_PType");
            });

            modelBuilder.Entity<Ptype>(entity =>
            {
                entity.ToTable("PType");

                entity.Property(e => e.PtypeId).HasColumnName("Ptype_id");

                entity.Property(e => e.PtypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Ptype_name");
            });

            modelBuilder.Entity<Purchse>(entity =>
            {
                entity.Property(e => e.PurchseId).HasColumnName("Purchse_id");

                entity.Property(e => e.DelId).HasColumnName("Del_ID");

                entity.Property(e => e.PaymentId).HasColumnName("Payment_ID");

                entity.Property(e => e.PurchseDate)
                    .HasColumnType("date")
                    .HasColumnName("Purchse_Date");

                entity.Property(e => e.PurchseSpay)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Purchse_SPay");

                entity.Property(e => e.PurchseTotol).HasColumnName("Purchse_Totol");

                entity.Property(e => e.UserId).HasColumnName("User_ID");

                entity.HasOne(d => d.Del)
                    .WithMany(p => p.Purchse)
                    .HasForeignKey(d => d.DelId)
                    .HasConstraintName("FK_Purchse_Delivery");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Purchse)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Cart_User");
            });

            modelBuilder.Entity<Rm>(entity =>
            {
                entity.ToTable("RM");

                entity.Property(e => e.RmId).HasColumnName("RM_id");

                entity.Property(e => e.RmImgPath)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("RM_IMG_Path");

                entity.Property(e => e.RmName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RM_name");

                entity.Property(e => e.RmPrice).HasColumnName("RM_price");
            });

            modelBuilder.Entity<StatusDelivery>(entity =>
            {
                entity.HasKey(e => e.SdelId);

                entity.ToTable("Status Delivery");

                entity.Property(e => e.SdelId).HasColumnName("SDel_id");

                entity.Property(e => e.SdelName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SDel_name");
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.Property(e => e.TitleId).HasColumnName("Title_id");

                entity.Property(e => e.TitleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Title_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("User_id");

                entity.Property(e => e.TitleId).HasColumnName("Title_id");

                entity.Property(e => e.UserAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_address");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_email");

                entity.Property(e => e.UserLastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_lastname");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_name");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("User_password");

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.TitleId)
                    .HasConstraintName("FK_User_Title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}