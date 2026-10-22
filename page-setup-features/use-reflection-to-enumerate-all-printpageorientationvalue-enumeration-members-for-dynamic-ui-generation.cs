using System;
using System.IO;
using System.Reflection;
using Aspose.Diagram;
using Aspose.Diagram.Printing;

class Program
{
    static void Main(string[] args)
    {
        // No input files are required for this task, so no file existence checks are needed.

        try
        {
            // Obtain the Type object for the PrintPageOrientationValue enum using reflection.
            Type enumType = typeof(PrintPageOrientationValue);

            // Retrieve all public static fields of the enum (these represent the enum members).
            FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);

            Console.WriteLine("PrintPageOrientationValue members:");

            // Iterate over each field and output its name.
            foreach (FieldInfo field in fields)
            {
                // The Name property gives the enum member name.
                Console.WriteLine($"- {field.Name}");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}