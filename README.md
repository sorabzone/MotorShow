# MotorShow

# Requirement
 To build a service that consumes this API, and produces a list of the models of cars and the show they attended, grouped by make alphabetically.

# How to Run and Test
1. Clone Repository
2. Open in Visual Studio 2017 and execute MotorShow. It is a .Net Core WebAPI project created on top of Visual Studio boiler plate template.
3. UnitTest in project "MotorShow.UnitTest" can be run from Visual Studio,  
4. To execute UnitTest from command prompt, go to "\MotorShow\MotorShow.UnitTest" and execute following command

			dotnet test
			

# Solution Structure

MotorShow

It is a .Net Core WebAPI project created on top of Visual Studio boiler plate template.

App will always receive response in an standard format from API. If Code is 200 then the result is expected otherwise failed or exception.

			public class CustomResponse<TData>
			{
				public StatusCode Code { get; set; }
				public string Message { get; set; }
				public TData Data { get; set; }
			}


		CI/CD pipeline is also created and integrated to AppVeyor(OpenSource), to build and execute unit tests.


MotorShow.Logger

It is a common logger created using NLog. By default application is configured to write logs in text file.


MotorShow.Service

API Controller can communicate with either Service and Repository.

1. All business logic or data manipulation/modification should be handled by this layer.
2. Service should not access any data or information directly.
3. It can call different repository to get data and can combine them


MotorShow.Repository

This layer has direct access to all data sources.

1. Repository only fetch the raw data, but it doesn't modify it. It should be done by Service or sometimes in controller.
2. ServiceAPIClient is used to maintain HttpClients, so that application doesn't create it for each request.
3. All services are injected as singleton at startup.
4. If there is an invalid response from external API, then the response is null and is handled accordingly

        Note: HttpClient is configured to timeout in 3 seconds for this test application. If there is no response from WebjetAPI, then request will timeout after 3 seconds


MotorShow.UnitTest

Unit tests are created using NUnit and executed in CI pipeline.


There are several interface, extension, constant and helper classes are created to follow SOLID principles. It also helps in scaling and expanding the application in future.
