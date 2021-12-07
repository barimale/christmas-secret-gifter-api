using System.Collections.Generic;

namespace Algorithm.ConstraintsPairing.Model.Requests
{
    public class AlgorithmRequest
    {
        public List<Participant> Data { get; set; } = new List<Participant>();

        public InputData ToInputData()
        {
            var costs = new int[Data.Count, Data.Count];

            for (int i = 0; i < Data.Count; i++)
            {
                var row = Data[i].ToInputDataRow(Data.Count);
                for (int j = 0; j < row.Length; j++)
                {
                    costs[i, j] = row[j];
                }
            }

            return new InputData() { 
                Costs = costs 
            };
        }
    }

    public class Participant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<int> ExcludedIds { get; set; }
        public int[] ToInputDataRow(int amountOfParticipants)
        {
            var row = new int[amountOfParticipants];

            for (int i = 0; i < amountOfParticipants; i++)
            {
                if(i == Id)
                {
                    row[i] = -1;
                }else if (ExcludedIds.Contains(i))
                {
                    row[i] = 100;
                }
                else
                {
                    row[i] = 0;
                }
            }

            return row;
        }
    }
}
