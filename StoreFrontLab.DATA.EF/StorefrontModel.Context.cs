//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreFrontLab.DATA.EF
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class StoreFrontEntities1 : DbContext
    {
        public StoreFrontEntities1()
            : base("name=StoreFrontEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Archetype> Archetypes { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Element> Elements { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<InStockStatus> InStockStatus1 { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Rarity> Rarities { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<Weapon> Weapons { get; set; }
        public virtual DbSet<UserDetail> UserDetails { get; set; }
    }
}
