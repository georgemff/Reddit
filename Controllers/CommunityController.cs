using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reddit.Dtos;
using Reddit.Enums;
using Reddit.Helpers;
using Reddit.Mapper;
using Reddit.Models;


namespace Reddit.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommunityController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CommunityController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Communities>> GetCommunities([FromQuery] CommunitySearch search)
        {
            if (search.PageSize == 0)
                return BadRequest("PageSize must be greater than 0");
            
            if (search.PageNumber == 0)
                return BadRequest("PageNumber must be greater than 0");
            
            if (search.PageSize > 50 || search.PageSize < 0)
                return BadRequest("PageSize must be between 0 and 50");


            string? searchKey = search.SearchKey;
            SortKey? sortKey = search.SortKey;
            bool? isAscending = search.IsAscending;


            IQueryable<Community> queryableCommunity = _context.Communities.AsQueryable();

            if (searchKey != null)
            {
                queryableCommunity = queryableCommunity
                    .Where(c =>
                        c.Description.Contains(searchKey) ||
                        c.Name.Contains(searchKey)
                    ).AsQueryable();
            }

            queryableCommunity = OrderCommunity.OrderCommunityByKey(
                queryableCommunity, sortKey, isAscending);


            List<Community> communityList = await queryableCommunity
                .Skip((search.PageNumber - 1) * search.PageSize)
                .Take(search.PageSize)
                .ToListAsync();

            int totalCount = queryableCommunity.Count();
            int totalPages = (int)Math.Ceiling(totalCount / (double)search.PageSize);

            return new Communities
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                Data = communityList
            };
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Community>> GetCommunity(int id)
        {
            var community = await _context.Communities.FindAsync(id);

            if (community == null)
            {
                return NotFound();
            }

            return community;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCommunity(CreateCommunityDto communityDto)
        {
            var community = _mapper.toCommunity(communityDto);

            await _context.Communities.AddAsync(community);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunity(int id)
        {
            var community = await _context.Communities.FindAsync(id);
            if (community == null)
            {
                return NotFound();
            }

            _context.Communities.Remove(community);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommunity(int id, Community community)
        {
            if (!CommunityExists(id))
            {
                return NotFound();
            }

            _context.Entry(community).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool CommunityExists(int id) => _context.Communities.Any(e => e.Id == id);
    }
}