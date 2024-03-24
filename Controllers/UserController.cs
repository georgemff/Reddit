using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reddit.Dtos;
using Reddit.Mapper;
using Reddit.Models;

namespace Reddit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UserController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateUserDto createAuthorDto)
        {
            var author = new User
            {
                Name = createAuthorDto.Name
            };

            await _context.Users.AddAsync(author);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeToCommunity(SubscribeCommunityDto subscribeCommunityDto)
        {
            var community = await _context.Communities.FindAsync(subscribeCommunityDto.CommunityId);
            var user = await _context.Users.FindAsync(subscribeCommunityDto.UserId);
            
            if (community == null || user == null)
            {
                return NotFound();
            }

            var subscriber = new CommunitySubscriber
            {
                CommunityId = subscribeCommunityDto.CommunityId,
                UserId = subscribeCommunityDto.UserId
            };

            await _context.CommunitySubscribers.AddAsync(subscriber);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAuthors()
        {
            return await _context.Users.ToListAsync();
        }
    }
}