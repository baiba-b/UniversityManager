using System.ComponentModel.DataAnnotations;
using System.Reflection.Emit;
using System.Xml.Serialization;

namespace UniversityPages
{
    [XmlInclude(typeof(Student))] //kods ņemts no kursa prezentācijas
    [XmlInclude(typeof(Teacher))]
    public abstract class Person
    {
        [Key]
        public int Id { get; set; }

        private string? name;
        public string? Name
        {
            get { return name; }
            //Ja vērtība nav tukša tai tiek iestatīta jauna vērtība, citādi paliek vecā
            set { if (value != "") name = value; }
        }

        private string? surname;
        public string? Surname
        {
            get { return surname; }
            set { surname = value; }
        }
        public string? FullName
        {
            get { return $"{name} {surname}"; } // Read-only uzstāda
        }
        public Gender PersonGender { get; set; } = Gender.Woman;
        public Person(string name, string surname, Gender gender)
        {
            Name = name;
            Surname = surname;
            PersonGender = gender;
        }
        public Person()
        {

        }
        public override string? ToString() //atgriež visu īpašību vērtības kā string. 
        {
            return $"name: {Name}, surname: {Surname}, full name: {FullName}, gender: {PersonGender}";
        }
    }
}