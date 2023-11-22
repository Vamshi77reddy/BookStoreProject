using BusinessLayer.Interfaces;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Drawing;
using System.Linq;
using System.Security.Claims;

namespace fundooPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly INoteBl noteBl;
        public NoteController(IConfiguration configuration, INoteBl noteBl)
        {
            this.configuration = configuration;
            this.noteBl = noteBl;
        }

        [HttpPost]
        [Route("AddNote")]
        public IActionResult AddNotes(NoteModel model)
        {
            long userId = Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
            var result=noteBl.AddNote(model,userId);
            if (result != null)
            {
                return Ok(new {success = true,message="Note added successful",data=result});
            }
            else
            {
                return BadRequest(new { success = false, message = "Note adding unsuccessful", data = result });
            }
        }

        [HttpGet]
        [Route("GetNotes")]
        public IActionResult GetNotes()
        {
            var result = noteBl.GetAll();
            if (result != null)
            {
                return Ok(new { success = true, message = "Get Note successful", data = result });

            }
            else
            {
                return BadRequest(new { success = false, message = "Get Note  unsuccessful", data = result });
            }
        }

        [HttpGet]
        [Route("GetNoteByName")]
        public IActionResult GetbyName(string name)
        {
            var result= noteBl.GetByName(name);
            if (result != null)
            {
                return Ok(new { success = true, message = "Get Note successful", data = result });

            }
            else
            {
                return BadRequest(new { success = false, message = "Get Note  unsuccessful", data = result });

            }
        }

        [HttpGet]
        [Route("GetByDate")]
        public IActionResult GetBydate(DateTime date)
        {
            var result=noteBl.GetByDate(date);
            if(result != null)
            {
                return Ok(new { success = true, message = "Get Note successful", data = result });

            }
            else
            {
                return BadRequest(new { success = false, message = "Get Note  unsuccessful", data = result });
            }
        }

        [HttpPut]
        [Route("UpdateNote")]
        public IActionResult UpdateNote(NoteModel model,int noteId)
        {
            int userId = Convert.ToInt32(User.Claims.First(x => x.Type == "UserId").Value);
            var result=noteBl.Update(model,userId,noteId);
            if (result != null)
            {
                return Ok(new { success = true, message = "UpdateNote successful", data = result });

            }
            else
            {
                return BadRequest(new { success = false, message = "UpdateNote  unsuccessful", data = result });

            }
        }

        [HttpPut]
        [Route("IsTrash")]
        public IActionResult IsTrash(int noteId)
        {
            int userId=Convert.ToInt32(User.Claims.First(x=>x.Type == "UserId").Value);
            var result=noteBl.IsTrash(userId,noteId);
            if (result)
            {
                return Ok(new { success = true,message="IsTrashed",data=result});
            }
            else
            {
                return BadRequest(new { success = false,message="Not Trashed",data= result});
            }
        }

        [HttpPut]
        [Route("IsArchive")]
        public IActionResult IsArchive(int noteId)
        {
            int userId= Convert.ToInt32(User.Claims.First(x=>x.Type=="UserId").Value);
            var result=noteBl.IsArchive(userId,noteId);
            if (result)
            {
                return Ok(new { success = true, message = "IsArchive", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Not Archived", data = result });

            }
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete(int noteId)
        {
            int userId=Convert.ToInt32(User.Claims.First(x=>x.Type=="UserId").Value);
            var result=noteBl.Delete(userId,noteId);
            if (result)
            {
                return Ok(new { success = true, message = "Delete", data = result });
            }
            else
            {
                return BadRequest(new { success = false, message = "Not Delete", data = result });
            }
        }
    }
}
