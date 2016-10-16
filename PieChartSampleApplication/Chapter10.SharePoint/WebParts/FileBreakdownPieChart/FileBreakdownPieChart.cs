using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace Chapter10.SharePoint.WebParts
{
    [ToolboxItemAttribute(false)]
    public class FileBreakdownPieChart : WebPart
    {
        [WebBrowsable(true),
        Category("Custom Settings"),
        WebDisplayName("Chart Title"),
        WebDescription("Title displayed above pie chart"),
        Personalizable(PersonalizationScope.Shared)]
        public string ChartTitle
        {
            get;
            set;
        }

        [WebBrowsable(true),
        Category("Custom Settings"),
        WebDisplayName("Library Title"),
        WebDescription("Title (display name) of document library"),
        Personalizable(PersonalizationScope.Shared)]
        public string LibraryTitle
        {
            get;
            set;
        }

        protected override void CreateChildControls()
        {
            if (String.IsNullOrEmpty(ChartTitle) || String.IsNullOrEmpty(LibraryTitle))
            {
                this.Controls.Add(new LiteralControl(
                    "Configuration Error: Chart Title or Library Title has not been set."));
            }
            else
            {
                try
                {
                    SPSite site = SPContext.Current.Site;
                    SPList mpGallery = site.RootWeb.GetCatalog(SPListTemplateType.MasterPageCatalog);
                    string xapUrl = SPUrlUtility.CombineUrl(mpGallery.RootFolder.ServerRelativeUrl, "Chapter11.Silverlight.xap");

                    string markup = String.Format(
                      @"<div id=""silverlightControlHost"">
                            <object data=""data:application/x-silverlight-2,"" type=""application/x-silverlight-2"" width=""100%"" height=""100%"">
                                <param name=""source"" value=""{0}""/>
                                <param name=""minRuntimeVersion"" value=""5.0.61118.0"" />
                                <param name=""initParams"" value=""MS.SP.url={1},ChartTitle={2},LibraryTitle={3}"" />
                                <param name=""autoUpgrade"" value=""true"" />
                                <a href=""http://go.microsoft.com/fwlink/?LinkID=149156&v=5.0.61118.0"" style=""text-decoration:none"">
                                    <img src=""http://go.microsoft.com/fwlink/?LinkId=161376"" alt=""Get Microsoft Silverlight"" style=""border-style:none""/>
                                </a>
                            </object><iframe id=""_sl_historyFrame"" style=""visibility:hidden;height:0px;width:0px;border:0px""></iframe>
                        </div>",
                        xapUrl,
                        SPHttpUtility.HtmlEncode(SPContext.Current.Web.Url),
                        this.ChartTitle,
                        this.LibraryTitle);

                    this.Controls.Add(new LiteralControl(markup));
                }
                catch (Exception ex)
                {
                    this.Controls.Add(new LiteralControl("Error: " + ex.Message));
                }
            }
        }
    }
}
