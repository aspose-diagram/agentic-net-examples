using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect three arguments: input diagram path, output diagram path, shape name to lock paragraph editing.
        if (args.Length < 3)
        {
            Console.Error.WriteLine("Usage: Program <inputPath> <outputPath> <shapeName>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];
        string targetShapeName = args[2];

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
                // Iterate through all pages and shapes to find matching shapes.
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape's universal name matches the target name (case-sensitive).
                        if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU == targetShapeName)
                        {
                            // Lock paragraph (text) editing for this shape.
                            shape.Protection.LockTextEdit.Value = BOOL.True;

                            // Verify the lock was applied.
                            if (shape.Protection.LockTextEdit.Value == BOOL.True)
                            {
                                Console.WriteLine($"Paragraph editing locked for shape ID {shape.ID} (NameU: {targetShapeName}).");
                            }
                            else
                            {
                                Console.WriteLine($"Failed to lock paragraph editing for shape ID {shape.ID} (NameU: {targetShapeName}).");
                            }

                            // Example of allowing other modifications: change fill foreground color.
                            // Use a hex color string (e.g., yellow).
                            shape.Fill.FillForegnd.Value = "#FFFF00";
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}