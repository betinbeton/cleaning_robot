To execute the cleaning_robot


***You need .net core 2.*

command to execute the app

dotnet cleaning_robotApp_CallingApi.dll source.json result.json


NOTE: You can change the name in the arg for example

dotnet cleaning_robotApp_CallingApi.dll test1.json result.json

dotnet cleaning_robotApp_CallingApi.dll test1.json test1result.json



CHANGE URL
you can change the URL to call the API in appsettings.json
need to change only localhost:38984

"url": "http://localhost:38984/Cleaning_Robot/start/"