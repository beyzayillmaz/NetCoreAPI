using NetCoreAPI.Dto;
using NetCoreAPI.Models;
using static NetCoreAPI.Enums.GlobalEnums;

namespace NetCoreAPI.Repository
{
    public class CampaignRepository
    {
        private readonly List<Campaing> _campaigns = new();

        private readonly List<CampaingLog> _campaingsLog = new();

        public List<Campaing> GetAll() => _campaigns;

        public void Add(Campaing c)
        {
            _campaigns.Add(c);
            AddLog(c.Id, c.Status);
        }
        public void AddLog(int Id, CampaignStatus campaignStatus) => _campaingsLog.Add(new CampaingLog { Id = Id, Time = DateTime.Now, Status = campaignStatus });
        public bool Update(int Id, CampaignStatus campaignStatus)
        {
            // Listede aynı Id’ye sahip campaign’i bul
            var existing = _campaigns.Find(c => c.Id == Id);
            if (existing == null)
                return false;  

            existing.Status = campaignStatus;

            AddLog(Id, campaignStatus);

            return true;
        }
        public Campaing? GetById(int id) => _campaigns.Find(c => c.Id == id);

        public List<CampaingLog> GetCampaingLogs(int Id) => _campaingsLog.Where(x => x.Id == Id).ToList();
    }
}
