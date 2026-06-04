using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VSD diagram from file (lifecycle rule: Diagram(string))
            Diagram diagram = new Diagram("input.vsd");

            // Save the diagram as a JPEG image (lifecycle rule: Save(string, SaveFileFormat))
            diagram.Save("output.jpg", SaveFileFormat.Jpeg);

            // Release resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
