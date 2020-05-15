using SplashKitSDK;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swinburneexplorer {
    public class Objective : IDraw {
        private static bool _isComplete;
        private static string _description;
        private static string _description2;
        private static string _locationName;

        public Objective(int objectiveNumber) {
            _isComplete = false;
            AssignLocation(objectiveNumber);
        }

        public string Description {
            get {
                return _description;
            }
        }

        public string Description2 {
            get {
                return _description2;
            }
        }

        public static void AssignLocation(int objectiveNumber) {
            switch (objectiveNumber) {
                case 1:
                    RandomiseFirstObjective();
                    break;

                case 2:
                    RandomiseSecondObjective();
                    break;

                case 3:
                    RandomiseThirdObjective();
                    break;

                case 4:
                    RandomiseFourthObjective();
                    break;

                case 5:
                    RandomiseFifthObjective();
                    break;

                case 6:
                    if (GameController._player.StartingLocation == "Train") {
                        _locationName = "Train";
                        _description = "Finally, locate and travel";
                        _description2 = "to the train station";
                    } 
                    else if (GameController._player.StartingLocation == "instreet5") {
                        _locationName = "instreet5";
                        _description = "Finally, locate and travel";
                        _description2 = "to the SPS Building";
                    } 
                    else {
                        _locationName = "studentCarPark";
                        _description = "Finally, locate and travel";
                        _description2 = "to the Student Car Park";
                    }
                    break;
                default:
                    _locationName = "None";
                    _description = "Explore anywhere in Swinburne";
                    _description2 = "you have finished the game";
                    break; 
            }
        }

        public bool CheckIfObjectiveIsComplete(string locationName) {
            if (_locationName == locationName) {
                _isComplete = true;
            }
            return _isComplete;
        }

        public static void RandomiseFirstObjective() {
            Random rand = new Random();
            int randomNumber = rand.Next(0, 3);

            switch (randomNumber) {
                case 0:
                    _locationName = "ATC 501";
                    _description = "Locate and travel to";
                    _description2 = "ATC 501";
                    break;
                case 1:
                    _locationName = "AS 301";
                    _description = "Locate and travel to";
                    _description2 = "AS 301";
                    break;
                case 2:
                    _locationName = "study 101";
                    _description = "Locate and travel to";
                    _description2 = "Study 101";
                    break;
            }
        }

        public static void RandomiseSecondObjective() {
            Random rand = new Random();
            int randomNumber = rand.Next(0, 3);

            switch (randomNumber) {
                case 0:
                    _locationName = "TA 101";
                    _description = "Locate and travel to";
                    _description2 = "TA 101";
                    break;
                case 1:
                    _locationName = "TB 101";
                    _description = "Locate and travel to";
                    _description2 = "TB 101";
                    break;
                case 2:
                    _locationName = "TD 101";
                    _description = "Locate and travel to";
                    _description2 = "TD 101";
                    break;
            }
        }

        public static void RandomiseThirdObjective() {
            Random rand = new Random();
            int randomNumber = rand.Next(0, 3);

            switch (randomNumber) {
                case 0:
                    _locationName = "EN 701";
                    _description = "Locate and travel to";
                    _description2 = "EN 701";
                    break;
                case 1:
                    _locationName = "AMDC 301";
                    _description = "Locate and travel to";
                    _description2 = "AMDC 301";
                    break;
                case 2:
                    _locationName = "TC 101";
                    _description = "Locate and travel to";
                    _description2 = "TC 101";
                    break;
            }
        }

        public static void RandomiseFourthObjective() {
            Random rand = new Random();
            int randomNumber = rand.Next(0, 3);

            switch (randomNumber) {
                case 0:
                    _locationName = "studentCarPark";
                    _description = "Locate and travel to the";
                    _description2 = "Student Car Park";
                    break;
                case 1:
                    _locationName = "AR 101";
                    _description = "Locate and travel to";
                    _description2 = "AR 101.";
                    break;
                case 2:
                    _locationName = "AGSE 201";
                    _description = "Locate and travel to";
                    _description2 = "AGSE 201";
                    break;
            }
        }

        public static void RandomiseFifthObjective() {
            Random rand = new Random();
            int randomNumber = rand.Next(0, 3);

            switch (randomNumber) {
                case 0:
                    _locationName = "AD 101";
                    _description = "Locate and travel to";
                    _description2 = "AD 101";
                    break;
                case 1:
                    _locationName = "George 201";
                    _description = "Locate and travel to";
                    _description2 = "George 201";
                    break;
                case 2:
                    _locationName = "FS 101";
                    _description = "Locate and travel to";
                    _description2 = "FS 101";
                    break;
            }
        }

        public string LocationName {
            get {
                return _locationName;
            }
        }

        //Currently unsued - may or may not be used in the future
        public void Draw() {
            GameController.gameWindow.DrawText(_description, Color.DarkRed, 200, 200);
        }
    }
}
