using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                bool allShapesHaveMaster = true;

                // Validate that every shape has a non‑null Master reference.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (shape.Master == null)
                        {
                            Console.Error.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" has a null Master reference.");
                            allShapesHaveMaster = false;
                        }
                    }
                }

                if (!allShapesHaveMaster)
                {
                    Console.Error.WriteLine("Validation failed: one or more shapes lack a Master reference. Bulk modifications will not be performed.");
                    return;
                }

                // All shapes have valid Master references – proceed with bulk modifications.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Set fill foreground color to red (hex string).
                        shape.Fill.FillForegnd.Value = "#FF0000";

                        // Set line weight to 0.03 inches.
                        shape.Line.LineWeight.Value = 0.03;
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}