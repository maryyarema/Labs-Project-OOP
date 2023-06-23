using HospitalDatabase1.CRUD.Patients;
using HospitalDatabase1.Data;
using HospitalDatabase1.Data.Models;
using System.Net;

Console.WriteLine("Choose what to do:");
Console.WriteLine("1 - create patient\n2 - get list of patients\n3 - update patient\n4 - delete patient");

int action = int.Parse(Console.ReadLine());

switch (action)
{
    case 1:
        CRUDPatient.Create();
        break;
    case 2:
        CRUDPatient.Read();
        break;
    case 3:
        CRUDPatient.Update();
        break;
    case 4:
        CRUDPatient.Delete();
        break;
    default: Console.WriteLine("Invalid action!"); break;
}