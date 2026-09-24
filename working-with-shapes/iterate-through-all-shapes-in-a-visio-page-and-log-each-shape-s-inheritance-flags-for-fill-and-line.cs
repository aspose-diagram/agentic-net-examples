using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Ensure a file path argument is provided.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        // Assign the first argument to a variable.
        string visioPath = args[0];

        // Guard: verify the file exists before proceeding.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"File not found: {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(visioPath);

            // Iterate over each page in the diagram.
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve the shape's fill foreground color (as a string).
                    string fillForegnd = shape.Fill.FillForegnd.Value;

                    // Retrieve the inherited fill foreground color from the parent style/master.
                    string inheritFillForegnd = shape.InheritFill.FillForegnd.Value;

                    // Determine if the fill color is inherited (values match).
                    bool isFillInherited = string.Equals(fillForegnd, inheritFillForegnd, StringComparison.OrdinalIgnoreCase);

                    // Retrieve the shape's line color (as a string).
                    string lineColor = shape.Line.LineColor.Value;

                    // Retrieve the inherited line color from the parent style/master.
                    string inheritLineColor = shape.InheritLine.LineColor.Value;

                    // Determine if the line color is inherited (values match).
                    bool isLineInherited = string.Equals(lineColor, inheritLineColor, StringComparison.OrdinalIgnoreCase);

                    // Log the shape ID, universal name, and inheritance flags.
                    Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}, FillInherited: {isFillInherited}, LineInherited: {isLineInherited}");
                }
            }
        }
        catch (Exception ex)
        {
            // Output any exceptions that occur during processing.
            Console.Error.WriteLine($"Error processing Visio file: {ex.Message}");
        }
    }
}