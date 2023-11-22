using CommonLayer.Models;
using RepoLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Interfaces
{
    public interface INoteRl
    {
        public NoteEntity AddNote(NoteModel model, long userId);
        public List<NoteEntity> GetAll();
        public List<NoteEntity> GetByName(string name);
        public List<NoteEntity> GetByDate(DateTime date);
        public NoteEntity Update(NoteModel model, int UseId, int NoteId);
        public bool IsTrash(int userId, int noteId);
        public bool IsArchive(int userId, int noteId);
        public bool Delete(int userId, int noteId);



    }
}
