namespace OrderManagerLibrary.Model.Classes;
public class Size
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Size(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
