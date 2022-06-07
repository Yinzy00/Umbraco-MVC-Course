using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Filters;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Extensions;
using UmbracoCourse_Web.Helpers;
using UmbracoCourse_Web.Models;

namespace UmbracoCourse_Web.Controllers
{
    public class DocumentationFormController : SurfaceController
    {
        private readonly IContentService _contentService;
        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;
        private readonly IMediaService _mediaService;
        private readonly MediaFileManager _mediaFileManager;
        private readonly IShortStringHelper _shortStringHelper;
        private readonly IContentTypeBaseServiceProvider _contentTypeBaseServiceProvider;
        private readonly MediaUrlGeneratorCollection _mediaUrlGenerators;
        public DocumentationFormController(
            IUmbracoContextAccessor umbracoContextAccessor,
            IUmbracoDatabaseFactory databaseFactory,
            ServiceContext services,
            AppCaches appCaches,
            IProfilingLogger profilingLogger,
            IPublishedUrlProvider publishedUrlProvider,
            IPublishedSnapshotAccessor publishedSnapshotAccessor,
            IContentService contentService,
            IMediaService mediaService,
            MediaFileManager mediaFileManager,
            IShortStringHelper shortStringHelper,
            IContentTypeBaseServiceProvider contentTypeBaseServiceProvider,
            MediaUrlGeneratorCollection mediaUrlGenerators
        )
            : base(umbracoContextAccessor,
                  databaseFactory,
                  services,
                  appCaches,
                  profilingLogger,
                  publishedUrlProvider)
        {
            _publishedSnapshotAccessor = publishedSnapshotAccessor;
            _contentService = contentService;
            _mediaFileManager = mediaFileManager;
            _shortStringHelper = shortStringHelper;
            _contentService = contentService;
            _mediaService = mediaService;
            _contentTypeBaseServiceProvider = contentTypeBaseServiceProvider;
            _mediaUrlGenerators = mediaUrlGenerators;


        }

        [HttpPost]
        [UmbracoMemberAuthorize]
        public async Task<IActionResult> Submit(DocumentationFormModel model)
        {
            if (ModelState.IsValid == false)
            {
                return CurrentUmbracoPage();
            }

            var currentPageId = CurrentPage.Id;
            var content = _contentService.GetById(currentPageId);
            content.Name = model.Name;
            var bodyTextProperty = Wiki.GetModelPropertyType(_publishedSnapshotAccessor, d => d.BodyText);
            content.SetValue(bodyTextProperty.Alias, model.BodyText);
            
            if (model.Images.HasFiles() && model.Images.ContainsImages())
            {
                var imagesProperty = Wiki.GetModelPropertyType(_publishedSnapshotAccessor, x => x.Images).Alias;
                
                var dictionary = new List<Dictionary<string, string>>();

                var mdFolder = _mediaService.GetRootMedia().FirstOrDefault(x => x.Name.InvariantEquals(model.Name));
                Guid mdFolderId = Guid.Empty;
                if (mdFolder != null)
                {
                    mdFolderId = mdFolder.Key;
                }
                
                if (mdFolderId == Guid.Empty)
                {
                    var folder = _mediaService.CreateMedia(model.Name, -1, Folder.ModelTypeAlias);
                    _mediaService.Save(folder);
                    mdFolderId = folder.Key;
                }
                
                foreach (var file in model.Images)
                {
                    var image = _mediaService.CreateMedia(file.FileName, mdFolderId, Image.ModelTypeAlias);
                    var filePath = Path.GetTempFileName();

                    using (var stream = System.IO.File.Create(filePath))
                    {
                        await file.CopyToAsync(stream);
                    }
                    
                    var fileInfo = new FileInfo(filePath);
                    var fileStream = fileInfo.OpenReadWithRetry();
                    if (fileStream == null) throw new System.InvalidOperationException("Could not acquire file stream");
                    
                    using (fileStream)
                    {
                        image.SetValue(_mediaFileManager, 
                            _mediaUrlGenerators, 
                            _shortStringHelper, 
                            _contentTypeBaseServiceProvider, 
                            Constants.Conventions.Media.File, 
                            file.FileName, 
                            fileStream, 
                            null, 
                            null);
                        _mediaService.Save(image);
                        
                        var itemToInsert = new Dictionary<string, string>() { 
                            { "key", Guid.NewGuid().ToString() },
                            { "mediaKey", image.Key.ToString() },
                            { "crops", null },
                            { "focalPoint", null }

                        };
                        dictionary.Add(itemToInsert);
                    }
                }
                
                var json = JsonConvert.SerializeObject(dictionary);
                content.SetValue(imagesProperty, json);
            }


            _contentService.SaveAndPublish(content);

            return RedirectToCurrentUmbracoPage();
        }
    }
}
