using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: input diagram path and output diagram path.
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
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    try
                    {
                        // Attempt to set the text direction.
                        // Some shapes may not have a TextBlock section; handle gracefully.
                        if (shape.TextBlock != null)
                        {
                            // Example: set text direction to vertical.
                            shape.TextBlock.TextDirection.Value = TextDirectionValue.Vertical;
                        }
                        else
                        {
                            // Log that the shape lacks a TextBlock section.
                            Console.WriteLine($"Shape ID {shape.ID} on page '{page.Name}' does not have a TextBlock section.");
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log any unexpected errors while setting text direction for this shape.
                        Console.Error.WriteLine($"Error processing shape ID {shape.ID} on page '{page.Name}': {ex.Message}");
                    }
                }
            }

            // Save the modified diagram to the specified output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any errors that occur during loading or saving.
            Console.Error.WriteLine($"Aspose.Diagram operation failed: {ex.Message}");
        }
    }
}