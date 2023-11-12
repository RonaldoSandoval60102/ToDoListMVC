using MongoDB.Bson;

namespace ToDoList.Models
{
    public class ToDoItem
    {
        public string? Id { get; set; }

        public string? Title { get; set; }

        public bool IsDone { get; set; }

        public ToDoItem()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
