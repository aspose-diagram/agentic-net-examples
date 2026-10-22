using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, master ID, rotation angle
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <masterId> <angleDegrees>");
            return;
        }

        string inputPath = args[0];
        string masterIdArg = args[1];
        string angleArg = args[2];

        // Guard: check input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        // Parse master ID
        if (!int.TryParse(masterIdArg, out int targetMasterId))
        {
            Console.Error.WriteLine($"Error: Invalid master ID '{masterIdArg}'. It must be an integer.");
            return;
        }

        // Parse rotation angle
        if (!double.TryParse(angleArg, out double rotationAngle))
        {
            Console.Error.WriteLine($"Error: Invalid angle '{angleArg}'. It must be a numeric value.");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has an associated master
                    if (shape.Master == null)
                        continue;

                    // Check if the shape's master ID matches the target master ID
                    if (shape.Master.ID == targetMasterId)
                    {
                        // Rotate the shape to the specified angle (degrees)
                        // Using SetAngle method as per Aspose.Diagram documentation
                        shape.SetAngle(rotationAngle);
                    }
                }
            }

            // Prepare output file path (same directory, suffix "_rotated.vsdx")
            string directory = Path.GetDirectoryName(inputPath) ?? "";
            string filenameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            string outputPath = Path.Combine(directory, $"{filenameWithoutExt}_rotated.vsdx");

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Diagram processed successfully. Output saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}