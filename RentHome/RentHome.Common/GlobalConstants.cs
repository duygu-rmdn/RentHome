namespace RentHome.Common
{
    public static class GlobalConstants
    {
        public const string SystemName = "RentHome";
        public const string SystemEmail = "homerent325@gmail.com";

        public const string AdministratorRoleName = "Administrator";

        // Entities Property Constraints
        public const int UsersNameMin = 2;
        public const int UsersNameMax = 50;

        public const int PropertyNameMin = 5;
        public const int PropertyNameMax = 40;

        public const int PropertyDescriptionMin = 20;
        public const int PropertyDescriptionMax = 1000;

        public const int PropertyAddressMin = 10;
        public const int PropertyAddressMax = 100;

        public const string RegexAddress = @"^[\d-]{1,4}, [A-Za-z-. ]+, [\d]+";
        public const string RegexAddressError = "Enter address in the format: number, street name, post code";

        public const string PriceMin = "10";
        public const string PriceMax = "1000000";

        public const int ContractTitleMaxLength = 30;
        public const int AboutMaxLength = 200;

        public const int ContractDocumentMaxSize = 4 * 1024 * 1024;
        public const int RequestDocumentMaxSize = 2 * 1024 * 1024;

        public const int CountryCodeLength = 2;

        public const int MessageMinLenght = 10;
        public const int MessageMaxLenght = 1000;

        public const int SubjectMinLenght = 10;
        public const int SubjectMaxLenght = 50;

        // TempData and Messages
        public const string TitleTooLong = "The title must be less than {1} characters long";
    }
}
