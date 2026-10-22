using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Expect input Visio file path and output file path as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Processing Page ID {page.ID}, Name \"{page.Name}\"");

                    // Iterate through each shape on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes.
                        if (shape.Del == BOOL.True)
                            continue;

                        // If the shape is based on a master, validate line inheritance.
                        if (shape.Master != null)
                        {
                            // Access the inherited line formatting.
                            var inheritLine = shape.InheritLine;

                            // Validate that the inherited line color is present.
                            // (LineColor may be null if inheritance is not applied.)
                            if (inheritLine?.LineColor == null || inheritLine.LineColor.Value == null)
                            {
                                Console.WriteLine($"[Warning] Shape ID {shape.ID} does not inherit line color from its master \"{shape.Master.Name}\".");
                            }
                            else
                            {
                                Console.WriteLine($"Shape ID {shape.ID} inherits line color: {inheritLine.LineColor.Value}");
                            }

                            // Additional validation can be added here for other line properties
                            // such as LineWeight, BeginArrow, EndArrow, etc., following the same pattern.
                        }
                        else
                        {
                            // Shape has no master; inheritance check is not applicable.
                            Console.WriteLine($"Shape ID {shape.ID} has no master; skipping inheritance validation.");
                        }
                    }
                }

                // After validation, export the diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}