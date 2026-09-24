using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        // Assign the first argument to a variable.
        string visioPath = args[0];

        // Guard: verify the file exists before proceeding.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(visioPath);

            // Initialize a counter for shapes with a red line.
            int redLineShapeCount = 0;

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the line color value (hex string) of the shape.
                    string lineColor = shape.Line.LineColor.Value;

                    // Compare the line color to red ("#FF0000") ignoring case.
                    if (string.Equals(lineColor, "#FF0000", StringComparison.OrdinalIgnoreCase))
                    {
                        // Increment the counter when a red line is found.
                        redLineShapeCount++;
                    }
                }
            }

            // Output the final count to the console.
            Console.WriteLine($"Number of shapes with a red line: {redLineShapeCount}");
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}