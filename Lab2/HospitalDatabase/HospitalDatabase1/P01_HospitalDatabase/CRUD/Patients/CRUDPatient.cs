using HospitalDatabase1.Data.Models;
using HospitalDatabase1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HospitalDatabase1.CRUD.Patients
{
    public class CRUDPatient
    {
        public static string Create()
        {
            using (var context = new HospitalContext())
            {
                Console.WriteLine("Insert information about a new patient (new line each info) (name, surname, address, email, medical insurance {1 - yes, 0 - no}): ");

                string? firstName = Console.ReadLine();
                string? lastName = Console.ReadLine();
                string? address = Console.ReadLine();
                string? email = Console.ReadLine();
                bool hasMedicalInsurance = Console.ReadLine() == "1" ? true : false;

                if (firstName != null && lastName != null && address != null && email != null)
                {
                    // CREATE
                    var patient = new Patient
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        Address = address,
                        Email = email,
                        HasMedicalInsurance = hasMedicalInsurance,
                    };
                    context.Patients.Add(patient);
                    context.SaveChanges();
                    return $"Patient {firstName} {lastName} created";
                }

                return $"Invalid input. Patient wasn't created";
            }
        }

        public static string Read()
        {
            using (var context = new HospitalContext())
            {
                Console.WriteLine("Patient's list: ");

                List<Patient> patients = context.Patients.ToList();


                if (patients.Count != 0)
                {
                    foreach (Patient patient in patients)
                    {
                        Console.WriteLine($"\tId: {patient.PatientId} - {patient.FirstName} {patient.LastName}\n\tAddress: {patient.Address}\n\tHas medical insurance: {patient.HasMedicalInsurance}");
                    }
                    return "End";
                }

                return $"There aren't any patients in list";
            }
        }

        public static string Delete()
        {
            using (var context = new HospitalContext())
            {
                Console.WriteLine("Enter patient's id who we have to delete: ");

                int id = int.Parse(Console.ReadLine());

                var patient = context.Patients.Find(id);

                if (patient != null)
                {
                    context.Patients.Remove(patient);
                    context.SaveChanges();

                    return $"Patient with id {id} was deleted";
                }

                return $"Patient with id {id} wasn't find.";
            }
        }

        public static string Update()
        {
            using (var context = new HospitalContext())
            {
                Console.WriteLine("Enter patient's id who we have to update: ");

                int id = int.Parse(Console.ReadLine());

                var patient = context.Patients.Find(id);

                if (patient != null)
                {
                    Console.WriteLine("Insert information about a updatede patient (new line each info) (name, surname, address, email, medical insurance {1 - yes, 0 - no}): ");

                    string? firstName = Console.ReadLine();
                    string? lastName = Console.ReadLine();
                    string? address = Console.ReadLine();
                    string? email = Console.ReadLine();
                    bool hasMedicalInsurance = Console.ReadLine() == "1" ? true : false;

                    if (firstName != null && lastName != null && address != null && email != null)
                    {
                        var updatedPatient = new Patient
                        {
                            FirstName = firstName,
                            LastName = lastName,
                            Address = address,
                            Email = email,
                            HasMedicalInsurance = hasMedicalInsurance,
                        };
                        context.Patients.Update(updatedPatient);
                        context.SaveChanges();

                        return $"Patient with id {id} was updated!";
                    }
                }
                return $"Patient with id {id} wasn't found";
            }
        }
    }
}
