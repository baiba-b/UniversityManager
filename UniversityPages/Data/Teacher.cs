namespace UniversityPages;

public class Teacher : Person
{ 
    public string? ContractDate { get; set; }
    public Teacher() { }
    public Teacher(string name, string surname, Gender gender, string contractdate) : base(name, surname, gender)
    {
        ContractDate = contractdate;

    }
    public override string ToString()
    {
        return "Teacher - "+base.ToString() + ", contract date:" + ContractDate;
    }

}
