using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reddit.Models;

public class CommunitySubscriber
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(UserId))]
    public int UserId { get; set; }
    [ForeignKey(nameof(CommunityId))]
    public int CommunityId { get; set; }
    
}