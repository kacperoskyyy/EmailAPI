using System.ComponentModel.DataAnnotations.Schema;

namespace EmailAPIService.Models
{
    public class Email
    {
        public string recipient { get; set; }
        [ForeignKey("Template")]
        public int templateId { get; set; }
        public Template Template { get; set; }
        public string placeholders { get; set; }    //If you split the body of the template into multiple fields, you can use this field to pass the values of the placeholders
    }
}
