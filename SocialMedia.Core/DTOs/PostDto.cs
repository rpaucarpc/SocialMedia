using System;
using System.ComponentModel.DataAnnotations;

namespace SocialMedia.Core.DTOs
{
    /// <summary>
    /// Clase DTO para post
    /// </summary>
    public class PostDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

    }
}
