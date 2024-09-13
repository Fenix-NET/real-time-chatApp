using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Entities
{
    [Table("direct_message")]
    public class DirectMessage
    {
        [Column("direct_message_id")]
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [Column("sender_id")]
        public Guid SenderId { get; set; }
        [Required]
        [Column("received_id")]
        public Guid ReceivedId { get; set; }
        [Required]
        [MaxLength(1000)]
        [Column("message")]
        public string Message { get; set; }
        [Column("is_read")]
        public bool IsRead { get; set; } = false;
        [Column("sent_at")]
        public DateTime SentAt { get; set; } = DateTime.UtcNow; 
    }
}
