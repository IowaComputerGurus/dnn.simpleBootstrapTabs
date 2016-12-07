using System.Collections.Generic;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Data;
using ICG.Modules.SimpleBootstrapTabs.Components.Models;

namespace ICG.Modules.SimpleBootstrapTabs.Components.Repositories
{
    /// <summary>
    ///     Repository class for tab content interaction
    /// </summary>
    public static class TabContentRepository
    {
        /// <summary>
        ///     Gets the module tab contents.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <returns>List&lt;TabContent&gt;.</returns>
        public static List<TabContent> GetModuleTabContents(int moduleId)
        {
            return
                CBO.FillCollection<TabContent>(
                    DataProvider.Instance().ExecuteReader("ICG_BST_TabContentSelectByModuleId", moduleId));
        }

        /// <summary>
        ///     Gets the tab content by tab content identifier.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="tabContentId">The tab content identifier.</param>
        /// <returns>TabContent.</returns>
        public static TabContent GetTabContentByTabContentId(int moduleId, int tabContentId)
        {
            return
                CBO.FillObject<TabContent>(
                    DataProvider.Instance()
                        .ExecuteReader("ICG_BST_TabContentSelectByModuleIdAndContentId", moduleId, tabContentId));
        }

        /// <summary>
        ///     Deletes the content of the tab.
        /// </summary>
        /// <param name="moduleId">The module identifier.</param>
        /// <param name="tabContentId">The tab content identifier.</param>
        public static void DeleteTabContent(int moduleId, int tabContentId)
        {
            DataProvider.Instance().ExecuteNonQuery("ICG_BST_TabContentDelete", moduleId, tabContentId);
        }

        /// <summary>
        ///     Saves the content of the tab.
        /// </summary>
        /// <param name="toSave">To save.</param>
        public static void SaveTabContent(TabContent toSave)
        {
            DataProvider.Instance()
                .ExecuteNonQuery("ICG_BST_TabContentSave", toSave.ModuleId, toSave.TabContentId, toSave.TabTitle,
                    toSave.TabContents, toSave.SortOrder);
        }
    }
}