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
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate over all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all shapes on the page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Flip fill inheritance status.
                    // If currently inherited, set to explicit value (disable inheritance).
                    // Otherwise, enable inheritance by assigning the special INHERITED() function.
                    // The Ufe.F property holds the inheritance flag as a string.
                    string currentUfe = shape.Fill.FillForegnd.Ufe.F;

                    if (string.Equals(currentUfe, "INHERITED()", StringComparison.OrdinalIgnoreCase))
                    {
                        // Disable inheritance: assign the current concrete fill color.
                        // Use the existing FillForegnd value.
                        shape.Fill.FillForegnd.Ufe.F = shape.Fill.FillForegnd.Value;
                    }
                    else
                    {
                        // Enable inheritance.
                        shape.Fill.FillForegnd.Ufe.F = "INHERITED()";
                    }
                }
            }

            // Save the modified diagram. Use Vsdx format for standard Visio files.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}