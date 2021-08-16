# Project objective

The purpose of this code is to show my c# programing skills. This was a test that I did for an interview process and I enjoyed. That why im using this test project to show my C# development skills using all the good stuff that I know, classes, object oriented design, MVC for a rest web service and more cool things.


# Project requirement
 
[View Project Requirement](https://github.com/betinbeton/cleaning_robot/wiki/Project-requirement)  


# Project methodology

This project was created using scrum methodology. I create all the tasks that need to be done and create my custom scrum board having: things to be done, testing and done area.




# Folder description

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h2.JPG)

### Cleaning_robot_code

This folder had all the code that was used.

### Cleaning_robot_console

This is the final application for the testing code. This application runs using de cmd of
windows

Instruction

dotnet cleaning_robot.dll source.json result.json

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h3.JPG)

In this folder need to exist the source.json and the result is a new file in the folder
result.json

### cleaning_robot_console_CallingApi

Release console project for coding test using an API. This application ran be run using de
cmd of windows

dotnet cleaning_robotApp_CallingApi.dll source.json result.json

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h4.JPG)

Notes:
1-

First you need to check for the URL and see that is the correct one, this can be found in the
appsettings.json

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h5.JPG)

You only need to change the http://localhost:38984 in case is using a other port number
or url

2-

Publishing the Cleaning robot Api,

You need to copy the folder to the iisexpress folder or virtual directory (please, do not
include spaces in the path).

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h6.JPG)

I create a powershell to start the iisexpress using the path of the folder that you want. You
only need to configure where is the iisexpress and the path of the cleaning_robot_api

Open the PowerShellToStartCleaning_Robot_API.ps1

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h7.JPG)

Make sure that the iisExpress is in "c:\Program Files (x86)\IIS Express\iisexpress.exe" if not
please change where you have the exe.

You can change the port, but if you change it, please change in the appsettings.json that’s
call the api.

Change the path where api is going to be found, for example I copy the folder to
D:\cleaning_robot_Api and change the path
When you run the powershell a cmd will open and you can see that is working the api.

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h8.JPG)

To do a quick test that is working the api you can go to http://localhost:38984/ and you
will see a welcome message: Robot say: Welcome Friend :), ready to clean

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h9.JPG)

### cleaning_RobotQA

This folder is for QA. This folder is the console application to do the testing with normal
call and api call.

This project will read all the files that are inside of the folder QA_Cleaning_Robot, you can
configure another folder in the appsettings.json

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h10.JPG)

Please use the \\ to change folder.

### QA_Results

This folder includes the testing that I did in the time that I have and the results. I just copy
the results from the application cleaning_RobotQA.

### Xml_ documentation

This folder includes the class libray documentation in xml format that VS2017 generated.

### PowerShellToStartCleaning_Robot_API.ps1

Is a powershell to start iisexpress using the cleaning_robot_Api project

# Requisites

IIS Express
.net Core 2.*
This project is done using Visual Studio 2017

# Project

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h11.JPG)


### Cleaning_robot
Contains the code of a console application using the cleaning robot lib.

### Cleaning_Robot_api
Contains the code of a rest full api, this code also use the cleaning robot lib.

### Cleaning_Robot_Lib
This is a class library application that contains all the logic of the cleaning robot algorithm. All the
classes and objects that are used in the other application depend on this project.

### Cleaning_robot QA
Contains the code of a console application that calls the cleaning robot lib and the cleaning robot
api. The propose of this project is to loop and get all the testing files in a specific folder and test
the robot lib and robot api. This is a testing project.

### Building
To build the solution, open the solution file that is located on the cleaning_robot_code the name
of the file is cleaning_robot.sln

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h12.JPG)

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h13.JPG)

Note: for the first time, please do a rebuild solution and then a build solution so all the files that
are needed in the different project can be refreshed.

1 – Rebuild Solution

2- Build Solution

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h14.JPG)


