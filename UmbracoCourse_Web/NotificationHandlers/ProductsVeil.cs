using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Security;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace UmbracoCourse_Web.NotificationHandlers
{
    public class ProductsVeil : INotificationHandler<SendingContentNotification>
    {
        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;
        private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
        public ProductsVeil(IPublishedSnapshotAccessor publishedSnapshotAccessor,
            IBackOfficeSecurityAccessor backOfficeSecurityAccessor)
        {
            _publishedSnapshotAccessor = publishedSnapshotAccessor;
            _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
        }

        public void Handle(SendingContentNotification notification)
        {
            if (notification.Content.ContentTypeAlias != Products.ModelTypeAlias)
            {
                return;
            }

            //Only if isAdmin user can see Save & Publish button & change default currency
            var groups = _backOfficeSecurityAccessor.BackOfficeSecurity.CurrentUser.Groups;
            if (!groups.Any(x => x.Alias == Constants.Security.AdminGroupAlias))
            {
                foreach (var variant in notification.Content.Variants)
                {
                    var tab = variant.Tabs.FirstOrDefault(t => t.Label.InvariantContains("shop"));
                    if (tab != null)
                    {
                        tab.Properties = tab.Properties.Where(p => p.Alias != Products.GetModelPropertyType(_publishedSnapshotAccessor, x => x.DefaultCurrency).Alias);
                    }
                }

                notification.Content.AllowPreview = false;
                notification.Content.Urls = null;

                notification.Content.AllowedActions = notification.Content.AllowedActions.Where(x => x.ToString() != "U");
            }
        }
    }
}
