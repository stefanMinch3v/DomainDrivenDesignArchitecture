namespace PetClinic.Domain.Appointments.Factories
{
    using Appointments.Models;
    using Common.Exceptions;
    using Common.SharedKernel;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal class AppointmentFactory : IAppointmentFactory
    {
        private AppointmentDate appointmentDate = default!;
        private Client client = default!;
        private Doctor doctor = default!;
        private OfficeRoom officeRoom = default!;

        public Appointment Build()
        {
            if (this.appointmentDate is null
                || this.client is null
                || this.doctor is null
                || this.officeRoom is null)
            {
                throw new InvalidAppointmentException("Invalid appointment input.");
            }

            return new Appointment(this.doctor, this.client, this.appointmentDate, this.officeRoom);
        }

        public IAppointmentFactory WithAppointmentDate(DateTime startDate, DateTime endDate)
        {
            this.appointmentDate = new AppointmentDate(startDate, endDate);
            return this;
        }

        public IAppointmentFactory WithClient(Action<ClientFactory> client)
        {
            var clientFactory = new ClientFactory();
            client(clientFactory);
            clientFactory.Build();

            return this;
        }

        public IAppointmentFactory WithDoctor(Action<DoctorFactory> doctor)
        {
            var doctorFactory = new DoctorFactory();
            doctor(doctorFactory);
            doctorFactory.Build();

            return this;
        }

        public IAppointmentFactory WithOfficeRoom(int number, OfficeRoomType officeRoomType)
        {
            this.officeRoom = new OfficeRoom(false, number, officeRoomType);
            return this;
        }

        // for inner factories expression parsing
        private void ParseExpression(Expression expression, string key, IDictionary<string, string> dict)
        {
            // expression starts here in type lambda
            if (expression.NodeType == ExpressionType.Lambda)
            {
                var lambdaExpression = (LambdaExpression)expression;
                foreach (var parameter in lambdaExpression.Parameters)
                {
                    ParseExpression(parameter, string.Empty, dict);
                }

                var body = lambdaExpression.Body;
                ParseExpression(body, string.Empty, dict);
            }
            // here we get the actual parameter of the method
            else if (expression.NodeType == ExpressionType.Constant)
            {
                var constantExpression = (ConstantExpression)expression;
                var value = constantExpression.Value;

                // set the method value: for instance WithName("Gosho") so we add Gosho
                dict[key] = value.ToString()!;
            }
            // here we get the name of the method
            else if (expression.NodeType == ExpressionType.Call)
            {
                var methodCallExpression = (MethodCallExpression)expression;

                // set the method name: for instance WithName
                if (!dict.ContainsKey(methodCallExpression.Method.Name))
                {
                    dict.Add(methodCallExpression.Method.Name, string.Empty);
                }

                ParseExpression(methodCallExpression.Object, methodCallExpression.Method.Name, dict);

                foreach (var argument in methodCallExpression.Arguments)
                {
                    ParseExpression(argument, methodCallExpression.Method.Name, dict);
                }
            }
        }
    }
}
