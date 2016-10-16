using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint.Client;
using Chapter10.Silverlight.Models;

namespace Chapter10.Silverlight.Services
{
    public class PieChartDataService
    {
        public void GetData(Action<List<PieChartFileType>> onSuccess, Action<string> onError)
        {
            try
            {
                string libTitle = (string)App.Current.Resources["LibraryTitle"];

                ClientContext ctx = ClientContext.Current;
                List mpGallery = ctx.Web.Lists.GetByTitle(libTitle);
                CamlQuery query = new CamlQuery();
                query.ViewXml = "<View Scope='Recursive' />";

                ListItemCollection items = mpGallery.GetItems(query);

                ctx.Load(items);
                ctx.ExecuteQueryAsync(
                    (sender, successArgs) =>
                    {
                        SmartDispatcher.BeginInvoke(() =>
                            {
                                List<PieChartFileType> fileTypes = new List<PieChartFileType>();

                                var groups =
                                    from item in items.ToArray()
                                    group item by (string)item["File_x0020_Type"];

                                foreach (var group in groups)
                                {
                                    fileTypes.Add(new PieChartFileType(group.Key, group.Count()));
                                }

                                if (onSuccess != null)
                                {
                                    onSuccess(fileTypes);
                                }
                            }
                        );
                    },
                    (sender, failureArgs) =>
                    {
                        SmartDispatcher.BeginInvoke(() =>
                            {
                                if (onError != null)
                                {
                                    onError(failureArgs.Message);
                                }
                            }
                        );
                    }
                );
            }
            catch (Exception ex)
            {
                SmartDispatcher.BeginInvoke(() =>
                    {
                        if (onError != null)
                        {
                            onError(ex.ToString());
                        }
                    }
                );
            }
        }
    }
}
