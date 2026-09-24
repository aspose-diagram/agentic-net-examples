using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving; // Required for SaveFileFormat enum (if needed)

class Program
{
    static void Main(string[] args)
    {
        // Expect the Visio file path as the first argument
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Verify the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                Console.WriteLine($"--- Page ID: {page.ID}, Name: {page.NameU} ---");

                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True) continue;

                    // Access line pattern (dash style) and line weight (thickness)
                    var linePattern = shape.Line.LinePattern.Value; // Enum LinePatternValue
                    var lineWeight = shape.Line.LineWeight.Value;   // Double (in inches)

                    // Determine if the line pattern is inherited from the master/style
                    bool isPatternInherited = linePattern == shape.InheritLine.LinePattern.Value;
                    // Determine if the line weight is inherited
                    bool isWeightInherited = Math.Abs(lineWeight - shape.InheritLine.LineWeight.Value) < 1e-6;

                    // Output shape identification and line details
                    Console.WriteLine($"Shape ID: {shape.ID}, Name: {shape.NameU}");
                    Console.WriteLine($"  Line Pattern: {linePattern} (Inherited: {isPatternInherited})");
                    Console.WriteLine($"  Line Weight: {lineWeight:F4} inches (Inherited: {isWeightInherited})");
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}