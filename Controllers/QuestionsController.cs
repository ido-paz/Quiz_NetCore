using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quiz_NetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz_NetCore.Controllers
{
    public class QuestionsController : ControllerBase
    {
        Quiz_DBContext DB;
        public QuestionsController(Quiz_DBContext db) {
            DB = db;
        }

        [Route("GetQuestions")]
        public ActionResult GetQuestions()
        {
            return Ok(DB.Questions);
        }
        //
        [Route("GetQuestion/{id}")]
        public ActionResult GetQuestion(int id)
        {
            Question q=  DB.Questions.FirstOrDefault(q => q.ID == id);
            if (q == null)
                return NotFound($"question {id} not found");
            return Ok(q);
        }
        //
        [Route("AddQuestion")]
        [HttpPost]
        public ActionResult AddQuestion(Question question2Add)
        {
            if(DB.Questions.Find(q => q.Text.Trim() == question2Add.Text.Trim()) == null)
            {
                DB.Questions.Add(question2Add);
                return Created(Request.Path, question2Add.ID);
            }
            return Conflict("Question allready exists");
        }
        //
        [Route("RemoveQuestion")]
        [HttpDelete]
        public ActionResult RemoveQuestion(int id)
        {
            Question q = DB.Questions.FirstOrDefault(q => q.ID == id);
            if(DB.Questions.Remove(q))
                return Ok($"question {id} removed");
            else
                return NotFound($"question {id} not found");
        }
    }
}
