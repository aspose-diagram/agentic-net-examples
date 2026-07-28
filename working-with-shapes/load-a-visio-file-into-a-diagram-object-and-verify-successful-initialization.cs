using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be loaded
            string visioFilePath = @"C:\Path\To\Your\Diagram.vsdx";

            // Load the Visio file into a Diagram object using the constructor that accepts a file name
            using (Diagram diagram = new Diagram(visioFilePath))
            {
                // Verify that the diagram was initialized successfully
                bool isInitialized = diagram != null && diagram.Pages != null && diagram.Pages.Count > 0;

                // Output the verification result
                Console.WriteLine(isInitialized
                    ? "Diagram loaded successfully and contains pages."
                    : "Failed to load diagram or diagram contains no pages.");
            }

        }
        catch (System.IO.DirectoryNotFoundException ex)
        {
            Console.Error.WriteLine($"[DirectoryNotFoundException] {ex.Message}");
        }
    }
}
