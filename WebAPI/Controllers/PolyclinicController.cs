using Business.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PolyclinicController : ControllerBase
    {
        private readonly IPolyclinicService _polyclinicService;

        public PolyclinicController(IPolyclinicService polyclinicService)
        {
            _polyclinicService = polyclinicService;
        }
        [HttpPost("add")]
        public IActionResult AddPolyclinic(PolyclinicDetailDto polyclinicDto)
        {
            var polyclinic = new Polyclinic
            {
                PoliclinicName = polyclinicDto.PoliclinicName,
                PoliclinicDescription = polyclinicDto.PoliclinicDescription
            };

            _polyclinicService.Add(polyclinic);
            return Ok(new { message = "Poliklinik başarıyla eklendi.", data = polyclinic });
        }

        [HttpGet("getAll")]
        public IActionResult GetAllPolyclinics()
        {
            var polyclinics = _polyclinicService.GetAll();
            return Ok(polyclinics);
        }


    }
}
