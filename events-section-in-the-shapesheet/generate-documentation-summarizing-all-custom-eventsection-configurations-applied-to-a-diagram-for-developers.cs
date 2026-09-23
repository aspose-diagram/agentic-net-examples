using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio diagram file
            string diagramPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Check each supported event cell for a custom formula
                    PrintEventIfExists(shape, "EventXFMod", shape.Event.EventXFMod.Ufe.F);
                    PrintEventIfExists(shape, "EventDblClick", shape.Event.EventDblClick.Ufe.F);
                    PrintEventIfExists(shape, "EventDrop", shape.Event.EventDrop.Ufe.F);
                    PrintEventIfExists(shape, "EventMultiDrop", shape.Event.EventMultiDrop.Ufe.F);
                    PrintEventIfExists(shape, "TheText", shape.Event.TheText.Ufe.F);
                    PrintEventIfExists(shape, "TheData", shape.Event.TheData.Ufe.F);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Helper method to output event information when a formula is present
    static void PrintEventIfExists(Shape shape, string eventName, string formula)
    {
        if (!string.IsNullOrWhiteSpace(formula))
        {
            Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}, Event: {eventName}, Formula: {formula}");
        }
    }
}
