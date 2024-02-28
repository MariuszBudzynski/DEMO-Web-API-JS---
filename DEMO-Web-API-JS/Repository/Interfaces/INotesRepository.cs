using DEMO_Web_API_JS.Models;

namespace DEMO_Web_API_JS.Repository.Interfaces
{
    public interface INotesRepository
    {
        Task<IEnumerable<Note>> GetAllNotes();
        Task<Note> GetNoteById(Guid id);
        Task AddNote(Note note);
        Task UpdateNote(Guid id,Note note);
        Task DeleteNote(Guid id);

    }
}