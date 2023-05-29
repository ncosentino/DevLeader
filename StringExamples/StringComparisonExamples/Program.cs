string myString1 = null;
string myString2 = "HEllo";

bool areEqual_EqualityOperator = 
    myString1 == myString2; // false
//bool areEqual_EqualsMethod = myString1.Equals(
//    myString2, 
//    StringComparison.OrdinalIgnoreCase); 
bool areEqual_EqualsMethod = string.Equals(
    myString1,
    myString2,
    StringComparison.Ordinal);
bool areEqual_CompareMethod = string.Compare(
    myString1, 
    myString2,
    StringComparison.OrdinalIgnoreCase) == 0;