using System;
using System.Text;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using DotNetNuke.Services.Cache;
using ICG.Modules.SimpleBootstrapTabs.Components.Repositories;

namespace ICG.Modules.SimpleBootstrapTabs
{
    public partial class View : PortalModuleBase, IActionable
    {
        private const string TabSelectorTemplate =
            "<li role=\"presentation\"{2}><a href=\"#{0}\" aria-controls=\"{0}\" role=\"tab\" data-toggle=\"tab\">{1}</a></li>";

        private const string TabContentTemplate = "<div role=\"tabpanel\" class=\"tab-pane {2}\" id=\"{0}\">{1}{3}</div>";

        #region IActionable Implementation

        public ModuleActionCollection ModuleActions
        {
            get
            {
                var Actions = new ModuleActionCollection
                {
                    {
                        GetNextActionID(), LocalizeString("AddTab"), "", "", "",
                        ModuleContext.EditUrl("tci", "-1", "EditContent"), false,
                        SecurityAccessLevel.Edit, true, false
                    }
                };
                return Actions;
            }
        }

        #endregion

        protected void Page_load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var editLink = string.Empty;

                if (IsEditable)
                {
                    editLink = "<p><a href=\"" + ModuleContext.EditUrl("tci", "-1", "EditContent") + "\">Edit Tab</a></p>";
                }

                //Check for cached content
                var cachedContent = CachingProvider.Instance().GetItem("ICG_BST_" + ModuleId);
                if (cachedContent != null && !IsEditable)
                {
                    litContent.Text = cachedContent.ToString();
                }
                else
                {
                    var items = TabContentRepository.GetModuleTabContents(ModuleId);
                    if (items.Count > 0)
                    {
                        var tabContentBuilder = new StringBuilder();
                        var tabSelectorBuilder = new StringBuilder();
                        var isFirstItem = true;
                        foreach (var item in items)
                        {
                            tabContentBuilder.AppendFormat(TabContentTemplate, item.TabBootstrapId, item.TabContents,
                                isFirstItem ? " active" : string.Empty, editLink.Replace("-1", item.TabContentId.ToString()));
                            tabSelectorBuilder.AppendFormat(TabSelectorTemplate, item.TabBootstrapId, item.TabTitle,
                                isFirstItem ? " class=\"active\"" : string.Empty);
                            isFirstItem = false;
                        }

                        var builder = new StringBuilder();
                        builder.Append("<div role=\"tabpanel\">");
                        builder.Append("<ul class=\"nav nav-tabs\" role=\"tablist\">");
                        builder.Append(tabSelectorBuilder);
                        builder.Append("</ul>");
                        builder.Append("<div class=\"tab-content\">");
                        builder.Append(tabContentBuilder);
                        builder.Append("</div>");
                        builder.Append("</div>");
                        litContent.Text = builder.ToString();
                        if(!IsEditable)
                            CachingProvider.Instance().Insert("ICG_BST_" + ModuleId, litContent.Text);
                    }
                }
            }
        }
    }
}