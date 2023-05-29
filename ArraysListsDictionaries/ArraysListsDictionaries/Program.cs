string[] myArray = new string[7];
myArray[0] = "Monday";
myArray[1] = "Tuesday";
myArray[2] = "Wednesday";
myArray[3] = "Thursday";
myArray[4] = "Friday";
myArray[5] = "Saturday";
myArray[6] = "Sunday";

for  (int i = 0; i < myArray.Length; i++)
{
    Console.WriteLine(myArray[i]);
}

List<string> list = new List<string>();
list.Add("Monday");
list.Add("Tuesday");
list.Add("Wednesday");
list.Add("Thursday");
list.Add("Friday");
list.Add("Saturday");
list.Add("Sunday");

for (int i = 0; i < list.Count; i++)
{
    list[i] = list[i].Substring(0, 3);
    Console.WriteLine(list[i]);
}

Dictionary<int, string> dictionary = new Dictionary<int, string>();
dictionary[0] = "Monday";
dictionary[1] = "Tuesday";
dictionary[2] = "Wednesday";
dictionary[3] = "Thursday";
dictionary[4] = "Friday";
dictionary[5] = "Saturday";
//dictionary[6] = "Sunday";
dictionary.Add(6, "Sunday");

for (int i = 0; i < dictionary.Count; i++)
{
    dictionary[i] = dictionary[i].Substring(0, 3);
    Console.WriteLine(dictionary[i]);
}