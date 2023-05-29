using System.ComponentModel.DataAnnotations;

namespace HospitalDatabase1.Data.Models
{
    public class Medicament
    {
        public int MedicamentId { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<PatientMedicament> PatientMedicament { get; set; }
    }
}
