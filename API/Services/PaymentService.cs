using System;
using API;
using Core.Entities;
using Core.Entities.BookingAggregate;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using Service = Core.Entities.Service;

namespace Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IConfiguration _config;

        public PaymentService(
            IUnitOfWork unitOfWork,
            IAppointmentRepository appointmentRepository,
            IConfiguration config
        )
        {
            _config = config;
            _appointmentRepository = appointmentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PatientAppointment> CreateOrUpdatePaymentIntent(string appointmentId)
        {
            StripeConfiguration.ApiKey = _config["StripeSettings:SecretKey"];

            var appointment = await _appointmentRepository.GetAppointmentAsync(appointmentId);
            // if (appointment == null) return null;

            var appointmentTypePrice = 0m;

            if (appointment.AppointmentTypeId.HasValue)
            {
                var appointmentType = await _unitOfWork
                    .Repository<AppointmentType>()
                    .GetByIdAsync((int)appointment.AppointmentTypeId);
                appointmentTypePrice = appointmentType.Price;
            }

            foreach (var item in appointment.Items)
            {
                var serviceItem = await _unitOfWork.Repository<Service>().GetByIdAsync(item.Id);
                if (item.Price != serviceItem.Price)
                {
                    item.Price = serviceItem.Price;
                }
            }

            var service = new PaymentIntentService();

            PaymentIntent intent;

            if (string.IsNullOrEmpty(appointment.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount =
                        (long)appointment.Items.Sum(i => i.Capacity * (i.Price * 100))
                        + (long)appointmentTypePrice * 100,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                intent = await service.CreateAsync(options);
                appointment.PaymentIntentId = intent.Id;
                appointment.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount =
                        (long)appointment.Items.Sum(i => i.Capacity * (i.Price * 100))
                        + (long)appointmentTypePrice * 100
                };
                await service.UpdateAsync(appointment.PaymentIntentId, options);
            }
            await _appointmentRepository.UpdateAppointmentAsync(appointment);

            return appointment;
        }

        public Task<Booking> UpdateBookingPaymentFailed(string paymentIntentId)
        {
            throw new NotImplementedException();
        }

        public Task<Booking> UpdateBookingPaymentSucceeded(string paymentIntentId)
        {
            throw new NotImplementedException();
        }
    }
}
