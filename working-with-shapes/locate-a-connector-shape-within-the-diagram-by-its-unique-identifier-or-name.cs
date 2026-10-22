using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: diagram file path and connector identifier or name.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <diagramFilePath> <connectorIdOrName>");
            return;
        }

        string diagramPath = args[0];
        string searchTerm = args[1];

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Determine whether the search term is a numeric ID.
                bool isIdSearch = long.TryParse(searchTerm, out long targetId);

                bool found = false;

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Consider only connector shapes (1‑D shapes).
                        if (!shape.OneD)
                            continue;

                        // Search by ID.
                        if (isIdSearch && shape.ID == targetId)
                        {
                            Console.WriteLine("Connector found by ID:");
                            Console.WriteLine($"Page NameU: {page.NameU}");
                            Console.WriteLine($"Shape ID: {shape.ID}");
                            Console.WriteLine($"Shape NameU: {shape.NameU}");
                            found = true;
                            break;
                        }

                        // Search by NameU (case‑sensitive).
                        if (!isIdSearch && shape.NameU == searchTerm)
                        {
                            Console.WriteLine("Connector found by Name:");
                            Console.WriteLine($"Page NameU: {page.NameU}");
                            Console.WriteLine($"Shape ID: {shape.ID}");
                            Console.WriteLine($"Shape NameU: {shape.NameU}");
                            found = true;
                            break;
                        }
                    }

                    if (found)
                        break;
                }

                if (!found)
                {
                    Console.WriteLine(isIdSearch
                        ? $"No connector with ID {targetId} was found."
                        : $"No connector with name \"{searchTerm}\" was found.");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}