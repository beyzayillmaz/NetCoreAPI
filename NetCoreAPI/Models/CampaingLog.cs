using static NetCoreAPI.Enums.GlobalEnums;

namespace NetCoreAPI.Models
{
    public class CampaingLog
    {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public CampaignStatus Status { get; set; }
    }
}
