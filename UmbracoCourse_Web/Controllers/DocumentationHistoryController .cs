using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Extensions;
using UmbracoCourse_Web.Models;

namespace UmbracoCourse_Web.Controllers
{
    [UmbracoMemberAuthorize]
    public class DocumentationHistoryController : UmbracoApiController
    {
        private readonly IContentService _contentService;
        private readonly IPublishedContentQuery _publishedContentQuery;
        public DocumentationHistoryController(IContentService contentService,
            IPublishedContentQuery publishedContentQuery)
        {
            _contentService = contentService;
            _publishedContentQuery = publishedContentQuery;
        }
        [HttpGet]
        public IEnumerable<DocumentationVersionModel> Versions([FromRoute] int id)
        {
            IEnumerable<IContent> list = _contentService.GetVersions(id);
            return list.Select(x => new DocumentationVersionModel
            {
                Name = x.Name,
                PublishDate = x.UpdateDate,
                VersionId = x.VersionId
            });
        }

        [HttpGet]
        public string PublishVersion(int contentId, int versionId)
        {
            _contentService.Rollback(contentId, versionId);

            var content = _contentService.GetById(contentId);
            _contentService.SaveAndPublish(content);

            return _publishedContentQuery.Content(contentId).Url();
        }
    }
    
}
