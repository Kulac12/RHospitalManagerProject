using Business.Abstract;
using Business.Concrete;
using Business.Constants;
using DataAccess.Abstract;
using Entities.DTOs.ReqModels.Appointment;
using Entities.DTOs.ResModels;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpPost("add")]
        public ActionResult AddAppointment(AppointmentCreateDto appointmentCreateDto)
        {
            var result = _appointmentService.Add(appointmentCreateDto);  // Servis metodu ile yeni poliklinik ekliyoruz
            if (result.Success)
            {
                return Ok(Messages.AppointmentAdded);
            }

            return BadRequest(result.Message);
        }
    }
}
