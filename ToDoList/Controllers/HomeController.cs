using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMongoCollection<ToDoItem> _todoCollection;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            var client = new MongoClient("mongodb+srv://Ronaldo:estrella123linda@todolist.1qrzg4u.mongodb.net/");
            var database = client.GetDatabase("ToDoList");
            _todoCollection = database.GetCollection<ToDoItem>("Tasks");
        }

        public IActionResult Index()
        {
            var todoList = _todoCollection.Find(todo => true).ToList();
            var model = new TodoViewModel
            {
                TodoList = todoList
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(ToDoItem todo)
        {
            _todoCollection.InsertOne(todo);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(ToDoItem todo)
        {
            var filter = Builders<ToDoItem>.Filter.Eq("Id", todo.Id);
            var update = Builders<ToDoItem>.Update.Set("IsDone", todo.IsDone);
            _todoCollection.UpdateOne(filter, update);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(ToDoItem todo)
        {
            var filter = Builders<ToDoItem>.Filter.Eq("Id", todo.Id);
            _todoCollection.DeleteOne(filter);
            return RedirectToAction("Index");
        }
    }
}
