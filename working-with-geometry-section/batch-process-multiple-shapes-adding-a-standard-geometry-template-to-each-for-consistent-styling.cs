using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputFolder> <outputFolder>");
            return;
        }

        string inputFolder = args[0];
        string outputFolder = args[1];

        // Verify input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.Error.WriteLine($"Input folder does not exist: {inputFolder}");
            return;
        }

        // Create output folder if it does not exist.
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

        // Get all VSDX files in the input folder.
        string[] visioFiles = Directory.GetFiles(inputFolder, "*.vsdx");
        if (visioFiles.Length == 0)
        {
            Console.WriteLine("No Visio files found in the input folder.");
            return;
        }

        foreach (string inputPath in visioFiles)
        {
            // Guard against missing file (should not happen after GetFiles, but added per rules).
            if (!File.Exists(inputPath))
            {
                Console.Error.WriteLine($"File not found: {inputPath}");
                continue;
            }

            try
            {
                // Load the diagram inside a using block (mandatory pattern).
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Iterate through each page.
                    foreach (Page page in diagram.Pages)
                    {
                        // Iterate through each shape on the page.
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip logically deleted shapes.
                            if (shape.Del == BOOL.True)
                                continue;

                            // Clear existing geometry sections.
                            shape.Geoms.Clear();

                            // Create a new geometry representing a unit rectangle (0,0)-(1,1).
                            Geom rectGeom = new Geom();

                            // Move to the starting point (0,0).
                            MoveTo move = new MoveTo();
                            move.X.Value = 0.0;
                            move.Y.Value = 0.0;
                            rectGeom.CoordinateCol.Add(move);

                            // Line to (1,0).
                            LineTo line1 = new LineTo();
                            line1.X.Value = 1.0;
                            line1.Y.Value = 0.0;
                            rectGeom.CoordinateCol.Add(line1);

                            // Line to (1,1).
                            LineTo line2 = new LineTo();
                            line2.X.Value = 1.0;
                            line2.Y.Value = 1.0;
                            rectGeom.CoordinateCol.Add(line2);

                            // Line to (0,1).
                            LineTo line3 = new LineTo();
                            line3.X.Value = 0.0;
                            line3.Y.Value = 1.0;
                            rectGeom.CoordinateCol.Add(line3);

                            // Close the rectangle by returning to (0,0).
                            LineTo line4 = new LineTo();
                            line4.X.Value = 0.0;
                            line4.Y.Value = 0.0;
                            rectGeom.CoordinateCol.Add(line4);

                            // Add the new geometry to the shape.
                            shape.Geoms.Add(rectGeom);
                        }
                    }

                    // Prepare output file path.
                    string fileName = Path.GetFileName(inputPath);
                    string outputPath = Path.Combine(outputFolder, fileName);

                    // Save the modified diagram in VSDX format.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);

                    Console.WriteLine($"Processed and saved: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error processing file '{inputPath}': {ex.Message}");
            }
        }
    }
}