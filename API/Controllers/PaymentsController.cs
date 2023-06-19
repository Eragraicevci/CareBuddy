using API.Errors;
using Core.Entities;
using Core.Entities.BookingAggregate;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace API.Controllers
{
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentService _paymentService;
        public PaymentsController(IPaymentService paymentService)
        {
          //  _logger = logger;
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("{appointmentId}")]
        public async Task<ActionResult<PatientAppointment>> CreateOrUpdatePaymentIntent(string appointmentId)
        {
            return await _paymentService.CreateOrUpdatePaymentIntent(appointmentId);
        }
    }
}