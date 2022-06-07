using System;
using System.Runtime.Serialization;

namespace UmbracoCourse_Web.Models
{
    [DataContract(Name = "documentation")]
    public class DocumentationVersionModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "publishDate")]
        public DateTime PublishDate { get; set; }

        [DataMember(Name = "versionId")]
        public int VersionId { get; set; }
    }
}
