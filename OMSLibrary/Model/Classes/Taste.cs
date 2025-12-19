namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a taste that can be added to a product
/// </summary>
public class Taste
{
    /// <summary>
    /// Represents a unique Id for each taste
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Represents the name of the taste
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Constructor used when creating an instance 
    /// from the datasource and the Id is known. 
    /// </summary>
    public Taste(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
