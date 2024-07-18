using System;
using System.Collections.Generic;
using System.Linq;

class Patient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Diagnosis { get; set; }
}

class Doctor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Specialization { get; set; }
}

class Appointment
{
    public int Id { get; set; }
    public int PatientId { get; set; }
    public int DoctorId { get; set; }
    public DateTime Date { get; set; }
}

class HospitalSystem
{
    private List<Patient> patients = new List<Patient>();
    private List<Doctor> doctors = new List<Doctor>();
    private List<Appointment> appointments = new List<Appointment>();
    private int nextPatientId = 1;
    private int nextDoctorId = 1;
    private int nextAppointmentId = 1;

    public void Run()
    {
        while (true)
        {
            DisplayMenu();
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddPatient();
                    break;
                case "2":
                    AddDoctor();
                    break;
                case "3":
                    ScheduleAppointment();
                    break;
                case "4":
                    ViewPatients();
                    break;
                case "5":
                    ViewDoctors();
                    break;
                case "6":
                    ViewAppointments();
                    break;
                case "7":
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }
    }

    private void DisplayMenu()
    {
        Console.Clear();
        Console.WriteLine("=== Hospital Database System ===");
        Console.WriteLine("1. Add Patient");
        Console.WriteLine("2. Add Doctor");
        Console.WriteLine("3. Schedule Appointment");
        Console.WriteLine("4. View Patients");
        Console.WriteLine("5. View Doctors");
        Console.WriteLine("6. View Appointments");
        Console.WriteLine("7. Exit");
        Console.Write("Enter your choice (1-7): ");
    }

    private void AddPatient()
    {
        Console.Write("Enter patient name: ");
        string name = Console.ReadLine();
        Console.Write("Enter patient age: ");
        if (int.TryParse(Console.ReadLine(), out int age))
        {
            Console.Write("Enter patient diagnosis: ");
            string diagnosis = Console.ReadLine();
            
            patients.Add(new Patient { Id = nextPatientId++, Name = name, Age = age, Diagnosis = diagnosis });
            Console.WriteLine("Patient added successfully.");
        }
        else
        {
            Console.WriteLine("Invalid age. Patient not added.");
        }
    }

    private void AddDoctor()
    {
        Console.Write("Enter doctor name: ");
        string name = Console.ReadLine();
        Console.Write("Enter doctor specialization: ");
        string specialization = Console.ReadLine();
        
        doctors.Add(new Doctor { Id = nextDoctorId++, Name = name, Specialization = specialization });
        Console.WriteLine("Doctor added successfully.");
    }

    private void ScheduleAppointment()
    {
        Console.Write("Enter patient ID: ");
        if (int.TryParse(Console.ReadLine(), out int patientId) && patients.Any(p => p.Id == patientId))
        {
            Console.Write("Enter doctor ID: ");
            if (int.TryParse(Console.ReadLine(), out int doctorId) && doctors.Any(d => d.Id == doctorId))
            {
                Console.Write("Enter appointment date (yyyy-MM-dd HH:mm): ");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime appointmentDate))
                {
                    appointments.Add(new Appointment { Id = nextAppointmentId++, PatientId = patientId, DoctorId = doctorId, Date = appointmentDate });
                    Console.WriteLine("Appointment scheduled successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid date format. Appointment not scheduled.");
                }
            }
            else
            {
                Console.WriteLine("Invalid doctor ID. Appointment not scheduled.");
            }
        }
        else
        {
            Console.WriteLine("Invalid patient ID. Appointment not scheduled.");
        }
    }

    private void ViewPatients()
    {
        Console.WriteLine("Patients:");
        foreach (var patient in patients)
        {
            Console.WriteLine($"ID: {patient.Id}, Name: {patient.Name}, Age: {patient.Age}, Diagnosis: {patient.Diagnosis}");
        }
    }

    private void ViewDoctors()
    {
        Console.WriteLine("Doctors:");
        foreach (var doctor in doctors)
        {
            Console.WriteLine($"ID: {doctor.Id}, Name: {doctor.Name}, Specialization: {doctor.Specialization}");
        }
    }

    private void ViewAppointments()
    {
        Console.WriteLine("Appointments:");
        foreach (var appointment in appointments)
        {
            var patient = patients.First(p => p.Id == appointment.PatientId);
            var doctor = doctors.First(d => d.Id == appointment.DoctorId);
            Console.WriteLine($"ID: {appointment.Id}, Patient: {patient.Name}, Doctor: {doctor.Name}, Date: {appointment.Date}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        HospitalSystem hospitalSystem = new HospitalSystem();
        hospitalSystem.Run();
    }
}