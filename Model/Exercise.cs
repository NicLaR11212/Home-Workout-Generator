using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Workout_Generator.Model
{
    public class Exercise
    {
        private string base_name = "";//Comes from .XML named by workoutlevel
        private int base_duration = 20;//Duration in seconds (Influenced by WorkoutLevel)
        private int base_repsgoal = 10;//Display a rep goal for people to aim for (Influenced by WorkoutLevel and Duration)

        private WorkoutLevel base_workoutlevel = WorkoutLevel.Beginner;//What is the base difficulty level of the exercise?
        private muscleGroup muscle = muscleGroup.upper;//What muscle group does this exercise target?

        public string BASE_NAME { get { return base_name; } }
        public int BASE_DURATION { get { return base_duration; } }
        public int BASE_REPGOAL { get { return base_repsgoal; } }
        public WorkoutLevel BASE_WORKOUTLEVEL { get { return base_workoutlevel; } }
        public muscleGroup BASE_MUSCLEGROUP { get { return muscle; } }



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
            base_workoutlevel = (WorkoutLevel)level;
            muscle = (muscleGroup)musclegroup;
        }
        public enum muscleGroup
        {
            upper = 1,
            lower = 2,
            core = 3,
            cardio = 4
        }     
    }

    public enum WorkoutLevel
    {
        Beginner = 1,
        Intermediate = 2,
        Advanced = 3,
        Elite = 4
    }
}
