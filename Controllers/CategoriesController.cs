using Microsoft.AspNetCore.Mvc;
using Quiz_NetCore.Models;
using System.Linq;

namespace Quiz_NetCore.Controllers
{
    public class CategoriesController : ControllerBase
    {
        Quiz_DBContext DB;
        public CategoriesController(Quiz_DBContext db) {
            DB = db;
        }

        [Route("GetCategories")]
        public ActionResult GetCategories()
        {
            return Ok(DB.Categories);
        }
        //
        [Route("GetCategory/{id}")]
        public ActionResult GetCategory(int id)
        {
            Category q=  DB.Categories.FirstOrDefault(q => q.ID == id);
            if (q == null)
                return NotFound($"category {id} not found");
            return Ok(q);
        }
        //
        [Route("AddCategory")]
        [HttpPost]
        public ActionResult AddCategory(Category category2Add)
        {
            if(DB.Categories.Find(q => q.Name.Trim() == category2Add.Name.Trim()) == null)
            {
                DB.Categories.Add(category2Add);
                return Created(Request.Path, category2Add.ID);
            }
            return Conflict("Category allready exists");
        }
        //
        [Route("RemoveCategory")]
        [HttpDelete]
        public ActionResult RemoveCategory(int id)
        {
            Category q = DB.Categories.FirstOrDefault(q => q.ID == id);
            if(DB.Categories.Remove(q))
                return Ok($"category {id} removed");
            else
                return NotFound($"category {id} not found");
        }
    }
}
