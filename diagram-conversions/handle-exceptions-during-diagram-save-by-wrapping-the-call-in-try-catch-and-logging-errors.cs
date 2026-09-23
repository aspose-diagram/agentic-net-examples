using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Attempt to save the diagram and handle any exceptions
            try
            {
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Log the error details
                Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
