using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Module8.Task4
{

    [Serializable]
    public class Students
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Assessment { get; set; }

        public Students(string name, string group, DateTime dateOfBirth, decimal assessment)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateOfBirth;
            Assessment = assessment;
        }
    }

    public class Programm
    {
        public static void Main(string[] args)
        {
            using (var filestud = new FileStream("C:/Users/kolesnikov_aa/desktop/8Module/Task4/Students.dat", FileMode.OpenOrCreate))   // Место расположения файла

            if (File.Exists("" + filestud))                                                                                             // Проверка, есть ли файл по адресу
            {
                List<string> ListGroup = new List<string>();                                                                            // Если есть, идем дальше
                string Groups = "";

                BinaryFormatter formatter = new BinaryFormatter();
                var students = (Students[])formatter.Deserialize(filestud);                                                             // Считываем данные из файла

                for (int i = 0; i < students.Count(); i++)                                                                              
                {
                    if (Groups.Contains(students[i].Group + ",") == false)                                                              // Если групп не было, формируем
                    {
                        ListGroup.Add(students[i].Group);
                        Groups = Groups + students[i].Group + ",";
                    }
                }

                for (int g = 0; g < ListGroup.Count; g++)
                {
                    Directory.CreateDirectory("C:/Users/kolesnikov_aa/desktop/8Module/Task4/Groups");                                   // Задаем адрес, где создавать папки групп
                }
                
                for (int g = 0; g < ListGroup.Count; g++)                                                                               // Записываем группы в файлы
                {
                    StreamWriter streamWriter = new StreamWriter("C:/Users/kolesnikov_aa/desktop/8Module/Task4/Groups" + ListGroup[g] + ".txt");
                    for (int i = 0; i < students.Count(); i++)
                        if (students[i].Group == ListGroup[g])
                            streamWriter.WriteLine(students[i].Name + "\t" + students[i].DateOfBirth + "\t" + students[i].Assessment);

                    streamWriter.Close();
                }
            }
        }
    }
}