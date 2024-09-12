using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Core.Entities
{
    [Table("chat_message")]
    public class ChatMessage
    {
        [Key]
        [Column("chat_message_id")]
        public Guid Id { get; set; }

        [Column("sender_name")]
        [Required]
        [MaxLength(50)]
        public string? Sender { get; set; } // Имя пользователя, отправившего сообщение

        [Column("message")]
        [Required]
        [MaxLength(500)]
        public string Message { get; set; } // Текст сообщения
        [Column("sent_at")]
        public DateTime SentAt { get; set; } = DateTime.UtcNow; // Время отправки

    }
}
