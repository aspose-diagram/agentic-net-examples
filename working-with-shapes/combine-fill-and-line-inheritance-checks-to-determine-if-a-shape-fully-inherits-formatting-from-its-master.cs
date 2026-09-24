using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the Visio file path.
        string visioPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Verify that the file exists before proceeding.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file.
            Diagram diagram = new Diagram(visioPath);

            // Iterate through all pages in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure inheritance objects are available.
                    if (shape.InheritFill == null || shape.InheritLine == null)
                        continue;

                    // ----- Fill inheritance check -----
                    bool fillForegndInherited = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
                    bool fillBkgndInherited = shape.Fill.FillBkgnd.Value == shape.InheritFill.FillBkgnd.Value;
                    bool fillPatternInherited = shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;

                    // Combine fill checks.
                    bool fillInherited = fillForegndInherited && fillBkgndInherited && fillPatternInherited;

                    // ----- Line inheritance check -----
                    bool lineColorInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                    bool lineWeightInherited = shape.Line.LineWeight.Value == shape.InheritLine.LineWeight.Value;
                    bool linePatternInherited = shape.Line.LinePattern.Value == shape.InheritLine.LinePattern.Value;

                    // Combine line checks.
                    bool lineInherited = lineColorInherited && lineWeightInherited && linePatternInherited;

                    // Determine if the shape fully inherits both fill and line formatting.
                    if (fillInherited && lineInherited)
                    {
                        // Output shape identification details.
                        Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) fully inherits fill and line formatting from its master.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any unexpected errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}