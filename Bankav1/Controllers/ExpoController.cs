using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BankSystem.Models;
using Banka.Data;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpoController : ControllerBase
    {
        private readonly DataContext _context;
        public ExpoController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Expo>>> Get()
        {

            return Ok(await _context.Expos.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Expo>> Get(int id)
        {
            var ekspozitura = await _context.Expos.FindAsync(id);
            if (ekspozitura == null)
                return BadRequest("Ekspozitura not found.");
            return Ok(ekspozitura);
        }

        [HttpPost]

        public async Task<ActionResult<List<Expo>>> AddExpo(Expo ekspozitura)
        {
            _context.Expos.Add(ekspozitura);
            await _context.SaveChangesAsync();

            return Ok(await _context.Expos.ToListAsync());
        }
        [HttpPut]

        public async Task<ActionResult<List<Expo>>> UpdateExpo(Expo request)
        {
            var ekspozitura = await _context.Expos.FindAsync(request.Id);
            if (ekspozitura == null)
                return BadRequest("Ekspozitura not found.");

            ekspozitura.RegistrationNumber = request.RegistrationNumber;
            ekspozitura.City = request.City;
            ekspozitura.Address = request.Address;
            ekspozitura.Workers = request.Workers;
            ekspozitura.BankAccounts = request.BankAccounts;

        await _context.SaveChangesAsync();

            return Ok(await _context.Expos.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Expo>>> Delete(int id)
        {
            var ekspozitura = await _context.Expos.FindAsync(id);
            if (ekspozitura == null)
                return BadRequest("Ekspozitura not found.");

            _context.Expos.Remove(ekspozitura);
            await _context.SaveChangesAsync();

            return Ok(await _context.Expos.ToListAsync());
        }

    }
}
