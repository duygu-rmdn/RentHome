namespace RentHome.Web.ViewModels.Properties
{
    using System;
    using System.Collections.Generic;

    public class PropertiesListViewModel
    {
        public IEnumerable<PropertiesInListViewModel> Properties { get; set; }

        public int PageNumber { get; set; }

        public bool HasPreviousPage => this.PageNumber > 1;

        public bool HasNextPage => this.PageNumber < this.PagesCount;

        public int PreviousPageNumber => this.PageNumber - 1;

        public int NextPageNumber => this.PageNumber + 1;

        public int PagesCount => (int)Math.Ceiling((double)this.PropertiesCount / this.ItemsPerPage);

        public int PropertiesCount { get; set; }

        public int ItemsPerPage { get; set; }
    }
}
