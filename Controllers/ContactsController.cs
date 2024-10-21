using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class ContactsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContactsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var contacts = await _context.Contacts.Include(c => c.Category).ToListAsync();
        return View(contacts);
    }

    public async Task<IActionResult> Details(int id)
    {
        var contact = await _context.Contacts.Include(c => c.Category)
                                             .FirstOrDefaultAsync(c => c.ContactId == id);
        if (contact == null) return NotFound();
        return View(contact);
    }

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Contact contact)
    {
        if (ModelState.IsValid)
        {
            _context.Add(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", contact.CategoryId);
        return View(contact);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact == null) return NotFound();

        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", contact.CategoryId);
        return View(contact);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Contact contact)
    {
        if (id != contact.ContactId) return BadRequest();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(contact);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Contacts.Any(c => c.ContactId == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Details), new { id = contact.ContactId });
        }
        ViewBag.Categories = new SelectList(_context.Categories, "CategoryId", "Name", contact.CategoryId);
        return View(contact);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var contact = await _context.Contacts.Include(c => c.Category)
                                             .FirstOrDefaultAsync(c => c.ContactId == id);
        if (contact == null) return NotFound();

        return View(contact);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var contact = await _context.Contacts.FindAsync(id);
        if (contact != null)
        {
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
