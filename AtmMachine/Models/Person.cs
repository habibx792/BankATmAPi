using AtmMachine.Classes;
using System.ComponentModel.DataAnnotations;

namespace AtmMachine.Models
{
    public class Person:BaseModel
    {
        [MaxLength(100)]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age { get; set; }
        public string CNIC { get; set; }
        public Account? Account { get; set; }
        public Person() { }
        public Person(string firstName,string lastName,string fatherName, DateTime dateOfBirth)
        {
            FirstName = firstName;
            LastName = lastName;
            FatherName = fatherName;
            DateOfBirth = dateOfBirth;
            Age = DateTime.Now.Year - DateOfBirth.Year;
        }
        public bool SetCnic(string cnic)
        {
            if (AccountHandler.isValidChic(cnic))
            {
                this.CNIC = CNIC;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Name : {FirstName} + {LastName} \nAge : {Age} \nFatherName : {FatherName}\n Age :{Age}";
        }

    }
}
