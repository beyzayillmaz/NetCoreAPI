using static NetCoreAPI.Enums.GlobalEnums;

namespace NetCoreAPI.Dto
{
    public class CampaingUpdateDto
    {
        public int Id { get; set; }
        public CampaignStatus Status { get; set; }
    }
}
