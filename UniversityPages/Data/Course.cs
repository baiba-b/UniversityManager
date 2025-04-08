using System.ComponentModel.DataAnnotations;

namespace UniversityPages
{

    public class Course
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public Teacher? Teacher { get; set; }

        public Course()
        {

        }
        public Course(string name, Teacher teacher)
        {
            this.Name = name;
            this.Teacher = teacher;
        }
        public override string ToString() //atgriež visu īpašību vērtības kā string. Noņēmu string?
        {
            return $"Course name: {Name}, course teacher: {Teacher}";
        }
    }
}