using static NetCoreAPI.Enums.GlobalEnums;

namespace NetCoreAPI.Models
{
    public class Campaing : BaseModel
    {
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public CampaignCategory Category { get; set; }
        public CampaignStatus Status { get; set; }
    }
}
