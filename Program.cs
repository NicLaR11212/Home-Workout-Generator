using System;
using Home_Workout_Generator.Model;
using Home_Workout_Generator.Controller;
using System.IO;

//EXERCISE DATA\\
/*
ID - int
NAME - string
BASE DURATION - int
BASE REP GOAL - int
BASE LEVEL - int
MUSCLE GROUP - int
*/

namespace Home_Workout_Generator
{
    class Program
    {
        string workingDirectory = Environment.CurrentDirectory;
        static void Main(string[] args)
        {
            Start_Display();
            


            //DisplayWorkout(database_connection.Generate_Workout(2));




        }


        #region USER INPUT
        public static void Start_Display()
        {
            Console.Clear();
            Console.WriteLine("Welcome to the home fitness workout generator created by Nicholas La Raffa!");
            Console.WriteLine("Generate A Full Body Workout (No Gym Required!)\n");

            Console.WriteLine("Choose A Difficulty:\n");
            Console.WriteLine("Beginner (1)");
            Console.WriteLine("Intermediate (2)");
            Console.WriteLine("Advanced (3)");
            Console.WriteLine("Elite (4)");

            Select_Difficulty(Console.ReadLine());
        }

        public static void Select_Difficulty(string value)
        {
            Console.Clear();
            int int_val = -1;
            try
            {
                int_val = int.Parse(value);
            }
            catch
            {
                Console.WriteLine("Please input a valid difficulty.");
                Console.ReadKey();
                Start_Display();
            }

            if (int_val > 0 && int_val < 5)
            {
                Console.WriteLine("Your difficulty is:\n" + (WorkoutLevel)int_val);
                Console.WriteLine("Press Enter to start...");
                Console.ReadKey();
                DisplayWorkout(database_connection.Generate_Workout(int_val));
            }
            else
            {
                Console.WriteLine("Please input a valid difficulty.");
                Console.ReadKey();
                Start_Display();
            }
        }
        #endregion

        #region DISPLAY DATA
        //DISPLAY FUNCTIONS
        public static void DisplayWorkout(Workout workout)
        {
            Console.WriteLine("Generated Workout: " + workout.WORKOUT_DIFFICULTY);

            if (workout.WORKOUT_DIFFICULTY == WorkoutLevel.Elite)
                Console.WriteLine("You are a fool for attempting such an elite workout...");

            for (int i = 0; i < workout.ROUND_COUNT; i++)
            {
                Console.WriteLine("\n" + "Round " + (i + 1) + "\n");
                DisplayRound(workout.ROUND_DATA[i]);
            }

            Console.WriteLine("\n\nDo you want to display another workout? y/n");
            string cont_prog = Console.ReadLine();

            if (cont_prog == "n")
                System.Environment.Exit(-1);
            else if (cont_prog == "y")
            {
                Start_Display();
            }
            else
            {
                Console.WriteLine("Invalid input. Application will now exit.");
                Console.ReadKey();
                System.Environment.Exit(-1);
            }

        }
        public static void DisplayRound(Round r)
        {
            foreach (Exercise e in r.EXERCISE_DATA)
            {
                DisplayExercise(e);
            }
        }
        public static void DisplayExercise(Exercise e)
        {
            Console.WriteLine(e.BASE_NAME + " " +
                             (int)e.BASE_DURATION + " seconds" + " " +
                              e.BASE_REPGOAL + " reps" + " " +
                             (WorkoutLevel)e.BASE_WORKOUTLEVEL + " difficulty" + " " +
                             (Exercise.muscleGroup)e.BASE_MUSCLEGROUP + " muscle group");
            /*
            Console.WriteLine(e.base_name);
            Console.WriteLine((int)e.base_duration + " seconds");
            Console.WriteLine(e.base_repsgoal + " reps");
            Console.WriteLine((Exercise.workoutLevel)e.base_workoutlevel + " difficulty");
            Console.WriteLine((Exercise.muscleGroup)e.muscle + " muscle group");
            */
        }
        #endregion
    }
}

