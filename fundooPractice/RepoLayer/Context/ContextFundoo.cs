using Microsoft.EntityFrameworkCore;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context
{
    public class ContextFundoo : DbContext
    {
        public ContextFundoo(DbContextOptions<ContextFundoo> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<NoteEntity> Note { get; set; }
    }
}
