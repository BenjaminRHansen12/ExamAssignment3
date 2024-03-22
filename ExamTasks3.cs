using System.Text.Json;

//Task : Functions are a popping-----------------------------------------------
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
PrintLine();

//Task : Flatten those numbers -------------------------------------------------
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
PrintLine();

//Task : Left and right up and down, away we go.---------------------------------
//Calculate the sum of the full structure
string jsonNodes = File.ReadAllText("nodes.json");
JsonElement jsonElement = JsonDocument.Parse(jsonNodes).RootElement;

int sumOfTheCalculatedStructure = CalculateSumOfTheStructure(jsonElement);
Console.WriteLine($"Sum of values on the nodes structure: {sumOfTheCalculatedStructure}");
    
static int CalculateSumOfTheStructure(JsonElement node)
{
    if (node.ValueKind == JsonValueKind.Null)
    {
        return 0;
    }

    int value = node.GetProperty("value").GetInt32();

    JsonElement left = node.GetProperty("left");
    JsonElement right = node.GetProperty("right");
    int leftSum = CalculateSumOfTheStructure(left);
    int rightSum = CalculateSumOfTheStructure(right);

    return value + leftSum + rightSum;
}

//Report the deepest level of the structure
int deepestLevel = CalculateDeepestLevel(jsonElement);
Console.WriteLine($"Deepest level of the nodes structure: {deepestLevel}");

static int CalculateDeepestLevel(JsonElement node)
{
    if (node.ValueKind == JsonValueKind.Null)
    {
        return 0;
    }

    int leftLevel = CalculateDeepestLevel(node.GetProperty("left"));
    int rightLevel = CalculateDeepestLevel(node.GetProperty("right"));

    if (leftLevel > rightLevel)
    {
        return leftLevel + 1;
    }
    else
    {
        return rightLevel + 1;
    }
}

//Report the numbers nodes
JsonElement firstNode = JsonDocument.Parse(jsonNodes).RootElement;
int nodeCount = CountTheAmountOfNodes(firstNode);
Console.WriteLine($"The number of nodes are: {nodeCount}");
    
static int CountTheAmountOfNodes(JsonElement jsonElement)
{
    if (jsonElement.ValueKind == JsonValueKind.Null || jsonElement.ValueKind != JsonValueKind.Object && jsonElement.ValueKind != JsonValueKind.Array)
    {
        return 0;
    }

    int countOfNodes = 1; 
    
    if (jsonElement.ValueKind == JsonValueKind.Object)
    {
        foreach (JsonProperty property in jsonElement.EnumerateObject())
        {
            countOfNodes += CountTheAmountOfNodes(property.Value);
        }
    }
    else if (jsonElement.ValueKind == JsonValueKind.Array)
    {
        foreach (JsonElement element in jsonElement.EnumerateArray())
        {
            countOfNodes += CountTheAmountOfNodes(element);
        }
    }

    return countOfNodes;
}
PrintLine();

//Task : My books they are a mess.-----------------------------------------------
string allTheBooks = File.ReadAllText("books.json");
JsonDocument jsonDocumentBooks = JsonDocument.Parse(allTheBooks);
List<Book> books = ExtractBooks(jsonDocumentBooks.RootElement);

//Return only books starting with 'The'
List<Book> theBooks = FindingBookStartingWithThe(books);
Console.WriteLine("Books starting with 'The':");
foreach (Book book in theBooks)
{
    Console.WriteLine($"{book.Title} ({book.PublicationYear}), by {book.Author}, ISBN: {book.ISBN}");
}

static List<Book> FindingBookStartingWithThe(List<Book> books)
{
    List<Book> theBooks = new List<Book>();

    foreach (Book book in books)
    {
        if (book.Title.StartsWith("The", StringComparison.OrdinalIgnoreCase))
        {
            theBooks.Add(book);
        }
    }

    return theBooks;
}
PrintLine();

//Return only books written by authors with a 't' in their name
List<Book> tInAuthors = FindingBooksWithAuthorTInName(books);
Console.WriteLine("Books written by authors with 't' in their name:");
foreach (Book book in tInAuthors)
{
    Console.WriteLine($"{book.Title} ({book.PublicationYear}), by {book.Author}, ISBN: {book.ISBN}");
}

static List<Book> FindingBooksWithAuthorTInName(List<Book> books)
{
    List<Book> tInAuthors = new List<Book>();

    foreach (Book book in books)
    {
        string authorName = book.Author;

        if (!authorName.Contains("translated by", StringComparison.OrdinalIgnoreCase))
        {
            int indexOfOpenParenthesis = authorName.IndexOf('(');

            if (indexOfOpenParenthesis != -1)
            {
                authorName = authorName.Substring(0, indexOfOpenParenthesis).Trim();
            }

            if (authorName.Contains("t", StringComparison.OrdinalIgnoreCase))
            {
                tInAuthors.Add(book);
            }
        }
    }

    return tInAuthors;
}
PrintLine();

//The number of books written after '1992'
int booksWrittenAfter1992 = CountTheBooksAfter1992(books, 1992);
Console.WriteLine($"The number of book written after 1992: {booksWrittenAfter1992}");

static int CountTheBooksAfter1992(List<Book> books, int year)
{
    int count = 0;
    foreach (Book book in books)
    {
        if(book.PublicationYear > year)
        {
            count++;
        }
    }

    return count;
}
PrintLine();

//The number of books written before '2004'
int booksWrittenBefore2004 = CountTheBooksBefore2004(books, 2004);
Console.WriteLine($"The number of book written before 2004: {booksWrittenBefore2004}");

static int CountTheBooksBefore2004(List<Book> books, int year)
{
    int count = 0;
    foreach (Book book in books)
    {
        if(book.PublicationYear < year)
        {
            count++;
        }
    }

    return count;
}
PrintLine();

//Return the isbn number of all the books for a given author
List<Book> isbnOfTerryPratchett = TheIsbnOfTerryPratchett(books);
Console.WriteLine("The isbn of all Terry Pratchett books:");
foreach (Book book in isbnOfTerryPratchett)
{
    Console.WriteLine($"ISBN: {book.ISBN}");
}

static List<Book> TheIsbnOfTerryPratchett(List<Book> books)
{
    List<Book> isbnOfTerryPratchett = new List<Book>();

    foreach (Book book in books)
    {
        if (book.Author.Contains("Terry Pratchett", StringComparison.OrdinalIgnoreCase))
        {
            isbnOfTerryPratchett.Add(book);
        }
    }

    return isbnOfTerryPratchett;
}
PrintLine();

//List books alphabetically assending
Console.WriteLine("Books sorted by title in ascending order:");
PrintBooks(SortBooksByTitle(books, true));

static List<Book> SortBooksByTitle(List<Book> books, bool ascending)
{
    Comparison<Book> comparer = (book1, book2) => String.Compare(book1.Title, book2.Title);
        
    if (!ascending)
    {
        comparer = (book1, book2) => String.Compare(book2.Title, book1.Title);
    }

    books.Sort(comparer);
    return books;
}

static void PrintBooks(List<Book> books)
{
    foreach (var book in books)
    {
        Console.WriteLine($"{book.Title} ({book.PublicationYear}), by {book.Author}, ISBN: {book.ISBN}");
    }
}
PrintLine();

//List books chronologically assending
Console.WriteLine("Books sorted by publication year ascending order:");
SortBooksByPublicationYearAscending(books);

foreach (Book book in books)
{
    Console.WriteLine($"{book.Title} ({book.PublicationYear}), by {book.Author}, ISBN: {book.ISBN}");
}

static void SortBooksByPublicationYearAscending(List<Book> books)
{
    books.Sort((book1, book2) => book1.PublicationYear.CompareTo(book2.PublicationYear));
}
PrintLine();

//list books grouped by author last name
Console.WriteLine("Books grouped by author last name:");
PrintBooksGroupedByAuthorLastName(GroupBooksByAuthorLastName(books));

static Dictionary<string, List<Book>> GroupBooksByAuthorLastName(List<Book> books)
{
    Dictionary<string, List<Book>> groupedBooks = new Dictionary<string, List<Book>>();
    foreach (var book in books)
    {
        string authorLastName = GetAuthorLastName(book.Author);
        if (!groupedBooks.ContainsKey(authorLastName))
        {
            groupedBooks[authorLastName] = new List<Book>();
        }
        groupedBooks[authorLastName].Add(book);
    }
    return groupedBooks;
}

static string GetAuthorLastName(string author)
{
    int bracketIndex = author.IndexOf('(');
    if (bracketIndex != -1)
    {
        author = author.Substring(0, bracketIndex).Trim();
    }
    string[] authorNameParts = author.Split(' ');
    return authorNameParts[authorNameParts.Length - 1];
}

static void PrintBooksGroupedByAuthorLastName(Dictionary<string, List<Book>> groupedBooks)
{
    foreach (var authorGroup in groupedBooks)
    {
        Console.WriteLine($"Author: {authorGroup.Key}");
        foreach (var book in authorGroup.Value)
        {
            Console.WriteLine($"{book.Title} ({book.PublicationYear}), ISBN: {book.ISBN}");
        }
        Console.WriteLine();
    }
}
PrintLine();

//List books grouped by author first name
Console.WriteLine("Books grouped by author first name:");
Dictionary<string, List<Book>> groupedBooks = GroupBooksByAuthorFirstName(books);
PrintGroupedBooks(groupedBooks);
    
static Dictionary<string, List<Book>> GroupBooksByAuthorFirstName(List<Book> books)
{
    Dictionary<string, List<Book>> groupedBooks = new Dictionary<string, List<Book>>();

    foreach (var book in books)
    {
    string firstName = GetAuthorFirstName(book.Author);
        if (!groupedBooks.ContainsKey(firstName))
        {
            groupedBooks[firstName] = new List<Book>();
        }
        groupedBooks[firstName].Add(book);
    }

    return groupedBooks;
}

static string GetAuthorFirstName(string author)
{
    string[] parts = author.Split(' ');
    return parts[0];
}

static void PrintGroupedBooks(Dictionary<string, List<Book>> groupedBooks)
{
    foreach (var group in groupedBooks)
    {
        Console.WriteLine($"Authors first name \"{group.Key}\":");
        foreach (var book in group.Value)
        {
            Console.WriteLine($"{book.Title} ({book.PublicationYear}), by {book.Author}, ISBN: {book.ISBN}");
        }
        Console.WriteLine();
    }
}

//Book extractor
static List<Book> ExtractBooks(JsonElement jsonElement)
{
    List<Book> books = new List<Book>();

    if (jsonElement.ValueKind == JsonValueKind.Array)
    {
        foreach (JsonElement arrayElement in jsonElement.EnumerateArray())
        {
            string title = arrayElement.GetProperty("title").GetString();
            int publicationYear = arrayElement.GetProperty("publication_year").GetInt32();
            string author = arrayElement.GetProperty("author").GetString();
            string isbn = arrayElement.GetProperty("isbn").GetString();

            Book book = new Book(title, publicationYear, author, isbn);
            books.Add(book);
        }
    }

    return books;
}

static void PrintLine()
{
    const int length = 100;
    for (int i = 0; i < length; i++)
    {
        Console.Write('-');
    }
    Console.WriteLine();
}

public class Book
{
    public string Title { get; }
    public int PublicationYear { get; }
    public string Author { get; }
    public string ISBN { get; }

    public Book(string title, int publicationYear, string author, string isbn)
    {
        Title = title;
        PublicationYear = publicationYear;
        Author = author;
        ISBN = isbn;
    }
}

