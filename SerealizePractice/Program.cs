using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

/*
Сериализация объектов.

Из csv файла (имя; фамилия; телефон; год рождения) прочитать записи в коллекцию.
Каждая запись коллекции должна иметь тип Person.
Сериализовать коллекцию с помощью класса SoapFormatter и сохранить в файл.
Самостоятельно рассмотреть библиотеку Newtonsoft.Json и сериализовать коллекцию в json файл.

    
    
     
Создать библиотеку классов с именем «ClassLib». 
В библиотеке «ClassLib» создать класс с именем «РС», описывающий компьютер. Данный класс должен включать:  
3–4 поля (марка, серийный номер и т.д.),  
свойства (к каждому полю),  
конструкторы (по умолчанию, с параметрами),  
методы, моделирующие функционирование ПЭВМ (включение/выключение, перегрузку). 
Создать новый проект (тип — консольное приложение) с именем «SerializConsolApp».
Добавить ссылку на библиотеку «ClassLib».

В функции Main() данного проекта создать коллекцию (на базе обобщенного класса List<T> )
и добавить в коллекцию 4–5 объектов класса «РС». 

Произвести сериализацию коллекции в бинарный файл с именем «listSerial.txt» в каталоге на диске D.
В случае наличия аналогичного файла в каталоге
старый файл перезаписать новым файлом и вывести об этом уведомление. 

Создать новый проект (тип — консольное приложение) с именем «DeserializConsolApp».
Добавить ссылку на библиотеку «ClassLib».
В функции Main() произвести десериализацию коллекции, созданной в проекте с именем «SerializConsolApp» и вывести на экран.

 */


namespace SerealizePractice
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> contacts = new List<Person>();
            string path = @"C:\FolderToWork\contacts.csv";

            using (StreamReader sr = new StreamReader(path))
            {
                string allFileText = sr.ReadToEnd();
                string[] wordsInText = allFileText.Split(';');
                int dataSize = 4;

                for (int i = 0; i < wordsInText.Length-1; i += dataSize)
                {
                    Person personInText = new Person();
                    personInText.Name = wordsInText[i];
                    personInText.Surname = wordsInText[i + 1];
                    personInText.Telephone = wordsInText[i + 2];
                    personInText.BirthYear = int.Parse(wordsInText[i + 3]);
                    contacts.Add(personInText);
                }


            }
            SoapFormatter formatter = new SoapFormatter();

            using (FileStream fs = new FileStream(@"C:\FolderToWork\people.soap", FileMode.OpenOrCreate))
            {
                foreach(var person in contacts)
                {
                    formatter.Serialize(fs, person);
                }
            }


            Console.Read();
        }
    }
}
