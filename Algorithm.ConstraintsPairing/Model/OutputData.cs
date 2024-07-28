using Google.OrTools.LinearSolver;

namespace Algorithm.ConstraintsPairing.Model
{
    public class OutputData
    {
        private Variable[,] variables;

        public Solver.ResultStatus Status;

        public InputData Input { get; set; }

        public Variable[,] Variables
        {
            get
            {
                return variables;
            }
            set
            {
                variables = value;
            }
        }

        public Pair[] Pairs { get; set; }

        public int GifterAmount => Input.GifterAmount;
    }
}