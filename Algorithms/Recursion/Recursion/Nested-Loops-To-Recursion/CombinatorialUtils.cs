namespace Nested_Loops_To_Recursion
{
    enum LoopType
    {
        FullLoop,
        CombinationWithRepetitions,
        CombinationWithoutRepetitions
    }

    public delegate void LoopAction(string value);

    public class CombinatorialUtils
    {
        private LoopAction loopAction;

        internal CombinatorialUtils(LoopAction loopAction)
        {
            this.loopAction = loopAction;
        }

        public void MakeCombinations(int length, int loopDepth, bool withRepetitions)
        {
            LoopExponentially(length, loopDepth,
                withRepetitions ?
                    LoopType.CombinationWithRepetitions :
                    LoopType.CombinationWithoutRepetitions);
        }

        public void LoopExponentially(int length, int loopDepth)
        {
            LoopExponentially(length, loopDepth, LoopType.FullLoop);
        }

        private void LoopExponentially(
            int length, int loopDepth, LoopType loopType, int index = 0, int nestLevel = 1, string value = "")
        {
            if (index++ < length)
            {
                if (nestLevel < loopDepth)
                {
                    int startIndexNextLoop = 0;
                    switch (loopType)
                    {
                        case LoopType.CombinationWithRepetitions: startIndexNextLoop = index - 1; break;
                        case LoopType.CombinationWithoutRepetitions: startIndexNextLoop = index; break;
                    }

                    // loops in depth to the most nested loop
                    LoopExponentially(length, loopDepth, loopType, startIndexNextLoop, nestLevel + 1, value + index);
                }

                if (value.Length == loopDepth - 1)
                {
                    // do the action in the most nested loop
                    loopAction(value + index);
                }

                // loops as a normal loop
                LoopExponentially(length, loopDepth, loopType, index, nestLevel, value);
            }
        }
    }
}
