using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect the first argument to be the Visio file path.
        string visioPath = args.Length > 0 ? args[0] : "input.vsdx";

        // Verify the file exists before proceeding.
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
                    // Skip shapes that are marked as deleted.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Ensure the InheritFill collection is available.
                    if (shape.InheritFill == null)
                    {
                        Console.WriteLine($"Shape ID {shape.ID} (Name: {shape.Name}) has no InheritFill data.");
                        continue;
                    }

                    // Compare a representative fill property (foreground color) with the inherited value.
                    bool inheritsForeground = shape.Fill.FillForegnd.Value == shape.InheritFill.FillForegnd.Value;

                    // Compare the fill pattern as an additional check.
                    bool inheritsPattern = shape.Fill.FillPattern.Value == shape.InheritFill.FillPattern.Value;

                    // Determine overall inheritance based on both checks.
                    bool inheritsFill = inheritsForeground && inheritsPattern;

                    // Output the result for the current shape.
                    Console.WriteLine($"Shape ID {shape.ID} (Name: {shape.Name}) inherits fill: {inheritsFill}");
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}