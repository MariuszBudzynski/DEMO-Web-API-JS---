using DEMO_Web_API_JS.Models;
using DEMO_Web_API_JS.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DEMO_Web_API_JS.Controllers
{
    [ApiController] //No Views
    [Route("api/[controller]")] //automatic routing configuration
    public class NotesController : Controller
    {
        private readonly INotesRepository _notesRepository;

        public NotesController(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            var notes = await _notesRepository.GetAllNotes();

            if (notes.IsNullOrEmpty())
            {
                return NotFound();
            }
            return Ok(notes);
        }

        [HttpGet]
        [Route("{id:guid}")] //adding additonal parameter to the route of type GUID
        public async Task<IActionResult> GetNoteById(Guid id)
        {
            var note = await _notesRepository.GetNoteById(id);

            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }
        [HttpPost]
        public async Task<IActionResult> AddNote(Note note)
        {
            if (note == null)
            {
                return BadRequest();
            }
            else
            {
                note.Id = new Guid();
                var addedNote =  await _notesRepository.AddNote(note);
                return CreatedAtAction(nameof(GetNoteById),new {id=note.Id },note);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")] //we need id to know what item to update the Note will be model binded form the JSON request
        public async Task<IActionResult> UpdateNote(Guid id,Note note)
        {
            if (id == Guid.Empty || note == null)
            {
                return NotFound();
            }
            else
            {
                await _notesRepository.UpdateNote(id,note);
                return Ok(note);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var doesNoteExist = await _notesRepository.GetNoteById(id);

            if (id == Guid.Empty || doesNoteExist == null)
            {
                return NotFound();
            }
            else
            {
                await _notesRepository.DeleteNote(id);
                return Ok();
            }
        }

    }
}
