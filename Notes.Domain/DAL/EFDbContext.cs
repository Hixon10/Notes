using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Notes.Domain.Entities;

namespace Notes.Domain.DAL
{
    public class EFDbContext : DbContext
    {
        public EFDbContext() : base("name=EFDbContext") { }

        public DbSet<Note> Note { get; set; }
        public DbSet<NoteStatus> NoteStatus { get; set; }
        public DbSet<NoteType> NoteType { get; set; }
        public DbSet<User> User { get; set; }
    }
}
