using System.IO;
using System;
using Aspose.Diagram;

class TodoRemover
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the shape's text (if any)
                    string shapeText = shape.Text?.Value?.ToString();

                    if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains("TODO"))
                    {
                        // Replace all occurrences of "TODO" with an empty string
                        shape.ReplaceText("TODO", "");

                        // Refresh shape data to update geometry after text change
                        shape.RefreshData();

                        // Log the affected shape ID
                        Console.WriteLine($"Replaced 'TODO' in shape ID: {shape.ID}");
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
