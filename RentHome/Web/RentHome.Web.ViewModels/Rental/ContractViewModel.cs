namespace RentHome.Web.ViewModels.Rental
{
    using System;

    using RentHome.Data.Models.Enums;
    using RentHome.Web.ViewModels.Properties;

    public class ContractViewModel
    {
        public string Title { get; set; }

        public string RentDate { get; set; }

        public int? Duration { get; set; }

        public string TenantUserName { get; set; }

        public string OwnerUserName { get; set; }

        public RequestStatus Status { get; set; }

        public PropertiesInListViewModel Property { get; set; }
    }
}
