namespace PetClinic.Startup.Specs
{
    using Application.Appointments.Commands.Update;
    using Infrastructure.Common.Persistence;
    using MyTested.AspNetCore.Mvc;
    using System;
    using Web.Controllers;
    using Xunit;

    public class AppointmentsControllerSpecs
    {
        //[Theory]
        //[InlineData(
        //    TestData.AppointmentId,
        //    TestData.StartDate,
        //    TestData.EndDate)]
        //public void UpdateAppointmentShouldReturnOk(int appointmentId, string startDate, string endDate)
        //    => MyPipeline
        //        .Configuration()
        //        .ShouldMap(request => request
        //            .WithLocation("/Appointments/Update")
        //            .WithMethod(HttpMethod.Put)
        //            .WithJsonBody(new
        //            {
        //                AppointmentId = appointmentId,
        //                StartDate = startDate,
        //                EndDate = endDate
        //            }))

        //        .To<AppointmentsController>(c => c.Update(new UpdateAppointmentCommand 
        //        { 
        //            AppointmentId = appointmentId,
        //            StartDate = DateTime.Parse(startDate),
        //            EndDate = DateTime.Parse(endDate)
        //        }))

        //        .Which()
        //        .ShouldReturn()
        //        .ActionResult();

        [Theory]
        [InlineData(
           TestData.AppointmentId,
           TestData.StartDateInvalid,
           TestData.EndDateInvalid)]
        public void UpdateAppointmentWithOverlappingDatesShouldReturnBadRequest(
            int appointmentId, 
            string startDate, 
            string endDate)
            => MyController<AppointmentsController>
                .Instance(controller => controller
                    .WithData(entities => entities
                        .WithEntities<PetClinicDbContext>(TestData.TestAppointments)))

                .Calling(c => c.Update(new UpdateAppointmentCommand 
                {
                    AppointmentId = appointmentId,
                    StartDate = DateTime.Parse(startDate),
                    EndDate = DateTime.Parse(endDate)
                }))

                .ShouldReturn()
                .BadRequest();

        [Theory]
        [InlineData(
           TestData.AppointmentId,
           TestData.StartDateValid,
           TestData.EndDateValid)]
        public void UpdateAppointmentWithNonOverlappingDatesShouldReturnOk(
            int appointmentId,
            string startDate,
            string endDate)
            => MyController<AppointmentsController>
                .Instance(controller => controller
                    .WithData(entities => entities
                        .WithEntities<PetClinicDbContext>(TestData.TestAppointments)))

                .Calling(c => c.Update(new UpdateAppointmentCommand
                {
                    AppointmentId = appointmentId,
                    StartDate = DateTime.Parse(startDate),
                    EndDate = DateTime.Parse(endDate)
                }))

                .ShouldReturn()
                .Ok();

        [Theory]
        [InlineData(
           TestData.AppointmentId)]
        public void RemoveAppointmentWithValidIdShouldReturnOk(int appointmentId)
            => MyController<AppointmentsController>
                .Instance(controller => controller
                    .WithData(entities => entities
                        .WithEntities<PetClinicDbContext>(TestData.TestAppointments)))

                .Calling(c => c.Remove(appointmentId))

                .ShouldReturn()
                .Ok();

        [Theory]
        [InlineData(999)]
        public void RemoveAppointmentWithInvalidIdShouldReturnBadRequest(int appointmentId)
            => MyController<AppointmentsController>
                .Instance(controller => controller
                    .WithData(entities => entities
                        .WithEntities<PetClinicDbContext>(TestData.TestAppointments)))

                .Calling(c => c.Remove(appointmentId))

                .ShouldReturn()
                .BadRequest();
    }
}
