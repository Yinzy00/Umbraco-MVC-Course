using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UmbracoCourse_Web.Models
{
    public class DocumentationFormModel
    {
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        public string BodyText { get; set; }
        
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
