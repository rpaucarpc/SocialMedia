using System;
using System.Collections.Generic;

namespace SocialMedia.Core.Entities
{
    public partial class Post :BaseEntity
    {
        public Post()
        {
            Coments = new HashSet<Comment>();
        }

        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public virtual User Users { get; set; }
        public virtual ICollection<Comment> Coments { get; set; }
    }
}
