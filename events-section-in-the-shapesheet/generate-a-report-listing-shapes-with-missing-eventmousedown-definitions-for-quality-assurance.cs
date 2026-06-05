using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Get the Visio file path from command line arguments or prompt the user.
        string filePath;
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            filePath = args[0];
        }
        else
        {
            Console.Write("Enter the path to the Visio file: ");
            filePath = Console.ReadLine();
        }

        if (string.IsNullOrWhiteSpace(filePath))
        {
            Console.WriteLine("No file path provided. Exiting.");
            return;
        }

        if (!File.Exists(filePath))
        {
            Console.Error.WriteLine($"File not found: {filePath}");
            return;
        }

        // Load the diagram.
        Diagram diagram;
        try
        {
            diagram = new Diagram(filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to load diagram: {ex.Message}");
            return;
        }

        Console.WriteLine("Shapes missing EventDblClick definitions:");
        Console.WriteLine("-------------------------------------------------");

        // Iterate through all pages and shapes.
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // Ensure the shape is not marked as deleted.
                if (shape.Del == BOOL.True)
                    continue;

                // Check if the EventDblClick formula is empty or not set.
                bool missingEvent = false;
                try
                {
                    string formula = shape.Event.EventDblClick.Ufe.F;
                    if (string.IsNullOrWhiteSpace(formula))
                    {
                        missingEvent = true;
                    }
                }
                catch
                {
                    // If accessing the cell throws, treat it as missing.
                    missingEvent = true;
                }

                if (missingEvent)
                {
                    // Output shape identification details.
                    string shapeName = !string.IsNullOrWhiteSpace(shape.Name) ? shape.Name : "(no name)";
                    Console.WriteLine($"Page: {page.Name} | Shape ID: {shape.ID} | Name: {shapeName}");
                }
            }
        }

        Console.WriteLine("Report generation completed.");
    }
}