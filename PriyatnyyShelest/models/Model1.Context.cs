//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PriyatnyyShelest.models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class PriyatnyyShelestDBEntities : DbContext
    {
        private static PriyatnyyShelestDBEntities _context;
        public PriyatnyyShelestDBEntities()
            : base("name=PriyatnyyShelestDBEntities")
        {
        }
        
        public static PriyatnyyShelestDBEntities GetContext()
        {
            if (_context == null) _context = new PriyatnyyShelestDBEntities();
            return _context;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Agents> Agents { get; set; }
        public virtual DbSet<AgentsLog> AgentsLog { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<ProductSale> ProductSale { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<VW_AgentDetails> VW_AgentDetails { get; set; }
    }
}
