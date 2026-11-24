using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the path to the Visio file.
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Guard: ensure the file exists before proceeding.
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

                    // Determine if the shape has an associated master.
                    bool hasMaster = shape.Master != null;

                    // Default inheritance flags.
                    bool fillInherited = false;
                    bool lineInherited = false;

                    if (hasMaster)
                    {
                        // ----- Fill inheritance check -----
                        // Ensure Fill and InheritFill objects are available.
                        if (shape.Fill != null && shape.InheritFill != null)
                        {
                            // Compare the foreground fill color with the inherited fill color.
                            // Matching values indicate inheritance.
                            fillInherited = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;
                        }

                        // ----- Line inheritance check -----
                        // Ensure Line and InheritLine objects are available.
                        if (shape.Line != null && shape.InheritLine != null)
                        {
                            // Compare the line color with the inherited line color.
                            lineInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;
                        }
                    }

                    // A shape fully inherits formatting when both fill and line are inherited from its master.
                    bool fullyInherits = hasMaster && fillInherited && lineInherited;

                    // Output the result for the current shape.
                    Console.WriteLine($"Shape ID {shape.ID} on page \"{page.Name}\" fully inherits formatting: {fullyInherits}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors that occur during processing to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}