using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <VisioFilePath>");
            return;
        }

        string visioPath = args[0];

        // Guard: ensure the Visio file exists.
        if (!File.Exists(visioPath))
        {
            Console.Error.WriteLine($"Error: File not found - {visioPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block (required by the API).
            using (Diagram diagram = new Diagram(visioPath))
            {
                // Ensure the diagram contains at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                // Iterate through all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.NameU}");

                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted.
                        if (shape.Del == BOOL.True)
                            continue;

                        // Log basic shape information.
                        Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");

                        // Retrieve inherited fill and line data.
                        Fill inheritFill = shape.InheritFill;
                        Line inheritLine = shape.InheritLine;

                        // Log selected inherited fill properties.
                        string fillForegnd = inheritFill?.FillForegnd?.Value ?? "N/A";
                        string fillPattern = inheritFill != null ? inheritFill.FillPattern.Value.ToString() : "N/A";
                        Console.WriteLine($"  Inherit Fill - Foreground: {fillForegnd}, Pattern: {fillPattern}");

                        // Log selected inherited line properties.
                        string lineColor = inheritLine?.LineColor?.Value ?? "N/A";
                        string linePattern = inheritLine != null ? inheritLine.LinePattern.Value.ToString() : "N/A";
                        Console.WriteLine($"  Inherit Line - Color: {lineColor}, Pattern: {linePattern}");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
