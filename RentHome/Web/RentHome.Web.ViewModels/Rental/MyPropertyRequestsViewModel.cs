namespace RentHome.Web.ViewModels.Rental
{
    using System;

    using RentHome.Data.Models.Enums;

    public class MyPropertyRequestsViewModel
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string UserEmail { get; set; }

        public RequestType Type { get; set; }

        public string RentDate { get; set; }

        public int? Dutartion { get; set; }

        public string Message { get; set; }
    }
}
