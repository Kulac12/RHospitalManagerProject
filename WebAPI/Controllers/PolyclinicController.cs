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
        public IActionResult AddPolyclinic(PolyclinicDetailDto polyclinicDetailDto)
        {
            var polyclinicExists = _polyclinicService.PolyclinicExistByName(polyclinicDetailDto.PoliclinicName);

            if (polyclinicExists)
            {
                return BadRequest(new { message = "Bu isimde bir poliklinik zaten mevcut." });
            }
            var polyclinic = new Polyclinic
            {
                PoliclinicName = polyclinicDetailDto.PoliclinicName,
                PoliclinicDescription = polyclinicDetailDto.PoliclinicDescription
            };

            _polyclinicService.Add(polyclinic);
            return Ok(new { message = "Poliklinik başarıyla eklendi.", data = polyclinic });
        }

        [HttpGet("getAll")]
        public IActionResult GetAllPolyclinics()
        {
            var polyclinics = _polyclinicService.GetAllPolyclinics();
            return Ok(polyclinics);
        }

        //[HttpPost("delete/{polyclinicId}")]
        //public IActionResult SoftDeletePolyclinic(Guid polyclinicId )
        //{
        //    var polyclinic = _polyclinicService.PolyclinicExistById();
        //}
    }
}
