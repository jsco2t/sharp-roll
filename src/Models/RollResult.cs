using System;

namespace SharpRoll.Model
{
    public class RollResult
    {
        public int DiceSideCount;
        public int Result;
        public int Entropy;
        public DateTime RollTimeStamp; 

        public RollResult(int diceSideCount, int entropy, int result)
        {
            DiceSideCount = diceSideCount;
            Result = result;
            Entropy = entropy;
            RollTimeStamp = DateTime.Now;
        }
    }
}