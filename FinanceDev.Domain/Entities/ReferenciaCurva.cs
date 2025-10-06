namespace FinanceDev.Domain.Entities
{
    public class ReferenciaCurva
    {
        public int Id { get; set; }
        public DateTime DataReferencia { get; set; }
        public string Categoria { get; set; }

        ICollection<DI1Curva> DI1Curvas { get; set; }
    }
}
