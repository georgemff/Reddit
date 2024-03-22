using Microsoft.AspNetCore.Mvc;
using Reddit.Mapper;

namespace Reddit.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommunityController : ControllerBase
{
    private readonly ApplcationDBContext _context;
    private readonly IMapper _mapper;
    
    CommunityController(ApplcationDBContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    
}