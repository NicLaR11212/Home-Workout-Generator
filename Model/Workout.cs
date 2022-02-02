using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Workout_Generator.Model
{
    public class Workout
    {
        private WorkoutLevel workoutlevel = WorkoutLevel.Beginner;//Limits difficulty of exercises                                                                  //(Controls how many exercises per circuit, speed/tempo of exercises, duration of exercises, exercise complexity) 
        private int roundcount = 4; //How many exercise round groups?
        private int circuits = 2;//How many circuits will this entire workout consist of? (All rounds)
        private Round[] rounds;

        public WorkoutLevel WORKOUT_DIFFICULTY { get { return workoutlevel; } }
        public int ROUND_COUNT { get { return roundcount; } }
        public int CIRCUITS { get { return circuits; } }
        public Round[] ROUND_DATA { get { return rounds; } }

        //creates a workout with preset statistics       
        public Workout(int level, int round_count, int rotations, Round[] round_data)
        {
            workoutlevel = (WorkoutLevel)level;
            roundcount = round_count;
            circuits = rotations;
            rounds = round_data;           
        }

    }
}
