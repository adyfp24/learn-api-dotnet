using System.Collections.Generic;

namespace HelloDotnet.Models
{
    public class PersonContext
    {
        public List<Person> ListPerson()
        {
            List<Person> list1 = new List<Person>();
            object[,] arrayPerson = new object[,]{
                {1, "Budi", "Jember", "budi@gmail.com" },
                {2, "Iwan", "Banyuwangi", "iwan@gmail.com" },
                {3, "Wati", "Lumajang", "wati@gmail.com" }
            };
            for (int i = 0; i < arrayPerson.GetLength(0); i++)
            {
                list1.Add(new Person()
                {
                    id_person = int.Parse(arrayPerson[i, 0].ToString()),
                    nama = arrayPerson[i, 1].ToString(),
                    alamat = arrayPerson[i, 2].ToString(),
                    email = arrayPerson[i, 3].ToString()
                });
            }
            return list1;
        }

        public Person GetPersonById(int id)
        {
            List<Person> allPersons = ListPerson();
            return allPersons.Find(person => person.id_person == id);
        }
    }
}