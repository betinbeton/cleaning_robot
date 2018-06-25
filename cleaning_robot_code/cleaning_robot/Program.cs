using System;
using Newtonsoft.Json;
using Cleaning_Robot_Lib;
using System.IO;
using cleaning_robot.Class;

namespace cleaning_robot
{
    class Program
    {
        static void Main(string[] args)
        {
            //New configuration object
            Config config = new Config();

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

                        //Deserialize of the json object and passing to a object input for the robot
                        Input input = JsonConvert.DeserializeObject<Input>(fileJson);
                        //Object output, is what the robot did.
                        Output output = new Output();
                        //New object robot, ready to start some task? :p
                        Robot robot = new Robot(input);
                        //Robot please start cleaning.  :)
                        output = robot.start();
                        //what did you did robot. Did you perform the task that the input say to you ;)
                        string jsonOutPut = JsonConvert.SerializeObject(output, Formatting.Indented);
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
