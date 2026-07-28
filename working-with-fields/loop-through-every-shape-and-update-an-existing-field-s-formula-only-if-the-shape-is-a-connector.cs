using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify connector shapes (1‑D shapes)
                    if (shape.OneD)
                    {
                        // Ensure the shape contains at least one field
                        if (shape.Fields != null && shape.Fields.Count > 0)
                        {
                            // Update the formula of the first existing field
                            Field field = shape.Fields[0];
                            // Use the .Val property to set a new formula string (compatible with current API)
                            field.Value.Val = "NEWFORMULA()";
                        }
                    }
                }
            }

            // Save the modified diagram to a new file
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}