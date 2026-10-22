using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expecting four arguments: input diagram path, output diagram path, tag to match, rotation angle in degrees
        if (args.Length < 4)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <masterTag> <rotationAngle>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetTag = args[2];
        double rotationAngle;
        if (!double.TryParse(args[3], out rotationAngle))
        {
            Console.Error.WriteLine("Invalid rotation angle.");
            return;
        }

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all masters in the diagram
                foreach (Master master in diagram.Masters)
                {
                    // NOTE: Aspose.Diagram does not expose a Tag collection on Master.
                    // As a practical substitute, we treat the master Name as the tag identifier.
                    if (master != null && master.Name != null && master.Name.Equals(targetTag, StringComparison.OrdinalIgnoreCase))
                    {
                        // Apply rotation to each shape contained in the master.
                        // Masters themselves do not have an XForm; rotation is applied to their constituent shapes.
                        foreach (Shape shape in master.Shapes)
                        {
                            // Set the 2D rotation angle (in degrees) for the shape.
                            shape.XForm.Angle.Value = rotationAngle;
                        }

                        Console.WriteLine($"Applied rotation of {rotationAngle}° to all shapes in master '{master.Name}'.");
                    }
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}