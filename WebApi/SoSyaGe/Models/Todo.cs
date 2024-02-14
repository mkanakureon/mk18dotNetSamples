namespace SoSyaGeApp.Models
{
    public class Todo
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }

    public class TodoList
    {
        public string? Title { get; set; }
        public List<Todo>? Todos { get; set; }
    }
}
