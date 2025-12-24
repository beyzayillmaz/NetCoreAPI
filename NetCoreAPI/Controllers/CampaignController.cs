using NetCoreAPI.Dto;
using NetCoreAPI.Models;
using NetCoreAPI.Repository;
using NetCoreAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;

namespace NetCoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignController : BaseController
    {

        private readonly CampaingService _service;

        public CampaignController(CampaingService service)
        {
            _service = service;
        }
        [HttpGet("slow")]
        public async Task<IActionResult> Slow()
        {
            await Task.Delay(6000); // 6 saniye
            return Ok("Done");
        }
        /// <summary>
        /// Tüm Kampanyaların Hangi Durumlarda Olduğunu İstatistiksel Gösterir
        /// </summary>
        [HttpGet()]
        [Authorize]
        [SwaggerOperation(Summary = "Kampanya Listesi", Description = "Tüm Kampanya Listesini Durumlarıyla Çekebilirsiniz.")]
        public IActionResult GetAll()
        {
            var result = _service.GetAll();

            return HandleResult(result,200);
        }

        [SwaggerOperation(Summary = "Yeni Kampanya Ekler")]
        [HttpPost("Create")]
        /// <summary>
        /// Kampanya Oluşturur
        /// </summary>
        public IActionResult Create(CampaingCreateDTO campaingCreateDTO)
        {
            var result = _service.Create(campaingCreateDTO);

            return HandleResult(result,201); 
        }
        /// <summary>
        /// Kampanya Durum Günceller
        /// </summary>
        [SwaggerOperation(Summary = "Kampanyanın Durumunu Günceller")]
        [HttpPost("UpdateStatus")]
        public void UpdateStatus(CampaingUpdateDto campaingUpdateDto)
        {
            _service.UpdateStatus(campaingUpdateDto.Id, campaingUpdateDto.Status);
        }
        /// <summary>
        /// Kampanya Durum Günceller
        /// </summary>
        [SwaggerOperation(Summary = "Kampanyaların Hareketlerini İzleyebilirsiniz")]
        [HttpPost("GetCampaingLog")]
        public IActionResult GetCampaingMovementLog(int Id)
        { 
            var result = _service.CampaingMovementLog(Id);
            return HandleResult(result,200);
        }
    }
}
