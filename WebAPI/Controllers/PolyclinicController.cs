using Business.Abstract;
using Business.Constants;
using Entities.DTOs.ResModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PolyclinicController : Controller
    {
        private IPolyclinicService _polyclinicService;

        public PolyclinicController(IPolyclinicService polyclinicService)
        {
            _polyclinicService = polyclinicService;
        }

      
        [HttpPost ("add")]
        public  ActionResult AddPolyclinic( PolyclinicDto polyclinicDto)
        {

            var result =  _polyclinicService.Add(polyclinicDto);  // Servis metodu ile yeni poliklinik ekliyoruz
            if (result.Success)
            {
                return Ok(Messages.AddPolyclinic);  
            }

            return BadRequest(result.Message);  
        }
    

    }
}