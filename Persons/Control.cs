using System;

namespace Persons
{
    public class Control
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Lesson { get; set; }
        public int Mark { get; set; }
        public bool Presence { get; set; }
        public int ID_student { get; set; }


        public override string ToString()
        {
            return Date + " " + Lesson + " " + Mark + " " + Presence + " " + ID_student;
        }
    }
}
