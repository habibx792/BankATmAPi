using AtmMachine.Classes;
using AtmMachine.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AtmMachine.Controllers
{
    [ApiController]
    [Route("Transaction")]
    public class TransationContoller : ControllerBase
    {
        private readonly ATMContext context;
        public TransationContoller(ATMContext context)
        {
            this.context = context;
        }
        [HttpGet("CheckBalance")]
        public async Task<ActionResult<double>> CheckDouble([FromQuery] string passWord)
        {
            // Use FirstOrDefaultAsync for async non-blocking execution
            // Select the specific double property to optimize memory and network traffic
            var balance = await context.Accounts
                .Where(a => a.AccountPassword == passWord)
                .Select(a => a.Balance)
                .FirstOrDefaultAsync();

            return Ok(balance);
        }
        [HttpPost("WithDraw/{password}/{amount}")]
        public async Task<ActionResult> WithDraw(string password,double amount)
        {
           
            var account=context.Accounts.Where(a=>a.AccountPassword == password).FirstOrDefault();
            if (account == null)
            {
                return NotFound(new { Message = "Account Does not Exist" });
            }
            bool withDrawSuccess=account.WithDraw(amount);
            if(withDrawSuccess)
            {
                await context.SaveChangesAsync();
                return Ok(new { message = "With Successfully ", New_Balance = account.Balance });
            }
            return BadRequest(new { message = "DithDraw amount Must Be Great Than Zero" });
        }
        [HttpPost("Deposit/{password}/{amount}")]
       
        public async Task<ActionResult> DespoiteAmount(string password, double amount)
        {
            
            if (amount <= 0)
            {
                return BadRequest(new { message = "Invalid Amount. Deposit must be greater than zero." });
            }

            // Fetch the account from the database
            var account = await context.Accounts
                .Where(a => a.AccountPassword == password)
                .FirstOrDefaultAsync();

           
            if (account == null)
            {
                return NotFound(new { message = "Account not found with the provided credentials." });
            }
            account.DepositAmount(amount);
            await context.SaveChangesAsync();

            return Ok(new
            {
                message = "Balance updated successfully",
                newBalance = account.Balance 
            });
        }

    }



}
