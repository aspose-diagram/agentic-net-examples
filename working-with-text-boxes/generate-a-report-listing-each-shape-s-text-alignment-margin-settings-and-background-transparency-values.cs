using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Error: No input file path provided.");
            return;
        }

        string inputPath = args[0];

        // Guard against missing file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found - {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Retrieve vertical text alignment
                    string alignment = shape.TextBlock.VerticalAlign.Value.ToString();

                    // Retrieve margin settings (values are in the diagram's units)
                    double leftMargin = shape.TextBlock.LeftMargin.Value;
                    double rightMargin = shape.TextBlock.RightMargin.Value;
                    double topMargin = shape.TextBlock.TopMargin.Value;
                    double bottomMargin = shape.TextBlock.BottomMargin.Value;

                    // Retrieve background transparency (0 = opaque, 100 = fully transparent)
                    double backgroundTransparency = shape.TextBlock.TextBkgndTrans.Value;

                    // Output the report line for the current shape
                    Console.WriteLine($"Page: {page.Name}, Shape ID: {shape.ID}");
                    Console.WriteLine($"  Text Alignment (Vertical): {alignment}");
                    Console.WriteLine($"  Margins - Left: {leftMargin}, Right: {rightMargin}, Top: {topMargin}, Bottom: {bottomMargin}");
                    Console.WriteLine($"  Text Background Transparency: {backgroundTransparency}%");
                    Console.WriteLine();
                }
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}