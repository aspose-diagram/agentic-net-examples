using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source VSDX file (replace with your actual file path)
        string sourcePath = "input.vsdx";
        if (!System.IO.File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        // Guard: ensure the source file exists before proceeding
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"File not found: {sourcePath}");
            return;
        }

        try
        {
            // Read the VSDX file into a memory stream
            byte[] fileBytes = File.ReadAllBytes(sourcePath);
            using (MemoryStream memoryStream = new MemoryStream(fileBytes))
            {
                // Initialize the Diagram object directly from the memory stream
                using (Diagram diagram = new Diagram(memoryStream))
                {
                    // If we reach this point, parsing succeeded
                    Console.WriteLine("Diagram loaded successfully.");

                    // Output basic information to confirm the diagram content
                    Console.WriteLine($"Number of pages: {diagram.Pages.Count}");

                    // Iterate through pages and display their IDs and names
                    foreach (Page page in diagram.Pages)
                    {
                        string pageName = page.Name ?? "(no name)";
                        Console.WriteLine($"Page ID: {page.ID}, Name: {pageName}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Capture any errors from Aspose.Diagram operations
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
        }
    }
}