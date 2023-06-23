using System.ComponentModel.DataAnnotations;

namespace HospitalDatabase1.Data.Models
{
    public class Patient
    {
        public int PatientId { get; set; }

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string LastName { get; set; }

        [MaxLength(250)]
        public string Address { get; set; }

        [MaxLength(80)]
        public string Email { get; set; }
        public bool HasMedicalInsurance { get; set; }

        public ICollection<Visitation> Visitations { get; set; }
        public ICollection<Diagnose> Diagnoses { get; set; }
        public ICollection<PatientMedicament> PatientMedicaments { get; set; }
    }
}
