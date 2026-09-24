using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the VSDX file on disk
        string filePath = "diagram.vsdx";

        Diagram diagram = null;

        try
        {
            // Load the diagram from the specified file
            diagram = new Diagram(filePath);

            // Verify that the diagram was loaded successfully
            if (diagram != null && diagram.Pages.Count > 0)
            {
                Console.WriteLine("Diagram loaded successfully. Page count: " + diagram.Pages.Count);
            }
            else
            {
                Console.WriteLine("Diagram loaded, but it contains no pages.");
            }
        }
        catch (Exception ex)
        {
            // Handle any errors that occurred during loading
            Console.WriteLine("Error loading diagram: " + ex.Message);
        }
    }
}
