using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
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
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram has at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page (you can modify this to target a specific page).
                Page page = diagram.Pages[0];

                // Iterate all shapes on the page.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the current line pattern value.
                    // LinePattern.Value returns a LinePatternValue enum; casting to int for display.
                    int currentPattern = (int)shape.Line.LinePattern.Value;
                    Console.WriteLine($"Shape ID {shape.ID} current line pattern index: {currentPattern}");

                    // Change the line pattern to a dotted style.
                    // In Visio, line pattern index 3 typically represents a dotted line.
                    // Assign using the integer index as the enum underlying value.
                    shape.Line.LinePattern.Value = (LinePatternValue)3;

                    // Verify the change.
                    int newPattern = (int)shape.Line.LinePattern.Value;
                    Console.WriteLine($"Shape ID {shape.ID} new line pattern index: {newPattern}");
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}