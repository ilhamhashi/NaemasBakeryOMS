namespace OrderManagerLibrary.Model.Classes;
/// <summary>
/// Represents a size that can be added to a product
/// </summary>
public class Size
{
    /// <summary>
    /// Represents a unique Id for each size
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Represents the name of the size
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Constructor used when creating an instance 
    /// from the datasource and the Id is known. 
    /// </summary>
    public Size(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
