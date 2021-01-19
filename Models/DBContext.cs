using System.Collections.Generic;

namespace Quiz_NetCore.Models
{
    public class DBContext
    {
        public DBContext()
        {
            //create mock data
            Questions = new List<Question>();            
            for (int i = 0; i < 10; i++)
                Questions.Add(new Question() { ID = i, Text = "Q" + i });
            Categories = new List<Category>();
            for (int i = 0; i < 10; i++)
                Categories.Add(new Category() { ID = i, Name = "C" + i });
        }
        public List<Question> Questions { get; set; }
        public List<Category> Categories { get; set; }
    }
}
