namespace ICG.Modules.SimpleBootstrapTabs.Components.Models
{
    /// <summary>
    ///     Represents the content of a single tab
    /// </summary>
    public class TabContent
    {
        /// <summary>
        ///     Gets or sets the tab content identifier.
        /// </summary>
        /// <value>The tab content identifier.</value>
        public int TabContentId { get; set; }

        /// <summary>
        ///     Gets or sets the module identifier.
        /// </summary>
        /// <value>The module identifier.</value>
        public int ModuleId { get; set; }

        /// <summary>
        ///     Gets or sets the tab title.
        /// </summary>
        /// <value>The tab title.</value>
        public string TabTitle { get; set; }

        /// <summary>
        ///     Gets or sets the tab contents.
        /// </summary>
        /// <value>The tab contents.</value>
        public string TabContents { get; set; }

        /// <summary>
        ///     Gets or sets the sort order.
        /// </summary>
        /// <value>The sort order.</value>
        public int SortOrder { get; set; }

        #region Calculated Properties

        /// <summary>
        ///     Identifier to be used for the div id within the bootstrap implementation
        /// </summary>
        /// <value>icg_bst_(ModuleId)_(ContentId)</value>
        public string TabBootstrapId
        {
            get { return string.Format("icg_bst_{0}_{1}", ModuleId, TabContentId); }
        }

        #endregion
    }
}