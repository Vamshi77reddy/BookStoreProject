using BusinessLayer.Interfaces;
using CommonLayer.Models;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class NoteBl : INoteBl
    {
        private readonly INoteRl noteRl;
        public NoteBl(INoteRl noteRl)
        {
            this.noteRl = noteRl;
        }

        public NoteEntity AddNote(NoteModel model, long userId)
        {
           return noteRl.AddNote(model,userId);
        }
        public List<NoteEntity> GetAll()
        {
            return noteRl.GetAll();
        }

        public List<NoteEntity> GetByName(string name)
        {
            return noteRl.GetByName(name);
        }
        public List<NoteEntity> GetByDate(DateTime date)
        {
            return noteRl.GetByDate(date);
        }
        public NoteEntity Update(NoteModel model, int UseId, int NoteId)
        {
            return noteRl.Update(model,UseId, NoteId);
        }

        public bool IsTrash(int userId, int noteId)
        {
            return noteRl.IsTrash(userId, noteId);
        }
        public bool IsArchive(int userId, int noteId)
        {
            return noteRl.IsArchive(userId, noteId);
        }

        public bool Delete(int userId, int noteId)
        {
            return noteRl.Delete(userId, noteId);
        }

    }
}
