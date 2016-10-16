using System;
using System.Runtime.InteropServices;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace Chapter10.SharePoint.Features.Silverlight
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("8fa74794-9b9c-49fb-a002-56822639dc37")]
    public class Chapter10EventReceiver : SPFeatureReceiver
    {
        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            SPSite site = properties.Feature.Parent as SPSite;

            if (site != null)
            {
                SPWeb web = site.RootWeb;

                SPList mpGallery = web.GetCatalog(SPListTemplateType.MasterPageCatalog);
                string xapFileUrl = SPUrlUtility.CombineUrl(mpGallery.RootFolder.Url, "Chapter10.Silverlight.xap");
                SPFile file = web.GetFile(xapFileUrl);

                if (file.Exists && file.Level != SPFileLevel.Published)
                {
                    if (file.CheckOutType != SPFile.SPCheckOutType.None)
                    {
                        file.CheckIn("Checked in during feature activation", SPCheckinType.MajorCheckIn);
                    }

                    if (file.Level != SPFileLevel.Published)
                    {
                        file.Approve("Approved during feature activation");
                    }
                }
            }
        }
    }
}
