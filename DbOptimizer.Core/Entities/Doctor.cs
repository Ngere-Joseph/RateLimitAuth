namespace DbOptimizer.Core.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DstType { get; set; }
        public string StateCode { get; set; }
        public decimal ConsultFee { get; set; }
    }
}
