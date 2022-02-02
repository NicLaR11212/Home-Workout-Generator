using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Home_Workout_Generator.Model;

namespace Home_Workout_Generator.Controller
{
    public class database_connection
    {
        //a static random variable
        private static Random _random = new Random();
        private static string database_path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Model\\data\\Exercises Database.mdf";

        //GENERATION METHODS
        public static Workout Generate_Workout(int difficulty)
        {
            //CALCULATE INITIAL VALUES BASED ON DIFFICULTY
            int roundcount = (int)(2 * ((difficulty > 1) ? 2 : 1.5f));//3,4,6,8
            int circuits = 3 + difficulty;

            Round[] rounds = new Round[roundcount];

            //GENERATE ROUNDS BASED ON LEVEL
            for (int i = 0; i < roundcount; i++)
                rounds[i] = Generate_Round(difficulty);

            return new Workout(difficulty, roundcount, circuits, rounds);
        }
        public static Round Generate_Round(int difficulty)
        {
            //CREATES A DYNAMIC EXERCISE COUNT BY DIFFICULTY
            int exercisecount = 2 + difficulty;

            //OVERIDES DIFFICULTY TO NOT EFFECT EXERCISE DATABASE
            Math.Clamp(difficulty, 1, 4);

            //LIST OF EXERCISES IN ROUND
            Exercise[] exercises = new Exercise[exercisecount];

            //ITERATES FOR HOWEVER LARGE THE LIST IS TO GENERATE THAT MANY EXERCISES
            for (int i = 0; i < exercisecount; i++)
            {
                /*switch (i)
                {
                    default:
                        exercises[i] = new Exercise().GenerateExercise(level, _random.Next(1, 5));
                        break;
                    case 0:
                        exercises[i] = new Exercise().GenerateExercise(level, _random.Next(1, 3));
                        break;
                    case 1:
                        exercises[i] = new Exercise().GenerateExercise(level, _random.Next(3, 5));
                        break;
                    case 2:
                        exercises[i] = new Exercise().GenerateExercise(level, _random.Next(1, 3));
                        break;
                    case 3:
                        exercises[i] = new Exercise().GenerateExercise(level, _random.Next(3, 5));
                        break;
                    case 4:
                        exercises[i] = new Exercise().GenerateExercise(level, _random.Next(1, 3));
                        break;
                    case 5:
                        exercises[i] = new Exercise().GenerateExercise(level, _random.Next(3, 5));
                        break;
                }*/

                //IF ITS AN EVEN NUMBER, DO A UPPER/LOWER, IF ITS ODD, DO CORE OR CARDIO
                if (i % 2 == 0)
                    exercises[i] = Generate_Exercise(difficulty, _random.Next(1, 3));
                else
                    exercises[i] = Generate_Exercise(difficulty, _random.Next(3, 5));
            }
            return new Round(difficulty, exercises);
        }
        public static Exercise Generate_Exercise(int difficulty, int musclegroup)
        {
            List<Exercise> usableExercises = new List<Exercise>();

            //SQL STATEMENT
            string queryString = "SELECT ExerciseTable.name, ExerciseTable.duration, ExerciseTable.reps, ExerciseTable.difficulty, ExerciseTable.musclegroup, ExerciseTable.tip " +
                                    "FROM ExerciseTable " +
                                    "WHERE musclegroup = '" + musclegroup + "' AND difficulty <= '" + difficulty + "'";
            //string databse_path = 
            //CONNECTION STRING
            string connectionString = String.Format("Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                      "Integrated Security=true;" +
                                      "Connect Timeout = 3;" +
                                      "AttachDbFilename='{0}';", database_path);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                // Call Read before accessing data.
                while (reader.Read())
                {
                    //INITIAL VALUES
                    string name = "";
                    int duration = 0;
                    int reps = 0;
                    int level = 0;
                    int mgroup = 0;

                    //CHECK FOR NULL VALUES
                    int nameIndex = reader.GetOrdinal("name");
                    if (!reader.IsDBNull(nameIndex)) { name = reader.GetString(nameIndex); }
                    int durationIndex = reader.GetOrdinal("duration");
                    if (!reader.IsDBNull(durationIndex)) { duration = reader.GetInt32("duration"); }
                    int repsIndex = reader.GetOrdinal("reps");
                    if (!reader.IsDBNull(repsIndex)) { reps = reader.GetInt32(repsIndex); }
                    int levelIndex = reader.GetOrdinal("difficulty");
                    if (!reader.IsDBNull(levelIndex)) { level = reader.GetInt32(levelIndex); }
                    int musclegroupIndex = reader.GetOrdinal("musclegroup");
                    if (!reader.IsDBNull(musclegroupIndex)) { mgroup = reader.GetInt32(musclegroupIndex); }

                    /*string tip = reader.GetString(reader.GetOrdinal("tip"))*/

                    Exercise e = new Exercise(name, duration, reps, level, mgroup);

                    usableExercises.Add(e);
                }

                // Call Close when done reading.
                reader.Close();
            }


            //ONCE WE GET DATA FROM SQL, ADD TO A NEW EXERCISE OBJECT


            //RETURNS A RANDOM EXERCISE FROM USABLE LIST
            Random _random = new Random();
            return usableExercises[_random.Next(0, usableExercises.Count)];
        }
    }
}
