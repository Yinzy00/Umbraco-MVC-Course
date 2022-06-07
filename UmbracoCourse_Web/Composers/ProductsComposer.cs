using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;
using UmbracoCourse_Web.NotificationHandlers;

namespace UmbracoCourse_Web.Composers
{
    public class ProductsComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.AddNotificationHandler<SendingContentNotification,
           ProductsVeil>();
        }
    }
}
