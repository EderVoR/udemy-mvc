﻿using LanchesMac.Models;
using Microsoft.EntityFrameworkCore;

namespace LanchesMac.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) :base(opt)
        {

        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Lanche> Lanches { get; set; }
    }
}