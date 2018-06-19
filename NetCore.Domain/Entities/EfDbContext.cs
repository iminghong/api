using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using NetCore.Domain.Models.EntitiesModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Domain.Entities
{
    public class EfDbContext:DbContext
    {
        public EfDbContext(DbContextOptions<EfDbContext> options) : base(options)
        {
        }

        //public EfDbContext() : base()
        //{

        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("server=192.168.2.112;uid=sa;pwd=taotao778899!;database=DB_CFD;Max Pool Size=600;");
        }

        public DbSet<CFDAppInfo> CFDAppInfo { get; set; }

        public DbSet<Activity> Activity { get; set; }
    }
}
