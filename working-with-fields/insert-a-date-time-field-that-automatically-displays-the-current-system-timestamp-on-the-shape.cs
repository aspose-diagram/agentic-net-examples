using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Locate the first non-deleted shape on the first page
            Shape targetShape = null;
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    targetShape = shape;
                    break;
                }
                if (targetShape != null)
                    break;
            }

            if (targetShape == null)
            {
                Console.Error.WriteLine("No suitable shape found in the diagram.");
                return;
            }

            // Create a new field that displays the current date/time using the NOW() formula
            Field dateTimeField = new Field();

            // Set the field's formula to NOW() so it updates automatically
            dateTimeField.Value.Ufev.F = "NOW()";
            dateTimeField.Value.Ufev.Unit = MeasureConst.Undefined;

            // Optionally, set the field format (e.g., date/time format string)
            // Here we leave the default format; you can customize by setting dateTimeField.Format.Val, etc.

            // Add the field to the shape's Fields collection
            targetShape.Fields.Add(dateTimeField);

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors to the error console
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}