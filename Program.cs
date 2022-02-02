using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Home_Workout_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            /*Console.WriteLine("Generate Full Body Workout (No Gym!)");
            Console.WriteLine("Beginner (1)");
            Console.WriteLine("Intermediate (2)");
            Console.WriteLine("Advanced (3)");
            Console.WriteLine("Elite (4)");*/


            /*DisplayExercise(new Exercise().GenerateExercise(2, 3));
            DisplayExercise(new Exercise().GenerateExercise(1, 2));
            DisplayExercise(new Exercise().GenerateExercise(1, 4));
            DisplayExercise(new Exercise().GenerateExercise(3, 4));*/


            //GENERATES A SAMPLE WORKOUT
            DisplayWorkout(Workout.GenerateWorkout(3));

            /*Console.WriteLine("Round 1\n");
            DisplayRound(new Round(1));
            Console.WriteLine("\nRound 2\n");
            DisplayRound(new Round(1));
            Console.WriteLine("\nRound 3\n");
            DisplayRound(new Round(1));*/



        }


        public static void DisplayExercise(Exercise e)
        {
            Console.WriteLine(e.base_name + " " + (int)e.base_duration + " seconds" + " " + e.base_repsgoal + " reps" + " " + (Exercise.workoutLevel)e.base_workoutlevel + " difficulty" + " " + (Exercise.muscleGroup)e.muscle + " muscle group");
            /*
            Console.WriteLine(e.base_name);
            Console.WriteLine((int)e.base_duration + " seconds");
            Console.WriteLine(e.base_repsgoal + " reps");
            Console.WriteLine((Exercise.workoutLevel)e.base_workoutlevel + " difficulty");
            Console.WriteLine((Exercise.muscleGroup)e.muscle + " muscle group");
            */
        }
        public static void DisplayRound(Round r)
        {
            foreach (Exercise e in r.exercises)
            {
                DisplayExercise(e);
            }
        }
        public static void DisplayWorkout(Workout workout)
        {
            Console.WriteLine("Generated Workout: " + workout.workoutlevel);
            for (int i = 0; i < workout.rounds.Length; i++)
            {
                Console.WriteLine("\n" + "Round " + (i + 1) + "\n");
                DisplayRound(workout.rounds[i]);
            }
        }
    }

    public class Workout{

        public Exercise.workoutLevel workoutlevel = Exercise.workoutLevel.Beginner;//Limits difficulty of exercises
                                                                 //(Controls how many exercises per circuit, speed/tempo of exercises, duration of exercises, exercise complexity) 
        public int roundcount = 4; //How many exercise round groups?
        public int circuits = 2;//How many circuits will this entire workout consist of? (All rounds)

        public Round[] rounds;

        public Workout(int level)
        {
            workoutlevel = (Exercise.workoutLevel)level;
            roundcount = (int)(2 * ((level > 1) ? 2 : 1.5f));//3,4,6,8
            circuits = 3 + level;

            rounds = new Round[roundcount];

            for (int i = 0; i < roundcount; i++)
                rounds[i] = new Round((int)workoutlevel);
        }


        public static Workout GenerateWorkout(int level)
        {
            Workout workout = new Workout(level);

            return workout;
        }

   }

    public class Round
    {
        private static Random _random = new Random();

        //public int index;
        public int exercisecount;
        public int difficulty;
        
        public Exercise[] exercises = new Exercise[4];

        public Round(int level)
        {
            difficulty = level;
            exercisecount = 2 + difficulty;

            exercises = CreateRound(difficulty, exercisecount);
        }

        public Exercise[] CreateRound(int level, int ecount)
        {
            //OVERIDES DIFFICULTY TO NOT EFFECT EXERCISE DATABASE
            Math.Clamp(level, 1, 4);

            //LIST OF EXERCISES IN ROUND
            Exercise[] exercises = new Exercise[ecount];

            //ITERATES FOR HOWEVER LARGE THE LIST IS TO GENERATE THAT MANY EXERCISES
            for (int i = 0; i < ecount; i++)
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
                if (i%2==0)
                    exercises[i] = new Exercise().GenerateExercise(level, _random.Next(1, 3));
                else
                    exercises[i] = new Exercise().GenerateExercise(level, _random.Next(3, 5));
            }
            return exercises;
        }
    }

    public class Exercise
    {
        public string base_name = "";//Comes from .XML named by workoutlevel
        public int base_duration = 20;//Duration in seconds (Influenced by WorkoutLevel)
        public int base_repsgoal = 10;//Display a rep goal for people to aim for (Influenced by WorkoutLevel and Duration)

        public workoutLevel base_workoutlevel = workoutLevel.Beginner;//What is the base difficulty level of the exercise?
        public muscleGroup muscle = muscleGroup.upper;//What muscle group does this exercise target?

        public Exercise()
        {
            base_name = "";
            base_duration = 0;
            base_repsgoal = 0;
            base_workoutlevel = 0;
            muscle = 0;
        }

        public Exercise(string name, int duration, int repgoal, int level, int musclegroup)
        {
            base_name = name;
            base_duration = duration;
            base_repsgoal = repgoal;
            base_workoutlevel = (workoutLevel)level;
            muscle = (muscleGroup)musclegroup;
        }

        public enum workoutLevel
        {
            Beginner = 1,
            Intermediate = 2,
            Advanced = 3,
            Elite = 4
        }
        public enum muscleGroup
        {
            upper = 1,
            lower = 2,
            core = 3,
            cardio = 4
        }

        public Exercise GenerateExercise(int difficulty, int musclegroup)
        {
            List<Exercise> usableExercises = new List<Exercise>();

            string queryString = "SELECT ExerciseTable.name, ExerciseTable.duration, ExerciseTable.reps, ExerciseTable.difficulty, ExerciseTable.musclegroup, ExerciseTable.tip FROM ExerciseTable WHERE musclegroup = '" + musclegroup + "' AND difficulty <= '" + difficulty + "'";
            string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                                      "Integrated Security=true;" +
                                      "Connect Timeout = 3;" +
                                      "AttachDbFilename='C:\\Users\\Nlara\\Documents\\Exercises Database.mdf';";

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

            Random _random = new Random();
            return usableExercises[_random.Next(0, usableExercises.Count)];
        }

    }
}


//EXERCISE DATA\\
/*
ID - int
NAME - string
BASE DURATION - int
BASE REP GOAL - int
BASE LEVEL - int
MUSCLE GROUP - int
*/
