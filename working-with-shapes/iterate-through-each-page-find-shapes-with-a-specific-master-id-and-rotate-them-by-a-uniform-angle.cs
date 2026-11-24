using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments: input file, master ID, rotation angle, optional output file
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: <program> <inputVisioPath> <masterId> <angleDegrees> [outputVisioPath]");
            return;
        }

        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        // Parse master ID
        if (!int.TryParse(args[1], out int masterId))
        {
            Console.Error.WriteLine("Invalid master ID. It must be an integer.");
            return;
        }

        // Parse rotation angle
        if (!double.TryParse(args[2], out double angleDegrees))
        {
            Console.Error.WriteLine("Invalid angle. It must be a numeric value representing degrees.");
            return;
        }

        // Determine output path
        string outputPath;
        if (args.Length >= 4 && !string.IsNullOrWhiteSpace(args[3]))
        {
            outputPath = args[3];
        }
        else
        {
            string dir = Path.GetDirectoryName(inputPath) ?? "";
            string nameWithoutExt = Path.GetFileNameWithoutExtension(inputPath);
            outputPath = Path.Combine(dir, $"{nameWithoutExt}_rotated.vsdx");
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the shape has an associated master before checking its ID
                    if (shape.Master != null && shape.Master.ID == masterId)
                    {
                        // Rotate the shape by setting its XForm.Angle value (degrees)
                        shape.XForm.Angle.Value = angleDegrees;
                    }
                }
            }

            // Save the modified diagram to the specified output file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}