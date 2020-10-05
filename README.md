# Domain Driven Design + Clean Architecture
# Domain-Driven Design with ASP.NET Core Microservices @SoftUni - August 2020

![Home](https://i.imgur.com/W4xqEl0.jpg)

## Project description

Simple pet clinic application for managing appointments and operations.
There are two roles: Client and Doctor.
Four bounded contexts:
- Identity
- Adoptions
- Appointments
- MedicalRecords

Identity context contains the doctor/client registrations.

Adoptions context has the following functionality:
- All pets available for adoption
- Details
- Add new pet
- Adopt pet (which is available only for clients)

Appointments has:
- List of all for the current member
- Make new as client
- Make new as doctor
- Update date
- Remove

Medical records bounded context consists of:
- List of all clients
- List of all doctors
- Client details
- Doctor details
- Add diagnose - for pets
- Add new pet (to client)

### Technologies and tools used:
- ASP.NET Core + EF Core 3.1
- FluentValidation
- MediatR + CQRS
- AutoMapper
- Bogus
- FakeItEasy
- Fluent Assertions
- JwtBearer
- Scrutor
- Swagger
- Moq
- Shouldly
