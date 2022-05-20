using BankSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Banka.Data;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly DataContext _context;
        public ClientController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Client>>> Get()
        {

            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Client>> Get(int id)
        {
            var ekspozitura = await _context.Clients.FindAsync(id);
            if (ekspozitura == null)
                return BadRequest("Ekspozitura not found.");
            return Ok(ekspozitura);
        }

        [HttpPost]

        public async Task<ActionResult<List<Client>>> AddClient(Client ekspozitura)
        {
            _context.Clients.Add(ekspozitura);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }
        [HttpPut]

        public async Task<ActionResult<List<Client>>> UpdateClient(Client request)
        {
            var ekspozitura = await _context.Clients.FindAsync(request.Id);
            if (ekspozitura == null)
                return BadRequest("Ekspozitura not found.");

            ekspozitura.BankAccountId = request.BankAccountId;
            ekspozitura.JMBG = request.JMBG;
            ekspozitura.FirstName = request.FirstName;
            ekspozitura.LastName = request.LastName;
            ekspozitura.ParentName = request.ParentName;
            ekspozitura.Address = request.Address;
            ekspozitura.PhoneNumber = request.PhoneNumber;

            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Client>>> Delete(int id)
        {
            var ekspozitura = await _context.Clients.FindAsync(id);
            if (ekspozitura == null)
                return BadRequest("Ekspozitura not found.");

            _context.Clients.Remove(ekspozitura);
            await _context.SaveChangesAsync();

            return Ok(await _context.Clients.ToListAsync());
        }

    }
}
