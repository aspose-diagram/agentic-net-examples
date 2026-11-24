using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input CSV path and output VSDX path
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputCsvPath> <outputVsdxPath>");
            return;
        }

        string csvPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the CSV file exists
        if (!File.Exists(csvPath))
        {
            Console.Error.WriteLine($"Error: CSV file not found at '{csvPath}'.");
            return;
        }

        try
        {
            // Read all lines from the CSV file
            string[] lines = File.ReadAllLines(csvPath);

            // Each non‑empty line represents a lane name
            var laneNames = new System.Collections.Generic.List<string>();
            foreach (string line in lines)
            {
                string trimmed = line.Trim();
                if (!string.IsNullOrEmpty(trimmed))
                {
                    laneNames.Add(trimmed);
                }
            }

            // Create a new empty diagram inside a using block (mandatory)
            using (Diagram diagram = new Diagram())
            {
                // Add a single page to the diagram
                Page page = new Page();
                diagram.Pages.Add(page);

                // Retrieve page dimensions (in inches)
                double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                // Determine lane height based on the number of lanes
                int laneCount = laneNames.Count;
                if (laneCount == 0)
                {
                    Console.Error.WriteLine("Error: No lane definitions found in CSV.");
                    return;
                }

                double laneHeight = pageHeight / laneCount;

                // Iterate over lane definitions and draw each lane as a rectangle
                for (int i = 0; i < laneCount; i++)
                {
                    string laneName = laneNames[i];

                    // Calculate the center Y coordinate for the current lane
                    // Visio's Y axis grows upwards; PinY is the center of the shape
                    double centerY = pageHeight - (i * laneHeight) - (laneHeight / 2.0);
                    double centerX = pageWidth / 2.0; // Center horizontally

                    // Draw the rectangle representing the lane
                    // Parameters: pinX, pinY, width, height
                    long shapeIdLong = page.DrawRectangle(centerX, centerY, pageWidth, laneHeight);
                    int shapeId = (int)shapeIdLong;

                    // Retrieve the shape object to modify its text
                    Shape laneShape = page.Shapes.GetShape(shapeId);
                    if (laneShape == null)
                    {
                        Console.Error.WriteLine($"Warning: Unable to retrieve shape for lane '{laneName}'.");
                        continue;
                    }

                    // Clear any existing text and add the lane name
                    laneShape.Text.Value.Clear();
                    laneShape.Text.Value.Add(new Txt(laneName));

                    // Optional: set the shape's fill color to a light gray (using the FillForegnd cell)
                    // The FillForegnd cell expects a color value in the format "RGB(r,g,b)"
                    laneShape.Fill.FillForegnd.Value = "RGB(200,200,200)";

                    // Optional: set the shape's line weight (thickness) to 0.02 inches
                    laneShape.Line.LineWeight.Value = 0.02;
                }

                // Save the diagram to the specified VSDX file
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Swimlane diagram successfully saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}