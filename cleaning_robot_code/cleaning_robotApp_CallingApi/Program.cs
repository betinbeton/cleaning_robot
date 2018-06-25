using cleaning_robotApp_CallingApi.Class;
using System;
using System.IO;

namespace cleaning_robotApp_CallingApi
{
    class Program
    {
        static void Main(string[] args)
        {

            //New configuration object
            Config config = new Config();
            //Get the URL from the appsettings
            string url = config.getConfig("url");


            try
            {
                if ((args.Length != 2))
                {
                    Console.WriteLine(config.getConfig("argsError1"));
                    Console.WriteLine(config.getConfig("argsError2"));
                    Console.WriteLine(config.getConfig("argsError3"));
                    Console.WriteLine(config.getConfig("argsError4"));
                    Console.WriteLine(config.getConfig("argsError5"));

                }
                else
                {
                    //Get the current folder 
                    string currentFolderPath = Directory.GetCurrentDirectory() + @"\";
                    //file name of the json
                    string nameFile = args[0];
                    string completeFilePath = currentFolderPath + nameFile;

                    //validate that the file exist in the current folder
                    if (File.Exists(completeFilePath))
                    {
                        //Reading the Json File.
                        var readFile = new StreamReader(completeFilePath);
                        var fileJson = readFile.ReadToEnd();


                        //Make the RestApi call
                        WebCall restApiCall = new WebCall(url, fileJson);
                        Console.Write(config.getConfig("callingMsg"));
                        //Get the response
                        string jsonOutPut = restApiCall.callWebResApi();

                        //Write the json object in the file.
                        File.WriteAllText(currentFolderPath + args[1], jsonOutPut);
                    }
                    else
                    {
                        // Console.WriteLine("The robot didn't find the file of the tasks that needs to do");
                        Console.WriteLine(config.getConfig("fileErrorMsg1"));
                        Console.WriteLine(config.getConfig("fileErrorMsg2"));
                        Console.WriteLine(currentFolderPath);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(config.getConfig("generalErrorMsg"), e.ToString());
            }


        }


     


    }
}
