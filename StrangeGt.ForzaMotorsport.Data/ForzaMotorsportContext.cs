using Microsoft.EntityFrameworkCore;
using StrangeGt.ForzaMotorsport.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrangeGt.ForzaMotorsport.Data
{
    public class ForzaMotorsportContext: DbContext
    {
        public DbSet<UDPData> UDPDatas { get; set; }
     
        private string _databasePath;

        public ForzaMotorsportContext(string databasePath)
        {
            _databasePath = databasePath;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
