using System.ComponentModel.DataAnnotations;

namespace EmailAPIService.Models
{
    public class Template
    {
        [Key]
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string Name { get; set; }
        [Required, StringLength(150)]
        public string Subject { get; set; }
        [StringLength(500)]
        public string Body { get; set; } // Could be splited into multiple fields to implement placeholders
                                         // f.e. Introduction, MainBody, Task, Option, Conclusion, Signature
    }
}
