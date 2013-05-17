using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Notes.Domain.Entities;

namespace Notes.Domain.DAL
{
    public class UnituOfWork : IDisposable
    {
        private GenericRepository<Note> noteRepository;
        private GenericRepository<NoteStatus> noteStatusRepository;
        private GenericRepository<NoteType> noteTypeRepository;
        private GenericRepository<User> userRepository;

        EFDbContext context = new EFDbContext();

        #region RepositoryClasses

        public GenericRepository<Note> NoteRepository
        {
            get
            {
                if (this.noteRepository == null)
                    this.noteRepository = new GenericRepository<Note>(context);
                return noteRepository;
            }
        }

        public GenericRepository<NoteStatus> NoteStatusRepository
        {
            get
            {
                if (this.noteStatusRepository == null)
                    this.noteStatusRepository = new GenericRepository<NoteStatus>(context);
                return noteStatusRepository;
            }
        }

        public GenericRepository<NoteType> NoteTypeRepository
        {
            get
            {
                if (this.noteTypeRepository == null)
                    this.noteTypeRepository = new GenericRepository<NoteType>(context);
                return noteTypeRepository;
            }
        }

        public GenericRepository<User> UserRepository
        {
            get
            {
                if (this.userRepository == null)
                    this.userRepository = new GenericRepository<User>(context);
                return userRepository;
            }
        }

        #endregion

        public void Save()
        {
            if (context != null) context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
