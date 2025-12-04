using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents an internal note related to a specific order,
/// used to store comments, special instructions or customer requests.
/// </summary>
public class Note : INote
{
    public int NoteId { get; set; }
    public string NoteText { get; set; }
    public int OrderId { get; set; }
    //public List<string> ImagePaths { get; set; }

    /// <summary>
    /// Constructor used when loading an existing Note from the database.
    /// NoteId is already known and assigned by the database.
    /// </summary>
    public Note(int noteId, string noteText, int orderId)
    {
        NoteId = noteId;
        NoteText = noteText;
        OrderId = orderId;
        //ImagePaths = imagePaths;
    }
    /// <summary>
    /// Constructor used when creating a new Note to be saved to the database.
    /// NoteId will be generated automatically.
    /// </summary>
    public Note(string noteText)
    {
        NoteText = noteText;
        //ImagePaths = imagePaths;
    }
}
