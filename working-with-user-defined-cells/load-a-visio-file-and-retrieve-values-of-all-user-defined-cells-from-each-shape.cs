using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Please provide the path to a Visio file as the first argument.");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the input file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(visioPath, LoadFileFormat.Vsdx);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"Page ID: {page.ID}, Name: {page.NameU}");

                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    Console.WriteLine($"  Shape ID: {shape.ID}, Name: {shape.NameU}");

                    // Ensure the Users collection is not null before iterating.
                    if (shape.Users != null)
                    {
                        // Iterate through each user-defined cell.
                        foreach (User user in shape.Users)
                        {
                            // Retrieve the cell's name, value, and prompt (if any).
                            string nameU = user.NameU ?? string.Empty;
                            string value = user.Value?.Val ?? string.Empty;
                            string prompt = user.Prompt?.Value ?? string.Empty;

                            Console.WriteLine($"    User Cell - NameU: {nameU}, Value: {value}, Prompt: {prompt}");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during loading or processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}