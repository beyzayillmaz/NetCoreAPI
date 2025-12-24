using static NetCoreAPI.Enums.GlobalEnums;

namespace NetCoreAPI.Dto
{
    public class CampaingLogDTO
    {
        public DateTime Time { get; set; }
        public CampaignStatus Status { get; set; }
    }
}
