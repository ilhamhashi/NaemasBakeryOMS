namespace OrderManagerLibrary.Model.Classes;
public class Taste
{
    public int Id { get; set; }
    public string Name { get; set; }

    public Taste(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
