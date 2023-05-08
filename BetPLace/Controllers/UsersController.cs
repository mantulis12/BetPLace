using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BetPlace.Data;
using BetPlace.Models;
using BetPlace.Services;
using Microsoft.AspNetCore.Cors;
using BetPlace.Repositories;

namespace BetPlace.Controllers
{
    public class UsersController : Controller
    {
        private readonly BetPlaceContext _context;
        private UserService _userService;
        private JwtService _jwtService;
        private UserRepository _userRepository;

        public UsersController(BetPlaceContext context)
        {
            _context = context;
            _userService = new UserService(context);
            _jwtService = new JwtService();
            _userRepository = new UserRepository(context);
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.User != null ? 
                          View(await _userRepository.GetUsersAsync()) :
                          Problem("Entity set 'BetPlaceContext.User'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Username,Password,Email")] User user)
        {
            if (ModelState.IsValid)
            {
                _userRepository.AddUser(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Username,Password,Email")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _userRepository.UpdateUser(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.User == null)
            {
                return Problem("Entity set 'BetPlaceContext.User'  is null.");
            }
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user != null)
            {
                _userRepository.DeleteUser(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _userRepository.IfUserExists(id);
        }

        [EnableCors]
        [HttpPost("api/login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            // Verify user credentials
            var user = _userService.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            // Generate JWT token
            var token = _jwtService.GenerateToken(user);

            // Return token as JSON
            return Ok(new { token });
        }        
        
        [EnableCors]
        [HttpPost("api/getbalance")]
        public IActionResult getBalance([FromBody] BalanceRequestModel model)
        {
            // Verify user credentials
            if (model.Token == null)
            {
                return Ok(new { });
            }
            var principle = _jwtService.GetPrincipalFromToken(model.Token);
            var claims = principle.Claims.First().Value;
            var user = _context.User.Where(m => m.Email == claims).First();

            if (user == null)
                return BadRequest(new { message = "User not found" });

            // Return token as JSON
            return Ok(new { user.Balance });
        }
    }
}
