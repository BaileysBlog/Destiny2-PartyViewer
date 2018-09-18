using System;
using System.Collections.Generic;
using System.Text;

namespace D2DataAccess.Models
{
    public class DestinyProgression
    {
        public UInt32 progressionHash { get; set; }
        public Int32 dailyProgress { get; set; }
        public Int32 dailyLimit { get; set; }
        public Int32 weeklyProgress { get; set; }
        public Int32 currentProgress { get; set; }
        public Int32 level { get; set; }
        public Int32 levelCap { get; set; }
        public Int32 setpIndex { get; set; }
        public Int32 progressToNextLevel { get; set; }
        public Int32 nextLevelAt { get; set; }
    }
}
