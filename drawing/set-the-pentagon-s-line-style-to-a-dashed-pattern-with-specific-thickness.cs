using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify pentagon shapes by checking the master name.
                    if (shape.Master != null && shape.Master.Name == "Pentagon")
                    {
                        // Set the line dash pattern to a dashed style.
                        // LinePatternValue.Dash corresponds to a standard dash pattern.
                        shape.Line.LinePattern.Value = LinePatternValue.Dash;

                        // Set the line thickness (weight) in inches.
                        // Adjust the value as needed for the required thickness.
                        shape.Line.LineWeight.Value = 0.03; // 0.03 inches

                        Console.WriteLine($"Updated pentagon shape ID {shape.ID} on page \"{page.Name}\".");
                    }
                }
            }

            // Save the modified diagram to the specified output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}