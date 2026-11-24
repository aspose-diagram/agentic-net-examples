using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expected arguments: <inputPath> <outputPath> <tagName> [rotationAngle]
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <inputPath> <outputPath> <tagName> [rotationAngle]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string tagName = args[2];
        double rotationAngle = 45.0; // default rotation angle

        if (args.Length >= 4 && double.TryParse(args[3], out double parsedAngle))
        {
            rotationAngle = parsedAngle;
        }

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    try
                    {
                        // Use the master name as the tag identifier (since Master.Props is not available)
                        if (!string.IsNullOrEmpty(master.Name) && master.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase))
                        {
                            // Iterate over each shape that belongs to the master
                            foreach (Shape shape in master.Shapes)
                            {
                                // Apply 2‑D rotation by setting the Angle cell (value in degrees)
                                shape.XForm.Angle.Value = rotationAngle;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log any issues processing the tag for this master and continue
                        Console.Error.WriteLine($"Warning: Could not process master '{master.Name}'. Details: {ex.Message}");
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}
