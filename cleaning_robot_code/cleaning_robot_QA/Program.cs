using System;
using System.IO;
using Newtonsoft.Json;
using Cleaning_Robot_Lib;
using cleaning_robot_QA.Class;

namespace cleaning_robot_QA
{
    class Program
    {
        static void Main(string[] args)
        {
            //New configuration object
            Config config = new Config();

            //path where are the file to process
            string pathQA = config.getConfig("pathFolderQA");
            string url = config.getConfig("url");

            if (Directory.Exists(pathQA))
                {
                    // This path is a directory
                    ProcessDirectory(pathQA, url);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory to test", pathQA);
                }
   


        }

        /// <summary>
        /// Process all files in the directory passed in, recurse on any directories 
        /// that are found, and process the files they contain.
        /// </summary>
        /// <param name="targetDirectory"></param>
        /// <param name="url"></param>
        public static void ProcessDirectory(string targetDirectory, string url)
        {
            int count = 1;
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                ProcessFilesTest(fileName, count);
                ProcessApiTest(fileName, url, count);
                count++;
            }

        }

        /// <summary>
        /// Prints the procesed file, to see what is going on
        /// </summary>
        /// <param name="path"></param>
        public static void ProcessFilesTest(string fileName, int testCase )
        {
            Console.WriteLine("Processing from file  '{0}'.", fileName);

            string completeFilePath = fileName;

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
            File.WriteAllText(completeFilePath.Replace(".", "TestCase"+testCase.ToString()+"."), jsonOutPut);


            Console.WriteLine("Processed file '{0}'.", fileName);
        }

        public static void ProcessApiTest(string fileName, string url, int testCase)
        {
            Console.WriteLine("Processing from API  '{0}'.", fileName);

            string completeFilePath = fileName;


            //Reading the Json File.
            var readFile = new StreamReader(completeFilePath);
            var fileJson = readFile.ReadToEnd();


            //Make the RestApi call
            WebCall restApiCall = new WebCall(url, fileJson);
            //Get the response
            string jsonOutPut = restApiCall.callWebResApi();
            //Write the json object in the file.
            File.WriteAllText(completeFilePath.Replace(".", "ApiTestCase" + testCase.ToString() + "."), jsonOutPut);

            Console.WriteLine("Processed from API  '{0}'.", fileName);
        }




    }
}
