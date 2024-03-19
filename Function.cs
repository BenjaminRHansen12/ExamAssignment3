double testNumber = 5;
double testNumberThing = AreaOfACircle(testNumber);
Console.WriteLine($"From {testNumber} to {testNumberThing}");

//Functions
//Square of a number
static int SquareOfANumber(int number)
{
    return number * number;
}

//From inches to mm
static double InchesToMM(double inches)
{
    return inches * 25.4;
}

//Root of a number
//static double RootOfANumber(double number) // do it later
//{
    //return;
//}

//Cube of a number
static int CubeOfANumber(int number)
{
    return number * number * number;
}

//Area of a circle given radius
static double AreaOfACircle(double radius)
{
    return 3.14 * (radius * radius);
}

//Greeting given a name
static string GreetingSomeone(string name)
{
    return $"Greetings, {name}";
}