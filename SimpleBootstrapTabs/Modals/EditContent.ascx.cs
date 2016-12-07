using System;
using DotNetNuke.Common;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Cache;
using DotNetNuke.UI.UserControls;
using ICG.Modules.SimpleBootstrapTabs.Components.Models;
using ICG.Modules.SimpleBootstrapTabs.Components.Repositories;

namespace ICG.Modules.SimpleBootstrapTabs.Modals
{
    /// <summary>
    ///     UI control for editing content within the tabs
    /// </summary>
    public partial class EditContent : PortalModuleBase
    {
        /// <summary>
        ///     Gets the tab content identifier.
        /// </summary>
        /// <value>The tab content identifier.</value>
        private int TabContentId
        {
            get
            {
                if (Request.QueryString["tci"] != null)
                    return int.Parse(Request.QueryString["tci"]);
                else
                    return -1;
            }
        }

        /// <summary>
        ///     Text editor UI control for tab content
        /// </summary>
        /// <remarks>
        ///     DO NOT REMOVE: Needed for Visual Studio editor integration
        /// </remarks>
        protected TextEditor txtTabContent;

        /// <summary>
        ///     Page_s the load.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (TabContentId > 0)
                {
                    var toEdit = TabContentRepository.GetTabContentByTabContentId(ModuleId, TabContentId);
                    txtTabContent.Text = toEdit.TabContents;
                    txtTabTitle.Text = toEdit.TabTitle;
                    txtSortOrder.Text = toEdit.SortOrder.ToString();
                    liDelete.Visible = true;
                }
                else
                {
                    txtSortOrder.Text = "0";
                }
            }
        }

        /// <summary>
        ///     BTNs the cancel_ click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect(Globals.NavigateURL(TabId));
        }

        /// <summary>
        ///     BTNs the delete_ click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            TabContentRepository.DeleteTabContent(ModuleId, TabContentId);
            CachingProvider.Instance().Remove("ICG_BST_" + ModuleId);
            btnCancel_Click(sender, e);
        }

        /// <summary>
        ///     BTNs the save_ click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            var toSave = new TabContent
                         {
                             ModuleId = ModuleId,
                             SortOrder = int.Parse(txtSortOrder.Text),
                             TabContentId = TabContentId,
                             TabContents = txtTabContent.Text,
                             TabTitle = txtTabTitle.Text
                         };
            TabContentRepository.SaveTabContent(toSave);

            //Clear Cache
            CachingProvider.Instance().Remove("ICG_BST_" + ModuleId);
            btnCancel_Click(sender, e);
        }
    }
}