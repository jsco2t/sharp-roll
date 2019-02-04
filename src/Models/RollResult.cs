using System;

namespace SharpRoll.Model
{
    public class RollResult
    {
        public int DiceSideCount;
        public int RollCount;
        public int RollModifier;
        public int Result;
        public int Entropy;
        public DateTime RollTimeStamp; 

        public RollResult(int diceSideCount, int rollCount, int rollModifier, int entropy, int result)
        {
            DiceSideCount = diceSideCount;
            RollCount = rollCount;
            RollModifier = rollModifier;
            Result = result;
            Entropy = entropy;
            RollTimeStamp = DateTime.Now;
        }
    }
}