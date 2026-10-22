using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input folder and output folder
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Validate input folder
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            try
            {
                Directory.CreateDirectory(outputFolder);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create output folder: {ex.Message}");
                return;
            }
        }

        // Process each Visio file in the input folder
        string[] files = Directory.GetFiles(inputFolder, "*.vsdx");
        foreach (string filePath in files)
        {
            // Guard against missing file (should not happen after GetFiles, but added per rules)
            if (!File.Exists(filePath))
            {
                Console.Error.WriteLine($"File not found: {filePath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block as required
                using (Diagram diagram = new Diagram(filePath))
                {
                    // Iterate through all pages
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through all shapes on the page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip deleted shapes
                            if (shape.Del == BOOL.True)
                                continue;

                            // Create a new geometry template (simple rectangle matching shape size)
                            Geom geom = new Geom();

                            // Move to the top‑left corner (0,0)
                            MoveTo move = new MoveTo();
                            move.X.Value = 0;
                            move.Y.Value = 0;
                            geom.CoordinateCol.Add(move);

                            // Line to top‑right corner (Width,0)
                            LineTo lineTopRight = new LineTo();
                            lineTopRight.X.Value = shape.XForm.Width.Value;
                            lineTopRight.Y.Value = 0;
                            geom.CoordinateCol.Add(lineTopRight);

                            // Line to bottom‑right corner (Width,Height)
                            LineTo lineBottomRight = new LineTo();
                            lineBottomRight.X.Value = shape.XForm.Width.Value;
                            lineBottomRight.Y.Value = shape.XForm.Height.Value;
                            geom.CoordinateCol.Add(lineBottomRight);

                            // Line to bottom‑left corner (0,Height)
                            LineTo lineBottomLeft = new LineTo();
                            lineBottomLeft.X.Value = 0;
                            lineBottomLeft.Y.Value = shape.XForm.Height.Value;
                            geom.CoordinateCol.Add(lineBottomLeft);

                            // Close the path by returning to the start point (0,0)
                            LineTo closeLine = new LineTo();
                            closeLine.X.Value = 0;
                            closeLine.Y.Value = 0;
                            geom.CoordinateCol.Add(closeLine);

                            // Add the new geometry to the shape
                            shape.Geoms.Add(geom);
                        }
                    }

                    // Prepare output file path
                    string fileName = Path.GetFileName(filePath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the modified diagram in VSDX format
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{filePath}': {ex.Message}");
            }
        }
    }
}
