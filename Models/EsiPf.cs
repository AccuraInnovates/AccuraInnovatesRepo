namespace Accura_Innovatives.Models
{
    public class EsiPf
    {
        public int EsiPfID { get; set; }

        public double EsiEmployeeContribution { get; set; } = 0;

        public double EsiEmployerContribution { get; set; } = 0;

        public double EpfEmployeeContribution { get; set; } = 0;
        public double EpfEmployerContribution { get; set; } = 0;
        public double EpsEmployerContribution { get; set; } = 0;
        public string EffectFrom { get; set; } = (DateTime.Now).ToString();
        public string EffectTo { get; set; } = (DateTime.Now).ToString();
    }
}
