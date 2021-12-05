namespace Algorithm.ConstraintsPairing.Model.Responses
{
    public class AlgorithmResponse
    {
        public bool IsError { get; set; } = false;
        public string Reason { get; set; }
        public OutputData Data {  get; set; }
    }
}
