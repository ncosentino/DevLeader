using System.Text;

string myString = "";

string myBigString1 = "asdasdasdasdasdasd..."; // 1000's chars
string myBigString2 = "asdasdasdasdasdasd..."; // 1000's chars

myString = myBigString1 + myBigString2 + "asdasdasd";

StringBuilder myStringBuilder = new StringBuilder();
myStringBuilder.Append(myBigString1);
myStringBuilder.Append(myBigString2);
myString = myStringBuilder.ToString();
