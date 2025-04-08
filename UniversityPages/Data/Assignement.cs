using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace UniversityPages;
public class Assignement
{
    [Key]
    public int Id { get; set; }
    //Nebija datu atribūta Date, tādēļ izmantoju DateOnly
    public DateOnly Deadline { get; set; }
    public string? Description { get; set; }
    [Display(Name = "Course")]

    public int? CourseId { get; set; }
    public virtual Course? Course { get; set; }
    
    public Assignement()
    {

    }
    public Assignement(DateOnly deadline, Course course, string description)
    {

        Deadline = deadline;
        Course = course;
        Description = description;

    }
    public override string ToString()
    {
        return $"Assignment - deadline: {Deadline}, description: {Description}, course: [{Course}]";
    }
}
