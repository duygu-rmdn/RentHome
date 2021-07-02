namespace RentHome.Common
{
    public static class GlobalConstants
    {
        // Entities Property Constraints
        public const int UsersNameMin = 2;
        public const int UsersNameMax = 50;

        public const int AboutMaxLength = 200;
        public const int ContractTitleMaxLength = 30;
        public const string TitleTooLong = "The title must be less than {1} characters long";

        public const int HomeNameMin = 5;
        public const int HomeNameMax = 40;

        public const int HomeDescriptionMin = 20;
        public const int HomeDescriptionMax = 1000;

        public const int HomeAddressMin = 10;
        public const int HomeAddressMax = 100;

        public const string RegexAddress = @"^[\d-]{1,4}, [A-Za-z-. ]+, [\d]+";
        public const string RegexAddressError = "Enter address in the format: number, street name, post code";

        public const string PriceMin = "10";
        public const string PriceMax = "1000000";

        public const int CountryCodeLength = 2;

        public const int ContractDocumentMaxSize = 4 * 1024 * 1024;
        public const int RequestDocumentMaxSize = 2 * 1024 * 1024;
    }
}
