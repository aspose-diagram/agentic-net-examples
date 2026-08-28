using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        // Path to the Visio file to be loaded
        string visioFilePath = @"C:\Path\To\Your\Diagram.vsdx";

        // Ensure the file exists before attempting to load
        if (!File.Exists(visioFilePath))
        {
            Console.WriteLine("Visio file not found: " + visioFilePath);
            return;
        }

        // Load the diagram using the constructor that accepts a file name
        Diagram diagram = null;
        try
        {
            diagram = new Diagram(visioFilePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading diagram: " + ex.Message);
            return;
        }

        // Verify that the diagram was parsed successfully
        if (diagram != null && diagram.Pages != null && diagram.Pages.Count > 0)
        {
            Console.WriteLine("Diagram loaded successfully. Page count: " + diagram.Pages.Count);
        }
        else
        {
            Console.WriteLine("Diagram loaded but contains no pages or is invalid.");
        }

        // Dispose the diagram when done
        diagram?.Dispose();
    }
}
