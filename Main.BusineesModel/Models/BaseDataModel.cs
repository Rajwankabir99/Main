using Main.Common;

namespace BusinessModel;

public class BaseDataModel
{
        public BaseDataModel()
        {
            Currency = StaticAppSettings.Currency;
        }

        public string? PageName { get; set; } = string.Empty;

        public EnumCurrency? Currency { get; set; }
       
        public EnumCompanyName? HostCompanyName { get; set; }
       
        public EnumCountry? HostCountry { get; set; }

}
