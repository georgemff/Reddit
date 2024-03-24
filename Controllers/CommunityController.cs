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
    public async Task<ActionResult<Community>> CreateCommunity(CreateCommunityDto createCommunityDto)
    {
        var community = _mapper.toCommunity(createCommunityDto);

        _context.Communities.Add(community);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCommunity", new { id = community.Id }, community);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Community>> UpdateCommunity(int id, UpdateCommunityDto updateCommunityDto)
    {

        var community = await _context.Communities.FindAsync(id);
        if (community == null)
        {
            return NotFound();
        }

        community.Name = updateCommunityDto.Name;
        community.Description = updateCommunityDto.Description;
        
        _context.Communities.Update(community);
        await _context.SaveChangesAsync();

        return community;

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

        return NoContent();

    }
    
    
}