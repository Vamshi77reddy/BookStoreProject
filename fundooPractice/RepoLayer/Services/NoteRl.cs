using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entity;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepoLayer.Services
{
    public class NoteRl : INoteRl
    {
        private readonly ContextFundoo contextFundoo;
        private readonly IConfiguration configuration;
        public NoteRl(ContextFundoo contextFundoo, IConfiguration configuration)
        {
            this.contextFundoo = contextFundoo;
            this.configuration = configuration;
        }
        
        public NoteEntity AddNote(NoteModel model,long userId)
        {
            NoteEntity noteEntity = new NoteEntity();
            noteEntity.UserId = userId;
            noteEntity.Title = model.Title;
            noteEntity.Note = model.Note;
            noteEntity.Reminder = model.Reminder;
            noteEntity.Color = model.Color;
            noteEntity.Image = model.Image;
            noteEntity.IsArchive = model.IsArchive;
            noteEntity.Createat = model.Createat;
            noteEntity.Modifiedat = model.Modifiedat;
            contextFundoo.Add(noteEntity);
            contextFundoo.SaveChanges();
            return noteEntity;

        }

        public List<NoteEntity> GetAll()
        {
            var llist=contextFundoo.Note.ToList();
            if(llist.Count > 0 )
            {
                return llist;
            }
            else
            {
                return null;
            }
        }

        public List<NoteEntity> GetByName(string name)
        {
            var result=contextFundoo.Note.Where(x=>x.Title == name).ToList();
            if (result != null)
            {
                return result;
            }
            else { 
            return null;}
        }

        public List<NoteEntity> GetByDate(DateTime date)
        {
            var result=contextFundoo.Note.Where(x=>x.Createat == date).ToList();
            if (result != null)
            {
                return result;
            }
            else
            {
                return null;
            }
        }

        public NoteEntity Update(NoteModel model,int UseId,int NoteId)
        {
            NoteEntity noteEntity = new NoteEntity();   
            noteEntity=contextFundoo.Note.Where(x=>x.UserId==UseId && x.NoteID==NoteId).First();
            if(noteEntity!=null)
            {
                noteEntity.Title = model.Title;
                noteEntity.Note = model.Note;
                noteEntity.Reminder = model.Reminder;
                noteEntity.Color = model.Color;
                noteEntity.Image = model.Image;
                noteEntity.IsArchive = model.IsArchive;
                noteEntity.Createat = model.Createat;
                noteEntity.Modifiedat = model.Modifiedat;
                contextFundoo.SaveChanges();
                return noteEntity;
            }
            else
            {
                return null;
            }
        }

        public bool IsTrash(int userId,int noteId)
        {
            NoteEntity noteEntity=new NoteEntity();
            noteEntity=contextFundoo.Note.Where(x=>x.UserId==userId&&x.NoteID==noteId).First();
            if(noteEntity.IsTrash==true)
            {
                noteEntity.IsTrash = false;
                contextFundoo.SaveChanges();
                return false;
            }
            else if(noteEntity.IsTrash==false)
            { 
            
            noteEntity.IsTrash = true;
            contextFundoo.SaveChanges();
            return true;
            }
            else
            {
                return false;
            }
           
        }

        public bool IsArchive(int userId,int noteId)
        {
            NoteEntity noteEntity = new NoteEntity();
            noteEntity = contextFundoo.Note.Where(x => x.UserId == userId && x.NoteID == noteId).First();
            if (noteEntity.IsArchive == true)
            {
                noteEntity.IsArchive = false;
                contextFundoo.SaveChanges();
                return false;
            }
            else if (noteEntity.IsArchive == false)
            {

                noteEntity.IsArchive = true;
                contextFundoo.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Delete(int userId,int noteId)
        {
            NoteEntity noteEntity = new NoteEntity();
            noteEntity=contextFundoo.Note.Where(x=>x.UserId==userId&&x.NoteID==noteId).First();
            if(noteEntity.IsTrash == true)
            {
                contextFundoo.Remove(noteEntity);
                contextFundoo.SaveChanges();
                return true;
            }
            else
            {
                 noteEntity.IsTrash=true;
                contextFundoo.SaveChanges();
                return false;
            }
        }


    }
}
