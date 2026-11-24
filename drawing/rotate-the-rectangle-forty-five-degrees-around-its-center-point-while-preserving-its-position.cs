using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Determine input and output file paths (command‑line args or defaults)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required by the API
            using (Diagram diagram = new Diagram(inputPath))
            {
                Shape rectangleShape = null;
                Page targetPage = null;

                // Search all pages for a non‑deleted rectangle master shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Identify rectangle master
                        if (shape.Master != null && shape.Master.Name == "Rectangle")
                        {
                            rectangleShape = shape;
                            targetPage = page;
                            break;
                        }
                    }
                    if (rectangleShape != null)
                        break;
                }

                // If no rectangle was found, add one to the first page
                if (rectangleShape == null)
                {
                    // Ensure there is at least one page
                    if (diagram.Pages.Count == 0)
                    {
                        Console.Error.WriteLine("The diagram contains no pages; cannot add a rectangle.");
                        return;
                    }

                    targetPage = diagram.Pages[0];

                    // Define centre position (in inches) for the new rectangle
                    double pinX = 2.0;
                    double pinY = 2.0;
                    string masterName = "Rectangle";

                    // Add the rectangle shape; isCalculate = false (no immediate layout)
                    long shapeId = targetPage.AddShape(pinX, pinY, masterName, false);

                    // Retrieve the newly added shape
                    rectangleShape = targetPage.Shapes.GetShape((int)shapeId);
                }

                // Rotate the rectangle 45 degrees around its centre
                rectangleShape.SetAngle(45.0);

                // Save the modified diagram in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}