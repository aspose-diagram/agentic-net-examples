using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (adjust as needed)
        string inputPath = "sample.vsdx";

        // Guard to ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure both the Line and InheritLine objects are available
                    if (shape.Line != null && shape.InheritLine != null)
                    {
                        // Compare a representative line property (LineColor) with its inherited counterpart
                        bool isInherited = shape.Line.LineColor.Value == shape.InheritLine.LineColor.Value;

                        // Output the inheritance status for the current shape
                        Console.WriteLine($"Shape ID {shape.ID}: Line inheritance = {isInherited}");
                    }
                    else
                    {
                        // Inform that line inheritance information is not available for this shape
                        Console.WriteLine($"Shape ID {shape.ID}: Line inheritance information not available.");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}