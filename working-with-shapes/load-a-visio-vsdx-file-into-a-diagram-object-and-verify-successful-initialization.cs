using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio VSDX file
            string filePath = "sample.vsdx";

            // Load the diagram using the constructor that accepts a file name
            Diagram diagram = new Diagram(filePath);

            // Verify that the diagram was loaded successfully
            bool isInitialized = diagram != null && diagram.Pages != null && diagram.Pages.Count > 0;

            Console.WriteLine(isInitialized
                ? "Diagram loaded successfully with {0} page(s).".Replace("{0}", diagram.Pages.Count.ToString())
                : "Failed to load diagram.");

            // Clean up resources
            diagram.Dispose();

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
