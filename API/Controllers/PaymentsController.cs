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
         private const string WhSecret = "";
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
           _logger = logger;
            _paymentService = paymentService;
        }

        [Authorize]
        [HttpPost("{appointmentId}")]
        public async Task<ActionResult<PatientAppointment>> CreateOrUpdatePaymentIntent(string appointmentId)
        {
            return await _paymentService.CreateOrUpdatePaymentIntent(appointmentId);

        }

         [HttpPost("webhook")]
        public async Task<ActionResult> StripeWebhook()
        {
            var json = await new StreamReader(Request.Body).ReadToEndAsync();

            var stripeEvent = EventUtility.ConstructEvent(json,
                Request.Headers["Stripe-Signature"], WhSecret);

            PaymentIntent intent;
            Booking booking;

            switch (stripeEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment succeeded: ", intent.Id);
                    booking = await _paymentService.UpdateBookingPaymentSucceeded(intent.Id);
                    _logger.LogInformation("Booking updated to payment received: ", booking.Id);
                    break;
                case "payment_intent.payment_failed":
                    intent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment failed: ", intent.Id);
                    booking = await _paymentService.UpdateBookingPaymentFailed(intent.Id);
                    _logger.LogInformation("Booking updated to payment failed: ", booking.Id);
                    break;
            }

            return new EmptyResult();
        }
    
    }
}