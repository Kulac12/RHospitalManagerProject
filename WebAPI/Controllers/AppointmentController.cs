using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

   
        [HttpPost("appointment/add")]
        public ActionResult AppointmentCreate(AppointmentCreateDto appointmentCreateDto)
        {
            //  seçilen tarihte ve saatte kişinin başka randevusu var mı? Varsa randevu alamaz. 
            //seçilen tarih ve saatte ilgili poliklinik ve doktor uygun mu? değilse randevu alınamaz (hatta direk listelenmemeli de  ama o kısma gelmediğimiz için şu anda bu şekilde kontrol sağlayalım.)

            //appointmentService.AppointmentCreate yazılıp ilgili kayıt yapılmalı ve gerekli tablolara kayıt atılmalı. 
        }
    }
}
