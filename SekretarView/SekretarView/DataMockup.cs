using Model.HospitalResources;
using Model.Schedule.Hospitalizations;
using Model.Schedule.Procedures;
using Model.Users.Employee;
using Model.Users.Generalities;
using Model.Users.Patient;
using Model.Users.UserAccounts;
using Model.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SekretarView
{
    class DataMockup
    {
        private static DataMockup _instance;

        public static DataMockup Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataMockup();

                return _instance;
            }
        }

        public List<Country> Countries { get; private set; }
        public List<City> Cities { get; private set; }
        public List<Patient> Patients { get; private set; }
        public List<Specialty> Specialties { get; private set; }
        public List<Doctor> Doctors { get; private set; }
        public List<Room> Rooms { get; private set; }
        public List<Department> Departments { get; private set; }
        public List<EmployeeAccount> Users { get; private set; }
        public List<ProcedureType> ProcedureTypes { get; private set; }
        public Dictionary<DateTime, List<Examination>> Examinations { get; private set; }
        public Dictionary<DateTime, List<Surgery>> Surgeries { get; private set; }
        public List<HospitalizationType> HospitalizationTypes { get; private set; }
        public List<Hospitalization> Hospitalizations { get; private set; }

        private DataMockup()
        {
            #region CitiesAndCountries
            Countries = new List<Country>();
            Countries.Add(new Country() { Name = "Srbija", Code = "SRB" });
            Countries.Add(new Country() { Name = "Bosna i Hercegovina", Code = "BIH" });

            Cities = new List<City>();
            Cities.Add(new City() { Name = "Novi Sad", Country = Countries[0] });
            Cities.Add(new City() { Name = "Beograd", Country = Countries[0] });
            Cities.Add(new City() { Name = "Kula", Country = Countries[0] });
            Cities.Add(new City() { Name = "Sombor", Country = Countries[0] });
            Cities.Add(new City() { Name = "Niš", Country = Countries[0] });
            Cities.Add(new City() { Name = "Subotica", Country = Countries[0] });
            Cities.Add(new City() { Name = "Loznica", Country = Countries[0] });
            Cities.Add(new City() { Name = "Valjevo", Country = Countries[0] });
            Cities.Add(new City() { Name = "Bijeljina", Country = Countries[1] });
            Cities.Add(new City() { Name = "Milići", Country = Countries[1] });
            Cities.Add(new City() { Name = "Banja Luka", Country = Countries[1] });
            Cities.Add(new City() { Name = "Teslić", Country = Countries[1] });

            #endregion

            #region Patients

            Patients = new List<Patient>();
            Patients.Add(new Patient()
            {
                Name = "Milica",
                Surname = "Todorović",
                Registered = true,
                DateOfBirth = DateTime.Parse("28/2/1997"),
                JMBG = "2802997188736",
                TelephoneNumber = "0601322175",
                Address = new Address() { City = Cities[0], Street = "Marka Miljanova 7" }
            });
            Patients.Add(new Patient()
            {
                Name = "Marko",
                Surname = "Franjo",
                Registered = false,
                DateOfBirth = DateTime.Parse("15/10/1995"),
                JMBG = "1510995188736",
                TelephoneNumber = "0601322175",
                Gender = Gender.Male
            });
            Patients.Add(new Patient()
            {
                Name = "Jelena",
                Surname = "Krstić",
                Registered = false,
                DateOfBirth = DateTime.Parse("3/3/1950"),
                JMBG = "0303950188736",
                TelephoneNumber = "0601322175",
                Gender = Gender.Female
            });

            #endregion

            #region Doctors

            Specialties = new List<Specialty>();
            Specialties.Add(new Specialty { Name = "Opšti", Description = "nešto" });
            Specialties.Add(new Specialty { Name = "Gastroenterolog", Description = "nešto" });
            Specialties.Add(new Specialty { Name = "Hirurg", Description = "nešto" });

            Doctors = new List<Doctor>();
            Doctors.Add(new Doctor
            {
                Name = "Tanja",
                Surname = "Sušić",
                Specialty = Specialties[0]
            });
            Doctors.Add(new Doctor
            {
                Name = "Stevan",
                Surname = "Trbojević",
                Specialty = Specialties[1]
            });
            Doctors.Add(new Doctor
            {
                Name = "Gregory",
                Surname = "House",
                Specialty = Specialties[0]
            });
            Doctors.Add(new Doctor
            {
                Name = "Alex",
                Surname = "Karev",
                Specialty = Specialties[2]
            });
            Doctors.Add(new Doctor
            {
                Name = "Jackson",
                Surname = "Avery",
                Specialty = Specialties[2]
            });

            #endregion

            #region Rooms

            Departments = new List<Department>();
            Departments.Add(new Department() { Name = "Opšte", Description = "" });
            Departments.Add(new Department() { Name = "Hirurgija", Description = "" });
            Departments.Add(new Department() { Name = "Gastroenterologija", Description = "" });

            Rooms = new List<Room>();
            Rooms.Add(new Room()
            {
                Name = "Soba za preglede 1",
                Department = Departments[0],
                Purpose = RoomType.examinationRoom
            });
            Rooms.Add(new Room()
            {
                Name = "Soba za preglede 2",
                Department = Departments[0],
                Purpose = RoomType.examinationRoom
            });
            Rooms.Add(new Room()
            {
                Name = "Soba za preglede 3",
                Department = Departments[2],
                Purpose = RoomType.examinationRoom
            });
            Rooms.Add(new Room()
            {
                Name = "Soba za operacije 1",
                Department = Departments[1],
                Purpose = RoomType.operatingRoom
            });
            Rooms.Add(new Room()
            {
                Name = "Soba za operacije 2",
                Department = Departments[1],
                Purpose = RoomType.operatingRoom
            });
            Rooms.Add(new Room()
            {
                Name = "Soba za operacije 3",
                Department = Departments[1],
                Purpose = RoomType.operatingRoom
            });
            Rooms.Add(new Room()
            {
                Name = "Soba za pacijente 1",
                Department = Departments[2],
                Purpose = RoomType.recoveryRoom
            });
            Rooms.Add(new Room()
            {
                Name = "Soba za pacijente 2",
                Department = Departments[2],
                Purpose = RoomType.recoveryRoom
            });

            #endregion

            #region User

            Users = new List<EmployeeAccount>();
            Users.Add(new EmployeeAccount()
            {
                EmployeeType = EmployeeType.secretary,
                Username = "sekretar",
                Password = "sekretar",
                Employee = new Employee()
                {
                    Name = "Sekretar",
                    Surname = "Sekretarić",
                    DateOfBirth = DateTime.Parse("21/12/1980"),
                    Address = new Address() { City = Cities[0], Street = "Kosovska 13" },
                    TelephoneNumber = "00381601452700",
                    JMBG = "2711998188734"
                }
            });

            #endregion

            #region ProcedureTypes

            ProcedureTypes = new List<ProcedureType>();
            ProcedureTypes.Add(new ProcedureType
            {
                Name = "Pregled kod opšteg doktora",
                Kind = ProcedureKind.examination,
                QualifiedSpecialties = new ArrayList(new Specialty[] { Specialties[0] }),
                Duration = new TimeSpan(0, 20, 0)
            });
            ProcedureTypes.Add(new ProcedureType
            {
                Name = "Pregled kod gastroenterologa",
                Kind = ProcedureKind.examination,
                QualifiedSpecialties = new ArrayList(new Specialty[] { Specialties[1] }),
                Duration = new TimeSpan(0, 30, 0)
            });
            ProcedureTypes.Add(new ProcedureType
            {
                Name = "Gastroskopija",
                Kind = ProcedureKind.examination,
                QualifiedSpecialties = new ArrayList(new Specialty[] { Specialties[1] }),
                Duration = new TimeSpan(0, 45, 0)
            });
            ProcedureTypes.Add(new ProcedureType
            {
                Name = "Apendektomija",
                Kind = ProcedureKind.surgery,
                QualifiedSpecialties = new ArrayList(new Specialty[] { Specialties[2] }),
                Duration = new TimeSpan(0, 45, 0)
            });
            ProcedureTypes.Add(new ProcedureType
            {
                Name = "Tonsilektomija",
                Kind = ProcedureKind.surgery,
                QualifiedSpecialties = new ArrayList(new Specialty[] { Specialties[2] }),
                Duration = new TimeSpan(0, 45, 0)
            });

            #endregion

            #region Examinations
            Examinations = new Dictionary<DateTime, List<Examination>>();

            DateTime today = DateTime.Now.Date;
            Examinations[today] = new List<Examination>();
            Examinations[today].Add(new Examination
            {
                ProcedureType = ProcedureTypes[0],
                Doctor = Doctors[0],
                Room = Rooms[0],
                Patient = Patients[0],
                TimeInterval = new TimeInterval()
                {
                    Start = today.AddHours(10),
                    End = today.AddHours(10) + ProcedureTypes[0].Duration
                }
            });
            Examinations[today].Add(new Examination
            {
                ProcedureType = ProcedureTypes[0],
                Doctor = Doctors[0],
                Room = Rooms[1],
                Patient = Patients[2],
                TimeInterval = new TimeInterval()
                {
                    Start = today.AddHours(12),
                    End = today.AddHours(12) + ProcedureTypes[0].Duration
                }
            });
            Examinations[today].Add(new Examination
            {
                ProcedureType = ProcedureTypes[1],
                Doctor = Doctors[1],
                Room = Rooms[1],
                Patient = Patients[1],
                TimeInterval = new TimeInterval()
                {
                    Start = DateTime.Now,
                    End = DateTime.Now + ProcedureTypes[1].Duration
                }
            });

            DateTime yesterday = today - new TimeSpan(24, 0, 0);
            Examinations[yesterday] = new List<Examination>();
            Examinations[yesterday].Add(new Examination
            {
                ProcedureType = ProcedureTypes[0],
                Doctor = Doctors[0],
                Room = Rooms[1],
                Patient = Patients[2],
                TimeInterval = new TimeInterval()
                {
                    Start = yesterday.AddHours(9),
                    End = yesterday.AddHours(9) + ProcedureTypes[0].Duration
                }
            });
            Examinations[yesterday].Add(new Examination
            {
                ProcedureType = ProcedureTypes[0],
                Doctor = Doctors[0],
                Room = Rooms[0],
                Patient = Patients[0],
                TimeInterval = new TimeInterval()
                {
                    Start = yesterday.AddHours(11),
                    End = yesterday.AddHours(11) + ProcedureTypes[0].Duration
                }
            });
            Examinations[yesterday].Add(new Examination
            {
                ProcedureType = ProcedureTypes[1],
                Doctor = Doctors[1],
                Room = Rooms[1],
                Patient = Patients[1],
                TimeInterval = new TimeInterval()
                {
                    Start = yesterday.AddHours(17),
                    End = yesterday.AddHours(17) + ProcedureTypes[1].Duration
                }
            });

            DateTime tomorrow = today + new TimeSpan(24, 0, 0);
            Examinations[tomorrow] = new List<Examination>();
            Examinations[tomorrow].Add(new Examination
            {
                ProcedureType = ProcedureTypes[1],
                Doctor = Doctors[1],
                Room = Rooms[1],
                Patient = Patients[1],
                TimeInterval = new TimeInterval()
                {
                    Start = tomorrow.AddHours(8),
                    End = tomorrow.AddHours(8) + ProcedureTypes[0].Duration
                }
            });
            Examinations[tomorrow].Add(new Examination
            {
                ProcedureType = ProcedureTypes[0],
                Doctor = Doctors[0],
                Room = Rooms[1],
                Patient = Patients[2],
                TimeInterval = new TimeInterval()
                {
                    Start = tomorrow.AddHours(15),
                    End = tomorrow.AddHours(15) + ProcedureTypes[0].Duration
                }
            });
            Examinations[tomorrow].Add(new Examination
            {
                ProcedureType = ProcedureTypes[0],
                Doctor = Doctors[0],
                Room = Rooms[0],
                Patient = Patients[0],
                TimeInterval = new TimeInterval()
                {
                    Start = tomorrow.AddHours(17),
                    End = tomorrow.AddHours(17) + ProcedureTypes[1].Duration
                }
            });
            #endregion

            #region Surgeries
            Surgeries = new Dictionary<DateTime, List<Surgery>>();

            Surgeries[today] = new List<Surgery>();
            Surgeries[today].Add(new Surgery
            {
                ProcedureType = ProcedureTypes[3],
                Doctor = Doctors[3],
                Room = Rooms[3],
                Patient = Patients[0],
                TimeInterval = new TimeInterval()
                {
                    Start = today.AddHours(15),
                    End = today.AddHours(15) + ProcedureTypes[3].Duration
                }
            });
            Surgeries[today].Add(new Surgery
            {
                ProcedureType = ProcedureTypes[3],
                Doctor = Doctors[3],
                Room = Rooms[3],
                Patient = Patients[2],
                TimeInterval = new TimeInterval()
                {
                    Start = today.AddHours(20),
                    End = today.AddHours(20) + ProcedureTypes[3].Duration
                }
            });
            #endregion

            #region Hospitalizations
            
            HospitalizationTypes = new List<HospitalizationType>();
            HospitalizationTypes.Add(new HospitalizationType()
            {
                Name = "Jednodnevno",
                UsualNumberOfDays = 1
            });
            HospitalizationTypes.Add(new HospitalizationType()
            {
                Name = "Postoperativno",
                UsualNumberOfDays = 3
            });
            HospitalizationTypes.Add(new HospitalizationType()
            {
                Name = "Infektološko",
                UsualNumberOfDays = 14
            });

            Hospitalizations = new List<Hospitalization>();
            Hospitalizations.Add(new Hospitalization()
            {
                Patient = Patients[0],
                Room = Rooms[6],
                HospitalizationType = HospitalizationTypes[0],
                TimeInterval = new TimeInterval()
                {
                    Start = DateTime.Now.Date,
                    End = DateTime.Now.Date
                }
            });
            Hospitalizations.Add(new Hospitalization()
            {
                Patient = Patients[1],
                Room = Rooms[6],
                HospitalizationType = HospitalizationTypes[1],
                TimeInterval = new TimeInterval()
                {
                    Start = DateTime.Now.Date.AddDays(1),
                    End = DateTime.Now.Date.AddDays(3)
                }
            });
            Hospitalizations.Add(new Hospitalization()
            {
                Patient = Patients[2],
                Room = Rooms[7],
                HospitalizationType = HospitalizationTypes[2],
                TimeInterval = new TimeInterval()
                {
                    Start = DateTime.Now.Date.AddDays(-3),
                    End = DateTime.Now.Date.AddDays(10)
                }
            });

            #endregion
        }
    }
}
