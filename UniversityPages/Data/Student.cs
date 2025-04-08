namespace UniversityPages;

public class Student : Person
{
    public string? StudentIdNumder { get; set; }
    public Student() //1. konstruktors bez parametriem (uzdevuma nosacījumiem)
    {
        
    }
    public Student(string name, string surname, Gender personGender, string studentID): base(name, surname, personGender) // 2. konstruktors ar parametriem, kas tiek mantoti no bāzes klases (uzdevuma nosacījumiem)
    {
        this.StudentIdNumder = studentID;
    }
    

    public override string ToString()
    {
        return "Student - "+base.ToString() +", student ID number: "+StudentIdNumder;
    }

}
