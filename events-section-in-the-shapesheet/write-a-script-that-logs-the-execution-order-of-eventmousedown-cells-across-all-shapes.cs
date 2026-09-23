using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Path to the Visio file (adjust as needed)
        string filePath = "input.vsdx";
        // Guard to ensure the input file exists
        if (!File.Exists(filePath)) { Console.Error.WriteLine($"File not found: {filePath}"); return; }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(filePath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the Event section exists
                    if (shape.Event != null)
                    {
                        // NOTE: The Aspose.Diagram API does not expose an EventMouseDown cell.
                        // As an alternative, we log the EventDblClick cell if it exists.
                        if (shape.Event.EventDblClick != null)
                        {
                            // Retrieve the formula stored in the EventDblClick cell
                            string formula = shape.Event.EventDblClick.Ufe.F;
                            // Log the execution order (page name, shape ID, and formula)
                            Console.WriteLine($"Page: {page.NameU}, Shape ID: {shape.ID}, EventDblClick Formula: {formula}");
                        }
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