using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.PublishedModels;
using UmbracoCourse_Web.Models;

namespace UmbracoCourse_Web.ViewComponents
{
    public class DocumentationFormViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(Wiki model)
        {
            var documentationFromModel = new DocumentationFormModel()
            {
                Name = model.Name,
                BodyText = model.BodyText.ToString()
            };
            return View(documentationFromModel);
        }
    }
}
