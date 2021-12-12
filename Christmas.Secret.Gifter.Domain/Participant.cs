using TypeGen.Core.TypeAnnotations;

namespace Christmas.Secret.Gifter.Domain
{
    [ExportTsInterface]
    public class Participant
    {
        public string Id { get; set; }
        public int OrderId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public IEnumerable<int> ExcludedOrderIds { get; set; }
        public int[] ToInputDataRow(int amountOfParticipants)
        {
            var row = new int[amountOfParticipants];

            for (int i = 0; i < amountOfParticipants; i++)
            {
                if (i == OrderId)
                {
                    row[i] = -1;
                }
                else if (ExcludedOrderIds.Contains(i))
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
