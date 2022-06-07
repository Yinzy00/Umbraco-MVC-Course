using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Website.Controllers;
using UmbracoCourse_Web.Models;
using UmbracoCourse_Web.ViewModels;

namespace UmbracoCourse_Web.Controllers
{
    public class WikiController : RenderController
    {
        private readonly IPublishedValueFallback _publishedValueFallback;
        //private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
        public WikiController(ILogger<RenderController> logger, 
            ICompositeViewEngine compositeViewEngine, 
            IUmbracoContextAccessor umbracoContextAccessor,
            IPublishedValueFallback publishedValueFallback
            //IBackOfficeSecurityAccessor backOfficeSecurityAccessor
            )
            
            : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _publishedValueFallback = publishedValueFallback;
            //_backOfficeSecurityAccessor = backOfficeSecurityAccessor;
        }

        public async Task<IActionResult> Wiki(ContentModel model)
        {
            var vm = new DocumentationViewModel(model.Content, _publishedValueFallback);
            List<App> randomApps = new List<App>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://random-data-api.com/api/app/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //GET Method
                HttpResponseMessage response = await client.GetAsync("random_app");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    randomApps.Add(JsonConvert.DeserializeObject<App>(data));
                }
                else
                {
                    Console.WriteLine("Internal server Error");
                }
            }
            vm.RandomApps = randomApps;
            return CurrentTemplate(vm);
        }
    }
}
