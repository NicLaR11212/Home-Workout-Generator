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
            catch
            {
                Console.WriteLine("Please input a valid difficulty.");
                Console.ReadKey();
                Start_Display();
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

