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
            Console.Error.WriteLine("Usage: Program <inputFilePath> <outputFilePath>");
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
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip logically deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Identify connector shapes (1‑D shapes).
                    if (shape.OneD)
                    {
                        // Ensure the shape contains at least one field.
                        if (shape.Fields != null && shape.Fields.Count > 0)
                        {
                            // Update the formula of each existing field.
                            foreach (Field field in shape.Fields)
                            {
                                // Example: set a new formula value.
                                string newFormula = "UpdatedFormula()";
                                field.Value.Val = newFormula;
                            }
                        }
                    }
                }
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Diagram processed and saved successfully.");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}