using Core.Utilities.Results;
using Entities.DTOs.ReqModels.Appointment;
using Entities.DTOs.ResModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAppointmentService
    {
        IResult Add(AppointmentCreateDto appointmentCreateDto);
    }
}
