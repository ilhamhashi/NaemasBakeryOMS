using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;
/// <summary>
/// Represents an internal note related to a specific order,
/// used to store comments, special instructions or customer requests.
/// </summary>
public class Note : INote
{
    public int Id { get; set; }
    public string Content { get; set; }


    /// <summary>
    /// Constructor used when loading an existing Note from the database.
    /// NoteId is already known and assigned by the database.
    /// </summary>
    public Note(int noteId, string content)
    {
        Id = noteId;
        Content = content;
    }

    /// <summary>
    /// Constructor used when creating a new Note to be saved to the database.
    /// NoteId will be generated automatically.
    /// </summary>
    public Note(string content)
    {
        Content = content;
    }
}
