using AtmMachine.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using AtmMachine.Classes;

namespace AtmMachine.Controllers
{
    [ApiController]
    [Route("Home")]
    public class ATMController : ControllerBase
    {
        private readonly ATMContext _context;

        public ATMController(ATMContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<string> WelcomeMessage()
        {
            return "Welcome to Atm";
        }
        [HttpGet("UserBy/{id}")]
        public async Task<ActionResult<UserResult>>GetUserById(int id)
        {
         
            var users = await _context.UserResults
                .FromSqlInterpolated($"EXEC GetUserById {id}")
                .ToListAsync();
            return Ok(users);
        }
        [HttpGet("GetALlUsers")]
        public async Task<ActionResult<IEnumerable<UserName>>> GetUserNames()
        {
            var UserNameList = await _context.UserNames.ToListAsync();
            return Ok(UserNameList);
        }
        [HttpGet("GetAllAccount")]
        public async Task<ActionResult<List<AccountDetailsDto>>> GetAllAccount()
        {
            var accounts = await _context.Accounts
                .Select(a => new AccountDetailsDto
                {
                    Id = a.Id,
                    Balance = a.Balance,
                    AccountPassword = a.AccountPassword,
                    ModifiedBy=a.ModifiedBy,
                    FirstName = a.Person.FirstName,
                    LastName = a.Person.LastName,
                    CNIC = a.Person.CNIC
                })
                .ToListAsync();

            return Ok(accounts);
        }

        [HttpGet("details")]
        public async Task<ActionResult<AccountDetailsDto>> GetAccountDetails(string cnic,string password)
        {
            var account = await _context.Accounts
                .Where(a=>a.AccountPassword== password)
                .Include(a => a.Person)
                .Where(a => a.Person.CNIC == cnic)
                
                .FirstOrDefaultAsync();


            if (account == null)
                return NotFound("Account not found");


            var result = new AccountDetailsDto
            {
                Id = account.Id,
                Balance = account.Balance,
                AccountPassword = account.AccountPassword,

                FirstName = account.Person.FirstName,
                LastName = account.Person.LastName,
                CNIC = account.Person.CNIC
            };


            return Ok(result);
        }
        [HttpPost("CreateAccount")]
        public async Task<ActionResult<Account>> CreateAccount(Account account)
        {
            bool isValidPass = AccountHandler.isValidPassword(account.AccountPassword);
            bool isValidCnic = AccountHandler.isValidChic(account.AccountPassword);
            bool cond = isValidPass && isValidCnic;
            if (cond)
            {

                await _context.Accounts.AddAsync(account);

                int rowsAffected = await _context.SaveChangesAsync();
                if (rowsAffected > 0)
                {
                    return Ok("Account created");
                }
                else
                {
                    return Ok("Sorry Something Went Wrong");
                }
            }
            else
            {
                return Ok("Bad request");
            }





        }
    }
    public class UserResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
