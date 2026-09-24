using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments: input Visio file and output Visio file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: DiagramFillInheritanceToggle <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        // Guard: ensure the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = args[1];

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through every page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through every shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes (Del == BOOL.True) to avoid processing hidden elements.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the current foreground fill color of the shape.
                    string currentFill = shape.Fill.FillForegnd.Value;

                    // Retrieve the inherited foreground fill color from the shape's parent style.
                    string inheritedFill = shape.InheritFill.FillForegnd.Value;

                    // Determine if the shape is currently using the inherited fill.
                    bool isInherited = string.Equals(currentFill, inheritedFill, StringComparison.OrdinalIgnoreCase);

                    if (isInherited)
                    {
                        // Shape is inheriting fill – set an explicit fill color (red) to break inheritance.
                        shape.Fill.FillForegnd.Value = "#FF0000";
                        // Optionally set a solid fill pattern to ensure visibility.
                        shape.Fill.FillPattern.Value = 1; // 1 = solid fill.
                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}': inheritance removed (set to red).");
                    }
                    else
                    {
                        // Shape has an explicit fill – revert to inherited fill by copying the inherited value.
                        shape.Fill.FillForegnd.Value = inheritedFill;
                        // Preserve the inherited fill pattern.
                        shape.Fill.FillPattern.Value = shape.InheritFill.FillPattern.Value;
                        Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}': inheritance restored.");
                    }
                }
            }

            // Save the modified diagram to the output path using VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}