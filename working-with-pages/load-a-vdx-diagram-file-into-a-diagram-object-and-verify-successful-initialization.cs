using System.IO;
using Aspose.Diagram;
using System;

class Program
{
    static void Main()
    {
        try
        {

            // Load the VDX diagram file into a Diagram object
            Diagram diagram = new Diagram("sample.vdx");

            // Verify successful initialization
            if (diagram != null && diagram.Pages.Count > 0)
            {
                Console.WriteLine("Diagram loaded successfully. Page count: " + diagram.Pages.Count);
            }
            else
            {
                Console.WriteLine("Failed to load diagram or diagram contains no pages.");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
