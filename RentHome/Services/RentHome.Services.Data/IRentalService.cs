﻿namespace RentHome.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using RentHome.Web.ViewModels.Properties;
    using RentHome.Web.ViewModels.Rental;

    public interface IRentalService
    {
        PropertiesInListViewModel GetProperty(string id);

        Task RequestAsync(RequestInputModel input, string userId, string id);

        IEnumerable<MyPropertyRequestsViewModel> MyPropertyRequests(string propertyId);

        Task ApproveAsync(string id, string requestId);

        Task RejectedAsync(string id);
    }
}
