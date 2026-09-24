using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the Visio file path.
        string inputPath = args.Length > 0 ? args[0] : "";
        // Guard: ensure the file path is provided.
        if (string.IsNullOrWhiteSpace(inputPath))
        {
            Console.Error.WriteLine("Error: No input file path specified.");
            return;
        }
        // Guard: verify the file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            // Write a header line for the report.
            Console.WriteLine("ShapeID\tShapeNameU\tFillInherited\tLineInherited");

            // Iterate through each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the fill properties are inherited.
                    bool fillInherited = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value &&
                                         shape.Fill.FillBkgnd.Value == shape.InheritFill.FillBkgnd.Value &&
                                         shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;

                    // Determine if the line properties are inherited.
                    bool lineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value &&
                                         shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value &&
                                         shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value;

                    // Output the shape information as a tab‑separated line.
                    Console.WriteLine($"{shape.ID}\t{shape.NameU}\t{fillInherited}\t{lineInherited}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose‑Diagram errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}