using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the saved diagram file
            Diagram diagram = new Diagram("input.vsdx");

            // Retrieve the embedded Aspose.Diagram API version information
            string apiVersion = diagram.Version;

            // Output the version for audit purposes
            Console.WriteLine("Embedded Aspose.Diagram version: " + apiVersion);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
