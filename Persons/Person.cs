namespace Persons
{
    public class Person
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Class { get; set; }
        public int Omissions { get; set; }
        public string Progress { get; set; }


        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Class + " " + Progress + " " + Omissions;
        }
    }
}
