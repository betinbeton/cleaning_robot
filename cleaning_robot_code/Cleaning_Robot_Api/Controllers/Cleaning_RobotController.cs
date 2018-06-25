using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using Cleaning_Robot_Api.Service;
//Here we call the Cleaning Robot Lib is where is all the logic of the cleaning robot.
using Cleaning_Robot_Lib;

namespace Cleaning_Robot_Api.Controllers
{
    
    
    public class Cleaning_RobotController : Controller
    {
        private IConfigMessage _configMessage;

        /// <summary>
        /// We create a constructor that receive the configuration of the appsettings
        /// </summary>
        /// <param name="configMessage">configuration that is in appsetting</param>
        public Cleaning_RobotController(IConfigMessage configMessage)
        {
            _configMessage = configMessage;
        }

        /// <summary>
        /// This us uses to see is the API is online
        /// </summary>
        /// <returns>Hi from the API and from the robot</returns>
        public IActionResult Index()
        {
            //this is the key that is on the appsetting to say Hi
            string msgRobotSayHi = "msgRobotSayHi";
            return Content(_configMessage.getConfigurationMessage(msgRobotSayHi));
        }

        /// <summary>
        /// With this method the robot can start cleaning
        /// Cleaning is Fun the robot say :)
        /// </summary>
        /// <param name="jsonInput"></param>
        /// <returns>output of the robot, what does the robot did</returns>
        [HttpPost]
        public string start([FromBody] string jsonInput)
        {
            //We recive the text json and we deserialize the object
            Input input = JsonConvert.DeserializeObject<Input>(jsonInput);
            //Object output, is what the robot did.
            Output output = new Output();
            //New object robot, ready to start some task? :p
            Robot robot = new Robot(input);
            //Robot please start cleaning.  :)
            output = robot.start();
            //what did you did robot. Did you perform the task that the input say to you ;)
            string jsonOutPut = JsonConvert.SerializeObject(output, Formatting.Indented);
            return  jsonOutPut;
        }

    }
}
