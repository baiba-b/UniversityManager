using System.ComponentModel.DataAnnotations;

namespace UniversityPages;

public class Submission
{
    [Key]
    public int Id { get; set; }
    [Display(Name = "Assignement")]
    public int AssignementId {  get; set; }
    [Display(Name = "Student")]

    public int StudentId { get; set; }
    public DateTime SubmissionTime { get; set; }
    public int Score { get; set; }
    public virtual Assignement? Assignement { get; set; }
    public virtual Student? Student { get; set; }
   
    public Submission(Assignement assignement, Student student, DateTime SubmissionTime, int score)
    {
        this.Assignement = assignement;
        this.Student = student;
        this.SubmissionTime = SubmissionTime;
        this.Score = score;
    }
    public Submission()
    {

    }
    public override string ToString()
    {
        return $"Submission - assignment:[{Assignement}],\n" + //saliku \n, jo teksts nebija pārskatāms
            $" student: [{Student}],\n" +
            $" submissiondate: {SubmissionTime},\n" +
            $" score: [{Score}]";
    }
}
