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
            Console.Error.WriteLine("Please provide the path to the Visio diagram file as the first argument.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check if the shape contains text
                    if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                    {
                        // Iterate over each character run to retrieve font size
                        for (int i = 0; i < shape.Chars.Count; i++)
                        {
                            Aspose.Diagram.Char ch = shape.Chars[i];
                            double fontSize = ch.Size.Value; // Font size in points

                            // Log the font size information
                            Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}, Shape Name: {shape.Name}, Char Index: {i}, Font Size: {fontSize} pt");
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}