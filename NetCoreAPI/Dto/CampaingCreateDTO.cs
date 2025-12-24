using NetCoreAPI.Models;
using static NetCoreAPI.Enums.GlobalEnums;

namespace NetCoreAPI.Dto
{
    public class CampaingCreateDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public CampaignCategory Category { get; set; }
    }
}
