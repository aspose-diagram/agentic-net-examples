using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // Enumerate all members of the PrintPageOrientationValue enum using reflection.
        try
        {
            // Get the enum type.
            Type enumType = typeof(PrintPageOrientationValue);

            // Retrieve all defined values.
            Array enumValues = Enum.GetValues(enumType);

            Console.WriteLine("PrintPageOrientationValue members:");
            foreach (PrintPageOrientationValue value in enumValues)
            {
                // Output the name and its underlying integer value.
                Console.WriteLine($"{value} = {(int)value}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error enumerating PrintPageOrientationValue: {ex.Message}");
        }
    }
}