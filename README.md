# OrbitGroupTest
Student App Test for Orbit group

# Developed By
Miguel Angel Pulido Rodriguez

# Projects
- OrbitGroup.Api.Students : contains the .net core 2.1 API for the students app
- OrbitGroup.Api.Students.Test : contains the validation test for the API OrbitGroup.Api.Students
- OrbitGroup.FrontEnd.Students : contains the frontend for the students app, done in Angular 11

#Steps to run:
1. from VS 2017, clone the repository https://github.com/MiguelPulido/OrbitGroupTest.git
2. Open the solution OrbitGroup

For the API

3. in project OrbitGroup.Api.Students, in "appsettings.json", modify the conection string of the database in the variable "StudentsConectionString"
4. compile the solution
5. run the project OrbitGroup.Api.Students

For the Frontend

6. from the command line, navigate to the folder FrontEndStudents located in project OrbitGroup.FrontEnd.Students
7. run the command "npm install", this will download all libraries required to run the Angular project.
8. in file "src\environments\environment.ts" modify the variable "baseUrl" to match with the IP and port of the students API
eg: 
baseUrl:"http://localhost:50078"
9. from the command line, run the following command to run the angular application:
"ng serve -o"
Once it finish loading you will be redirect to the following page:
"
http://localhost:4200/students
"

