using System.Text.Json;

//Task : Functions are a popping
//Square of a number
int squareOfNumber = 20;
int squareOfNumberTest = SquareOfANumber(squareOfNumber);
Console.WriteLine($"Square: From {squareOfNumber} to {squareOfNumberTest}");

static int SquareOfANumber(int number)
{
    return number * number;
}

//From inches to mm
double inchesTest = 20;
double inchesToMMTest = InchesToMM(inchesTest);
Console.WriteLine($"Inches: From {inchesTest} to {inchesToMMTest}");

static double InchesToMM(double inches)
{
    return inches * 25.4;
}

//Root of a number
double testSquareRoot = 25;
int iterationsNewtonsMethod = 10;
double squareRoot = SquareRoot(testSquareRoot, iterationsNewtonsMethod);
Console.WriteLine($"SqareRoot: From {testSquareRoot} to {squareRoot}");

static double SquareRoot(double number, int iterations)
{
    if (number == 0 || number == 1)
    {
        return number;
    }

    double numberToCalc = number / 2.0; 
    for (int i = 0; i < iterations; i++)
    {
        numberToCalc = (numberToCalc + number / numberToCalc) / 2.0;
    }

    return numberToCalc;
}

//Cube of a number
int cubingNumber = 20;
int cubingNumberTest = CubeOfANumber(cubingNumber);
Console.WriteLine($"Cube: From {cubingNumber} to {cubingNumberTest}");

static int CubeOfANumber(int number)
{
    return number * number * number;
}

//Area of a circle given radius
double radiusTest = 20;
double areaOfCircleTest = AreaOfACircle(radiusTest);
Console.WriteLine($"AreaOfCircle: Radius {radiusTest}, Area {areaOfCircleTest}");

static double AreaOfACircle(double radius)
{
    return 3.14 * (radius * radius);
}

//Greeting given a name
string nameOfPerson = "Benjamin";
string greetingsTest = GreetingSomeone(nameOfPerson);
Console.WriteLine($"{greetingsTest}");

static string GreetingSomeone(string name)
{
    return $"Greetings, {name}";
}


//Task : Flatten those numbers
int[] integersArray = GetIntegersArray("arrays.json");

Console.Write("Array of numbers: [");
foreach (int num in integersArray)
{
    Console.Write(num + ", ");
}
Console.WriteLine("\b\b]");

static int[] GetIntegersArray(string fileName)
{
    List<int> integers = new List<int>();
    JsonDocument jsonDocument;
    using (FileStream fs = File.OpenRead(fileName))
    {
        jsonDocument = JsonDocument.Parse(fs);
    }
    ExtractIntegers(jsonDocument.RootElement, integers);
    return integers.ToArray();
}

static void ExtractIntegers(JsonElement jsonElement, List<int> integers)
{
    if (jsonElement.ValueKind == JsonValueKind.Array)
    {
        foreach (JsonElement arrayElement in jsonElement.EnumerateArray())
        {
            if (arrayElement.ValueKind == JsonValueKind.Array)
            {
                ExtractIntegers(arrayElement, integers);
            }
            else if (arrayElement.ValueKind == JsonValueKind.Number)
            {
                integers.Add(arrayElement.GetInt32());
            }
        }
    }
}


//Task : Left and right up and down, away we go.




//Task : My books they are a mess.

