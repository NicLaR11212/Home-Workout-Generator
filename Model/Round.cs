using System;
using System.Collections.Generic;
using System.Text;

namespace Home_Workout_Generator.Model
{
    public class Round
    {
        private int exercisecount;
        private int difficulty;
        private Exercise[] exercises = new Exercise[4];        //base of 4

        public int ROUND_DIFFICULTY { get { return difficulty; } }
        public int EXERCISE_COUNT { get { return exercisecount; } }
        public Exercise[] EXERCISE_DATA { get { return exercises; } }

        public Round(int _difficulty, Exercise[] _exercises)
        {
            difficulty = _difficulty;
            exercisecount = _exercises.Length;
            exercises = _exercises;
        }
    }
}
