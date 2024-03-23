using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Reddit.Dtos;
using Reddit.Mapper;
using Reddit.Models;

namespace Reddit.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommunityController : ControllerBase
{
    private readonly ApplicationDBContext _context;
    private readonly IMapper _mapper;
    
    public CommunityController(ApplicationDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Community>>> GetCommunities()
    {
       return await _context.Communities.ToListAsync();
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
    public async Task<ActionResult<Community>> AddPostToCommunity(CreateCommunityDto createCommunityDto)
    {
        var community = _mapper.toCommunity(createCommunityDto);

        _context.Communities.Add(community);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCommunity", new { id = community.Id }, community);
    }
    
    
}