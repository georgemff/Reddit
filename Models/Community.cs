using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Reddit.Models;

public class Community
{
    [Key]
    public int Id { get; set; }
    [ForeignKey(nameof(AuthorId))]
    public int AuthorId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    public virtual ICollection<User> SubscriberUsers { get; set; } = new List<User>();



}