using System.Collections.Generic;
using Umbraco.Cms.Core.Models.Membership;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoCourse_Web.Models;

namespace UmbracoCourse_Web.ViewModels
{
    public class DocumentationViewModel : Wiki
    {
        public DocumentationViewModel(IPublishedContent content,
            IPublishedValueFallback publishedValueFallback)
            : base(content,
                  publishedValueFallback)
        {
            
        }
        public IEnumerable<App> RandomApps { get; set; }
    }
}
