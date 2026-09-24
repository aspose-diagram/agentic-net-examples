using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for diagram save operations

class Program
{
    static void Main(string[] args)
    {
        // Verify that both input and output paths are provided
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: program <inputVisioPath> <outputVisioPath>");
            return;
        }

        // Assign input file path and guard its existence
        string inputPath = args[0];
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Assign output file path and ensure its directory exists
        string outputPath = args[1];
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
        {
            Console.Error.WriteLine($"Output directory does not exist: {outDir}");
            return;
        }

        try
        {
            // Load the Visio diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the first page of the diagram
            Page page = diagram.Pages[0];

            // Locate the first shape that is not marked as deleted
            Shape targetShape = null;
            foreach (Shape s in page.Shapes)
            {
                // Check the deletion flag using the BOOL enum
                if (s.Del == BOOL.False)
                {
                    targetShape = s;
                    break;
                }
            }

            // If no suitable shape is found, report and exit
            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found in the diagram.");
                return;
            }

            // Rotate the shape by 45 degrees (value is in degrees)
            targetShape.XForm.Angle.Value = 45;

            // Disable KeepTextFlat to allow the text within the shape to rotate
            targetShape.ThreeDFormat.KeepTextFlat.Value = BOOL.False;

            // Save the modified diagram to the specified output path in VSDX format
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}