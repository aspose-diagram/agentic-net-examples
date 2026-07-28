using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file (replace with actual path)
            string inputPath = "input.vsdx";
            // Path for the optional output copy
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Get the current line pattern (dash style) and line weight (thickness)
                    var linePattern = shape.Line.LinePattern.Value;   // enum LinePatternValue
                    var lineWeight = shape.Line.LineWeight.Value;     // double, inches

                    // Determine whether each property is inherited from the master/style
                    bool patternInherited = linePattern == shape.InheritLine.LinePattern.Value;
                    bool weightInherited = lineWeight == shape.InheritLine.LineWeight.Value;

                    // Output the details
                    Console.WriteLine($"Shape ID {shape.ID} on Page ID {page.ID}:");
                    Console.WriteLine($"  Line Pattern: {linePattern} (Inherited: {patternInherited})");
                    Console.WriteLine($"  Line Weight: {lineWeight} inches (Inherited: {weightInherited})");
                }
            }

            // Save a copy of the diagram (optional)
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
