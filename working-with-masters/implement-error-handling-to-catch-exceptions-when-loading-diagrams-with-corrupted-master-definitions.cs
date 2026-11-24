using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as an argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before attempting to load
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        Diagram diagram = null;

        // Attempt to load the diagram with error handling for corrupted master definitions
        try
        {
            // Load the diagram from the specified file
            diagram = new Diagram(diagramPath);
        }
        catch (Exception ex)
        {
            // Catch any exception that occurs during loading (e.g., corrupted master definitions)
            Console.Error.WriteLine($"Error loading diagram '{diagramPath}': {ex.Message}");
            // Optionally, you could inspect ex.InnerException for more details
            return;
        }

        // If loading succeeded, proceed to work with the diagram
        try
        {
            Console.WriteLine($"Successfully loaded diagram: {diagramPath}");
            Console.WriteLine($"Number of pages: {diagram.Pages.Count}");
            Console.WriteLine($"Number of masters: {diagram.Masters.Count}");

            // Iterate through masters and display their basic information
            foreach (Master master in diagram.Masters)
            {
                // Guard against null master reference (should not happen, but defensive programming)
                if (master == null)
                    continue;

                // Output master details
                Console.WriteLine($"Master ID: {master.ID}, Name: {master.Name}, Hidden: {(master.Hidden == BOOL.True ? "True" : "False")}");
            }
        }
        catch (Exception ex)
        {
            // Catch any unexpected errors during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
        finally
        {
            // Dispose the diagram if it implements IDisposable (Aspose.Diagram.Diagram does)
            if (diagram is IDisposable disposableDiagram)
            {
                disposableDiagram.Dispose();
            }
        }
    }
}