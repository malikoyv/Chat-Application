public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Chat> CreatedChats { get; set; } = new List<Chat>();
}