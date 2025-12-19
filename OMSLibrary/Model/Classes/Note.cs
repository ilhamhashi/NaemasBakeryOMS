namespace OrderManagerLibrary.Model.Classes;
/// <summary>
/// Represents a note related to an order.
/// Used to save comments, special instructions or customer requests.
/// </summary>
public class Note
{
    /// <summary>
    /// Represents a unique Id for each Note
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Represents the content of the note
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Constructor used for adding a 
    /// new order when Id is not known. 
    /// </summary>
    public Note(string content)
    {
        Content = content;
    }

    /// <summary>
    /// Constructor used when creating an instance 
    /// from the datasource and the Id is known. 
    /// </summary>
    public Note(int noteId, string content)
    {
        Id = noteId;
        Content = content;
    }

    /// <summary>
    /// Constructor used when creating an instance
    /// from the datasource and the order is a foreign key
    /// </summary>
    public Note(int id)
    {
        Id = id;
    }
}
