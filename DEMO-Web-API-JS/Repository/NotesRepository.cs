using DEMO_Web_API_JS.Data;
using DEMO_Web_API_JS.Models;
using DEMO_Web_API_JS.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DEMO_Web_API_JS.Repository
{
    public class NotesRepository : INotesRepository
    {
        private readonly NotesDbContext _context;

        public NotesRepository(NotesDbContext context)
        {
            _context = context;
        }

        public async Task<Note> AddNote(Note note)
        {
            if (note != null)
            {
                await _context.AddAsync(note);
                await _context.SaveChangesAsync();
                return note;
            }
            return note;

        }

        public async Task DeleteNote(Guid id)
        {
            var notes = await _context.Notes.ToListAsync();
            var noteTobeRemoved = notes.FirstOrDefault(x => x.Id == id);

            if (noteTobeRemoved != null)
            {
                _context.Remove(noteTobeRemoved);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Note>> GetAllNotes()
        {
            var notes = await _context.Notes.ToListAsync();
            return notes;
        }

        public async Task<Note> GetNoteById(Guid id)
        {
            var notes = await _context.Notes.ToListAsync();
            return notes.FirstOrDefault(x=>x.Id==id);

        }

        public async Task UpdateNote(Guid id, Note note)
        {
            //using Update can cause issues so i am using manual update
            var notes = await _context.Notes.ToListAsync();
            var noteToUpdate = notes.FirstOrDefault(x => x.Id == id);

            if (noteToUpdate != null)
            {
                noteToUpdate.Title = note.Title;
                noteToUpdate.Description = note.Description;
                noteToUpdate.IsVisible = note.IsVisible;

                await _context.SaveChangesAsync(true);
            }
 
        }
    }
}
