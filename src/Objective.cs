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
                    } else {
                        _locationName = "instreet5";
                        _description = "Finally, locate and travel";
                        _description2 = "to the SPS Building";
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
                    _locationName = "ATC Building";
                    _description = "Locate and travel to the";
                    _description2 = "ATC Building";
                    break;
                case 1:
                    _locationName = "AS Building";
                    _description = "Locate and travel to the";
                    _description2 = "AS Building";
                    break;
                case 2:
                    _locationName = "study";
                    _description = "Locate and travel to the";
                    _description2 = "Study";
                    break;
            }
        }

        public static void RandomiseSecondObjective() {
            Random rand = new Random();
            int randomNumber = rand.Next(0, 3);

            switch (randomNumber) {
                case 0:
                    _locationName = "TA Building";
                    _description = "Locate and travel to the";
                    _description2 = "TA Building";
                    break;
                case 1:
                    _locationName = "TB Building";
                    _description = "Locate and travel to the";
                    _description2 = "TB Building";
                    break;
                case 2:
                    _locationName = "TD Building";
                    _description = "Locate and travel to the";
                    _description2 = "TD Building";
                    break;
            }
        }

        public static void RandomiseThirdObjective() {
            Random rand = new Random();
            int randomNumber = rand.Next(0, 3);

            switch (randomNumber) {
                case 0:
                    _locationName = "EN Building";
                    _description = "Locate and travel to the";
                    _description2 = "EN Building";
                    break;
                case 1:
                    _locationName = "AMDC Building";
                    _description = "Locate and travel to the";
                    _description2 = "AMDC Building";
                    break;
                case 2:
                    _locationName = "TC Building";
                    _description = "Locate and travel to the";
                    _description2 = "TC Building";
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
                    _locationName = "AR Building";
                    _description = "Locate and travel to the";
                    _description2 = "AR Building.";
                    break;
                case 2:
                    _locationName = "AGSE Building";
                    _description = "Locate and travel to the";
                    _description2 = "AGSE Building";
                    break;
            }
        }

        public static void RandomiseFifthObjective() {
            Random rand = new Random();
            int randomNumber = rand.Next(0, 3);

            switch (randomNumber) {
                case 0:
                    _locationName = "AD Building";
                    _description = "Locate and travel to the";
                    _description2 = "AD Building";
                    break;
                case 1:
                    _locationName = "George Building";
                    _description = "Locate and travel to the";
                    _description2 = "George Building";
                    break;
                case 2:
                    _locationName = "FS Building";
                    _description = "Locate and travel to the";
                    _description2 = "FS Building";
                    break;
            }
        }

        //Currently unsued - may or may not be used in the future
        public void Draw() {
            GameController.gameWindow.DrawText(_description, Color.DarkRed, 200, 200);
        }
    }
}
