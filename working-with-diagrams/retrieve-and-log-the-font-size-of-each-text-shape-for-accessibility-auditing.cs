using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: verify the file exists before attempting to load.
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
                // Manual page counter because Page does not expose an Index property.
                int pageNumber = 0;

                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    pageNumber++;

                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Ensure the shape contains non‑empty text.
                        if (shape.Text == null || string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                            continue;

                        // Log basic shape identification.
                        Console.WriteLine($"Page: {pageNumber}, Shape ID: {shape.ID}");

                        // Iterate over each character run to retrieve font size.
                        for (int i = 0; i < shape.Chars.Count; i++)
                        {
                            // Use fully qualified type to avoid ambiguity with System.Char.
                            Aspose.Diagram.Char ch = shape.Chars[i];

                            // Retrieve the font size (in points) from the character run.
                            double fontSize = ch.Size.Value;

                            // Log the character index and its font size.
                            Console.WriteLine($"  Char Index: {i}, Font Size: {fontSize} pt");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}