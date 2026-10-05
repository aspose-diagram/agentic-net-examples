using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <path-to-vdx-file>");
            return;
        }

        string vdxPath = args[0];

        // Guard: verify the file exists before attempting to load
        if (!File.Exists(vdxPath))
        {
            Console.Error.WriteLine($"Error: File not found - {vdxPath}");
            return;
        }

        Diagram diagram = null;
        try
        {
            // Load the existing VDX file using the Diagram constructor
            diagram = new Diagram(vdxPath);

            // Validate the page count
            int pageCount = diagram.Pages.Count;

            Console.WriteLine($"Diagram loaded successfully. Page count: {pageCount}");

            // Example validation: ensure there is at least one page
            if (pageCount == 0)
            {
                Console.Error.WriteLine("Error: The diagram contains no pages.");
                // Optionally, throw to indicate failure
                // throw new Exception("Diagram contains no pages.");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"An error occurred while processing the diagram: {ex.Message}");
        }
        finally
        {
            // Dispose the diagram if it implements IDisposable
            if (diagram != null)
            {
                try
                {
                    diagram.Dispose();
                }
                catch
                {
                    // Ignored - disposal failure should not crash the program
                }
            }
        }
    }
}