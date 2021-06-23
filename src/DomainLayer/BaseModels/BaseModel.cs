using System;
using System.ComponentModel.DataAnnotations;

namespace DomainLayer.BaseModels
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
