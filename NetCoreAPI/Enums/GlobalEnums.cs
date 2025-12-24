using System.ComponentModel;

namespace NetCoreAPI.Enums
{
    public class GlobalEnums
    {
        public enum CampaignCategory
        {
            [Description("Tamamlayıcı Sağlık Sigortası")]
            TSS = 1,

            [Description("Özel Sağlık Sigortası")]
            OSS = 2,

            [Description("Hayat Sigortası")]
            Hayat = 3,

            [Description("Diğer Kampanyalar")]
            Diger = 4
        }

        public enum CampaignStatus
        {
            [Description("Onay Bekliyor")]
            OnayBekliyor = 1,

            [Description("Aktif")]
            Aktif = 2,

            [Description("Deaktif")]
            Deaktif = 3,

            [Description("Mükerrer")]
            Mukerrer = 4
        }
    }
}
