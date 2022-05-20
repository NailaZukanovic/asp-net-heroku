using Banka.Data;
using BankSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BankSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BankController : ControllerBase
    {
        private readonly DataContext _context;
        public BankController(DataContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<BankAccount>>> Get()
        {

            return Ok(await _context.BankAccounts.ToListAsync());
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<BankAccount>> Get(int id)
        {
            var ekspozitura = await _context.BankAccounts.FindAsync(id);
            if (ekspozitura == null)
                return BadRequest("Ekspozitura not found.");
            return Ok(ekspozitura);
        }

        [HttpPost]

        public async Task<ActionResult<List<BankAccount>>> AddBank(BankAccount ekspozitura)
        {
            _context.BankAccounts.Add(ekspozitura);
            await _context.SaveChangesAsync();

            return Ok(await _context.BankAccounts.ToListAsync());
        }
        [HttpPut]

        public async Task<ActionResult<List<BankAccount>>> UpdateBank(BankAccount request)
        {
            var ekspozitura = await _context.BankAccounts.FindAsync(request.Id);
            if (ekspozitura == null)
                return BadRequest("Ekspozitura not found.");

            ekspozitura.ClientId = request.ClientId;
            ekspozitura.AccountType = request.AccountType;
            ekspozitura.CurrencyType = request.CurrencyType;
            ekspozitura.ExpoId = request.ExpoId;
            ekspozitura.Balance = request.Balance;
            
            await _context.SaveChangesAsync();

            return Ok(await _context.BankAccounts.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<BankAccount>>> Delete(int id)
        {
            var ekspozitura = await _context.BankAccounts.FindAsync(id);
            if (ekspozitura == null)
                return BadRequest("Ekspozitura not found.");

            _context.BankAccounts.Remove(ekspozitura);
            await _context.SaveChangesAsync();

            return Ok(await _context.BankAccounts.ToListAsync());
        }

    }
}
