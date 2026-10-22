using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block (required pattern)
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Work with the first page
                Page page = diagram.Pages[0];

                // Collect distinct fill foreground colors from all non-deleted shapes
                HashSet<string> colorSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the fill foreground color if available
                    string fillColor = shape.Fill?.FillForegnd?.Value;
                    if (!string.IsNullOrEmpty(fillColor))
                    {
                        colorSet.Add(fillColor);
                    }
                }

                // Build and add legend shape if colors were found
                if (colorSet.Count == 0)
                {
                    Console.WriteLine("No colored shapes found; legend will not be added.");
                }
                else
                {
                    // Construct legend text
                    string legendText = "Colors used in diagram:\n";
                    foreach (string color in colorSet)
                    {
                        legendText += $"{color}\n";
                    }

                    // Define legend shape position and size (adjust as needed)
                    double pinX = 1.0;   // Horizontal position (in inches)
                    double pinY = 10.0;  // Vertical position (in inches)
                    double width = 2.5;  // Width of the text box (in inches)
                    double height = 0.5 * colorSet.Count + 0.5; // Height based on number of lines

                    // Add a text shape to the page; AddText returns a Shape object
                    Shape legendShape = page.AddText(pinX, pinY, width, height, legendText);

                    // Optional formatting for the legend shape
                    if (legendShape != null)
                    {
                        // Make the text block background fully opaque
                        legendShape.TextBlock.TextBkgndTrans.Value = 0.0; // 0% transparent (fully opaque)

                        // Set a solid white background for the legend shape
                        legendShape.Fill.FillPattern.Value = 1; // Solid fill
                        legendShape.Fill.FillForegnd.Value = "#FFFFFF"; // White background
                    }
                }

                // Save the modified diagram with explicit format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}