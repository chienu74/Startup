using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace startup.Models
{
    [Table("PostComment")]
    public class PostComment
    {
        [Key]
        public int CommentId {  get; set; }
        public string? Name {  get; set; }
        public string? Detail {  get; set; }
        public int PostId {  get; set; }
        public bool IsActive {  get; set; }

        public virtual Post? post { get; set; }
    }
}
