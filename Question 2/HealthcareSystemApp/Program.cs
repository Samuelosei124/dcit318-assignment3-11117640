using System;

namespace HealthcareSystem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var app = new HealthSystemApp();
            app.SeedData();
            app.BuildPrescriptionMap();
            app.PrintAllPatients();

            Console.WriteLine("\nEnter a Patient ID to view prescriptions:");
            if (int.TryParse(Console.ReadLine(), out int patientId))
            {
                app.PrintPrescriptionsForPatient(patientId);
            }
            else
            {
                Console.WriteLine("Invalid Patient ID entered.");
            }
        }
    }
}
