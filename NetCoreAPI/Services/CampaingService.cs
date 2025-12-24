using NetCoreAPI.Dto;
using NetCoreAPI.Models;
using NetCoreAPI.Repository;
using AutoMapper;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using static NetCoreAPI.Enums.GlobalEnums;

namespace NetCoreAPI.Services
{
    public class CampaingService
    {
   
        private readonly CampaignRepository _repo;
        private readonly IMapper _mapper;
        private static int _nextId = 1;

        public CampaingService(CampaignRepository repo,IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public ServiceResult<CampaingCreateDTO> Create(CampaingCreateDTO dto)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(dto.Title) || dto.Title.Length < 10 || dto.Title.Length > 50)
                return ServiceResult<CampaingCreateDTO>.Fail("Başlık 10-50 karakter olmalıdır");

            if (string.IsNullOrWhiteSpace(dto.Details) || dto.Details.Length < 20 || dto.Details.Length > 200)
                return ServiceResult<CampaingCreateDTO>.Fail("Detay 20-200 karakter olmalıdır");

            // Yeni entity
            var campaign = new Campaing
            {
                Id = _nextId,
                Title = dto.Title,
                Details = dto.Details,
                Category = dto.Category
            };

            _nextId++;

            // Mükerrer kontrol
            if (_repo.GetAll().Any(x => x.Title == dto.Title && x.Details == dto.Details && x.Category == dto.Category))
                campaign.Status = CampaignStatus.Mukerrer;
            else
                campaign.Status = (dto.Category == CampaignCategory.Hayat) ? CampaignStatus.Aktif : CampaignStatus.OnayBekliyor;

            _repo.Add(campaign);

            var response = _mapper.Map<CampaingCreateDTO>(campaign);

            return ServiceResult<CampaingCreateDTO>.Ok(response);
        }
        public void UpdateStatus(int Id,CampaignStatus campaignStatus)
        {
            CampaingUpdateDto response = new CampaingUpdateDto();
            bool result = _repo.Update(Id, campaignStatus);
            if (!result)
                throw new Exception("Güncellenmek istenen kampanya bulunamadı");
        }
        public ServiceResult<List<CampaingListDTO>> GetAll()
        {
            List<Campaing> AllList = _repo.GetAll();

            List<CampaingListDTO> result = new List<CampaingListDTO>();

            result = AllList.GroupBy(x => x.Status).Select(g => new CampaingListDTO
            {
                Status = g.Key,
                Count = g.Count()
            }).ToList();
            
            return ServiceResult<List<CampaingListDTO>>.Ok(result);
        }
        public ServiceResult<List<CampaingLogDTO>> CampaingMovementLog(int Id)
        {
            List<CampaingLog> campaingLogs = _repo.GetCampaingLogs(Id);

            if(campaingLogs.Count == 0)
                return ServiceResult<List<CampaingLogDTO>>.Fail("Kampanya Hareket Kaydı Bulunamadı.");

            List<CampaingLogDTO> result = new List<CampaingLogDTO>();

            foreach (CampaingLog item in campaingLogs)
            {
                CampaingLogDTO campaingLogDTO = new CampaingLogDTO();
                campaingLogDTO.Status = item.Status;
                campaingLogDTO.Time = item.Time;

                result.Add(campaingLogDTO);
            }

            
            return ServiceResult<List<CampaingLogDTO>>.Ok(result);
        }
    }
}
