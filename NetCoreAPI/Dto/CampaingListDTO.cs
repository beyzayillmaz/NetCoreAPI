using NetCoreAPI.Models;
using static NetCoreAPI.Enums.GlobalEnums;

namespace NetCoreAPI.Dto
{
    public class CampaingListDTO
    {
        public CampaignStatus Status { get; set; }

        public int Count { get; set; }
    }
}
