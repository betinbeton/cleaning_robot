# Project objective

The purpose of this code is to show my c# programing skills. This was a test that I did for an interview process and I enjoyed. That why im using this test project to show my C# development skills using all the good stuff that I know, classes, object oriented design, MVC for a rest web service and more cool things.


# Project requirement
 
[View Project Requirement](https://github.com/betinbeton/cleaning_robot/wiki/Project-requirement)  


# Project methodology

This project was created using scrum methodology. I create all the tasks that need to be done and create my custom scrum board having: things to be done, testing and done area.

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h1.JPG)


# Folder description

![](https://github.com/betinbeton/cleaning_robot/blob/master/xml_documentation/img/h2.JPG)

### Cleaning_robot_code

This folder had all the code that was used.

###Cleaning_robot_console

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

