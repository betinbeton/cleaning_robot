using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Cleaning_Robot_Lib
{
    /// <summary>
    /// This class is use to play with the position of the robot.
    /// Be happy =)
    /// </summary>
    class Position
    {
        /// <summary>
        /// This method is use to change the position of the robot 
        /// using the new x axis or new y axis to move
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="facing">where is the robot and where is facing</param>
        /// <param name="newX">new x axis to move</param>
        /// <param name="newY">new y axis to move</param>
        /// <returns></returns>
        public List<Map> changePosition(List<Map> map, string facing, int newX, int newY)
        {
            foreach (var position in map)
            {
                if ((position.x == newX) && (position.y == newY))
                {
                    position.facing = facing;
                    position.visited = true;
                }
            }
            return map;
        }

        /// <summary>
        /// Save where the robot when out of battery
        /// we dont use but i was playing with it:)
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <returns>where the robot dies</returns>
        public List<Map> outBatteryPosition(List<Map> map, int x, int y)
        {
            foreach (var position in map)
            {
                if ((position.x == x) && (position.y == y))
                {
                    position.outBattery = true;

                }
            }
            return map;
        }

        /// <summary>
        ///This method flag the space as cleaned 
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <returns>the space cleaned</returns>
        public List<Map> cleanPosition(List<Map> map, int x, int y)
        {
            foreach (var position in map)
            {
                if ((position.x == x) && (position.y == y))
                {
                    position.cleaned = true;
                }
            }
            return map;
        }

        // <summary>
        /// This method is use to advance one space in the map depending of the facing orientation
        /// </summary>
        /// <param name="map">The map of the robot</param>
        /// <param name="facing">Where is facing the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <param name="maxRow">The max value in the row, after that is wall</param>
        /// <param name="maxColumn">The max value in the column, after that is wall</param>
        /// <param name="attemp">Number of attempts to move</param>
        /// <param name="baterry">The object battery</param>
        /// <returns>New Map with the new position of the robot</returns>
        public List<Map> advancePosition(List<Map> map, string facing, int x, int y, int maxRow, int maxColumn, int attemp, Batterry baterry)
        {



            Position robot = new Position();
            Obstacle obt = new Obstacle();
            Barrier barrier = new Barrier();
            string command = Commads.cmds.A.ToString();
            //we need to see what is the cost of doing the instruction
            int costOperation = baterry.getBatteryDrain(command);
            //  List<Map> validateMap = new List<Map>();

            if (Orientation.direction.N.ToString() == facing)
            {
                // y -1 becouse is facing nort so in the array the Y to move up we need to decrease 
                int newY = y - 1;
                //get the next space. this is to check what is in the new space that we are going to move
                string space = checkNextSpaceContain(map, x, newY);

                //If exist a wall(out of the matrix)
                if (newY < 0)
                {
                    //becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    //we call the obstacle method to handle the obtacle
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                //we need to check is a berrier exist
                else if (barrier.checkForBarrier(space))
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    //if a barrier exist we call the method obstacle to handle the barrier
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);

                }
                //if we pass the conditions of no wall and no obstacle we can move the robot
                else
                {

                    //if we dont have enough battery life, we return the object and we dont move;
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //If we have enough battery life to do the instruction
                        //Move the robot logic.
                        //Move the robot
                        map = robot.changePosition(map, facing, x, newY);
                        //in this point we decrease the cost of the battery
                        baterry.batteryLife -= costOperation;
                    }
                    else
                    {
                        //The robot is out of battery life
                        map = robot.outBatteryPosition(map, x, y);
                        return map;
                    }
                }

            }
            else if (Orientation.direction.S.ToString() == facing)
            {
                // y +1 becouse is facing south so in the array the Y to move up we need to increase 
                int newY = y + 1;
                //get the next space. this is to check what is in the new space that we are going to move
                string space = checkNextSpaceContain(map, x, newY);

                //We need to check is we are going to move out of the wall
                if (newY > maxRow)
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    //is we are going to move to a wall we call the method 
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else if (barrier.checkForBarrier(space))
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else
                {
                    //Move the robot logic.
                    //if we dont have enough battery life, we return the object and we dont move;
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //Move the robot
                        map = robot.changePosition(map, facing, x, newY);
                        //in this point we decrease the cost of the battery
                        baterry.batteryLife -= costOperation;
                    }
                    else
                    {
                        //The robot is out of battery life
                        map = robot.outBatteryPosition(map, x, y);
                        return map;
                    }
                }
            }
            else if (Orientation.direction.W.ToString() == facing)
            {
                // x - 1 becouse is facing west so in the array the X to move up we need to decrease 
                int newX = x - 1;
                //get the next space. this is to check what is in the new space that we are going to move
                string space = checkNextSpaceContain(map, newX, y);
                //If exist a wall(out of the matrix)
                if (newX < 0)
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else if (barrier.checkForBarrier(space))
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    //if a barrier exist we call the method obstacle to handle the barrier
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else
                {
                    //Move the robot logic.
                    // int costOperation = baterry.getBatteryDrain(command);
                    //if we dont have enough battery life, we return the object and we dont move;
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //Move the robot
                        map = robot.changePosition(map, facing, newX, y);
                        //in this point we decrease the cost of the battery
                        baterry.batteryLife -= costOperation;
                    }
                    else
                    {
                        //The robot is out of battery life
                        map = robot.outBatteryPosition(map, x, y);
                        return map;
                    }
                }
            }
            else if (Orientation.direction.E.ToString() == facing)
            {

                // x + 1 becouse is facing west so in the array the X to move up we need to increase 
                int newX = x + 1;
                //get the next space. this is to check what is in the new space that we are going to move
                string space = checkNextSpaceContain(map, newX, y);
                //We need to check is we are going to move out of the wall
                if (newX > maxRow)
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else if (barrier.checkForBarrier(space))
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else
                {
                    //Move the robot logic.
                    // int costOperation = baterry.getBatteryDrain(command);
                    //if we dont have enough battery life, we return the object and we dont move;
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //Move the robot
                        map = robot.changePosition(map, facing, newX, y);
                        //in this point we decrease the cost of the battery
                        baterry.batteryLife -= costOperation;

                    }
                    else
                    {
                        //The robot is out of battery life
                        map = robot.outBatteryPosition(map, x, y);
                        return map;
                    }
                }

            }
            return map;
        }

        /// <summary>
        /// This method is use to move back one space in the map
        /// </summary>
        /// <param name="map">The map of the robot</param>
        /// <param name="facing">Where is facing the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <param name="maxRow">The max value in the row, after that is wall</param>
        /// <param name="maxColumn">The max value in the column, after that is wall</param>
        /// <param name="attemp">Number of attempts to move</param>
        /// <param name="baterry">The object battery</param>
        /// <returns>New Map with the new position of the robot</returns>
        public List<Map> backPosition(List<Map> map, string facing, int x, int y, int maxRow, int maxColumn, int attemp, Batterry baterry)
        {
            Position robot = new Position();
            Obstacle obt = new Obstacle();
            Barrier barrier = new Barrier();
            string command = Commads.cmds.B.ToString();
            //we need to see what is the cost of doing the instruction
            int costOperation = baterry.getBatteryDrain(command);

            if (Orientation.direction.N.ToString() == facing)
            {
                // y +1 becouse is facing nort so in the array the Y to move back we need to increase 
                int newY = y + 1;
                //get the next space. this is to check what is in the new space that we are going to move
                string space = checkNextSpaceContain(map, x, newY);
                //We need to check is we are going to move out of the wall
                if (newY > maxRow)
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    //we call the obstacle method to handle the obtacle
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                //we need to check is a berrier exist
                else if (barrier.checkForBarrier(space))
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    //if a barrier exist we call the method obstacle to handle the barrier
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                //if we pass the conditions of no wall and no obstacle we can move the robot
                else
                {
                    //if we dont have enough battery life, we return the object and we dont move;
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //If we have enough battery life to do the instruction
                        //Move the robot logic.
                        //Move the robot
                        map = robot.changePosition(map, facing, x, newY);
                        //in this point we decrease the cost of the battery
                        baterry.batteryLife -= costOperation;
                    }
                    else
                    {
                        //The robot is out of battery life
                        map = robot.outBatteryPosition(map, x, y);
                        return map;
                    }
                }
            }
            else if (Orientation.direction.S.ToString() == facing)
            {
                // y -1 becouse is facing south so in the array the Y to move back we need to decrease 
                int newY = y - 1;
                //get the next space. this is to check what is in the new space that we are going to move
                string space = checkNextSpaceContain(map, x, newY);

                //If exist a wall(out of the matrix)
                if (newY < 0)
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    //is we are going to move to a wall we call the method 
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else if (barrier.checkForBarrier(space))
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else
                {
                    //Move the robot logic.
                    //int costOperation = baterry.getBatteryDrain(command);
                    //if we dont have enough battery life, we return the object and we dont move;
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //Move the robot
                        map = robot.changePosition(map, facing, x, newY);
                        //in this point we decrease the cost of the battery
                        baterry.batteryLife -= costOperation;
                    }
                    else
                    {
                        //The robot is out of battery life
                        map = robot.outBatteryPosition(map, x, y);
                        return map;
                    }
                }
            }
            else if (Orientation.direction.W.ToString() == facing)
            {
                // x + 1 becouse is facing west so in the array the X to move up we need to increase 
                int newX = x + 1;
                //get the next space. this is to check what is in the new space that we are going to move
                string space = checkNextSpaceContain(map, newX, y);
                //If exist a wall(out of the matrix)
                if (newX > maxRow)
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else if (barrier.checkForBarrier(space))
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else
                {
                    //Move the robot logic.
                    //if we dont have enough battery life, we return the object and we dont move;
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //Move the robot
                        map = robot.changePosition(map, facing, newX, y);
                        //in this point we decrease the cost of the battery
                        baterry.batteryLife -= costOperation;
                    }
                    else
                    {
                        //The robot is out of battery life
                        map = robot.outBatteryPosition(map, x, y);
                        return map;
                    }
                }
            }
            else if (Orientation.direction.E.ToString() == facing)
            {

                // x + 1 becouse is facing west so in the array the X to move up we need to increase 
                int newX = x - 1;
                //get the next space. this is to check what is in the new space that we are going to move
                string space = checkNextSpaceContain(map, newX, y);
                //We need to check is we are going to move out of the wall
                if (newX < 0)
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else if (barrier.checkForBarrier(space))
                {
                    // becouse the robot intent to advance, the robot use battery
                    baterry.batteryLife -= costOperation;
                    map = obt.obstacle(map, facing, x, y, maxRow, maxColumn, attemp + 1, baterry);
                }
                else
                {
                    //Move the robot logic.
                    //if we dont have enough battery life, we return the object and we dont move;
                    if (baterry.checkIfCanExecute(costOperation, baterry.batteryLife))
                    {
                        //Move the robot
                        map = robot.changePosition(map, facing, newX, y);
                        //in this point we decrease the cost of the battery
                        baterry.batteryLife -= costOperation;

                    }
                    else
                    {
                        //The robot is out of battery life
                        map = robot.outBatteryPosition(map, x, y);
                        return map;
                    }
                }

            }
            return map;
        }

        /// <summary>
        /// Check what is in the space
        /// </summary>
        /// <param name="map">map of the robot</param>
        /// <param name="x">x axis</param>
        /// <param name="y">y axis</param>
        /// <returns>What is in the space</returns>
        private string checkNextSpaceContain(List<Map> map, int x, int y)
        {
            string space = string.Join("", from m in map
                                           where (m.x == x) && (m.y == y)
                                           select m.space);
            return space;
        }





    }
}
