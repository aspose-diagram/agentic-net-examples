using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to analyze
            string filePath = "input.vsdx";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(filePath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape has an Event section
                    if (shape.Event == null)
                        continue;

                    // Local helper to output event information when a formula is present
                    void PrintEvent(string eventName, string formula)
                    {
                        if (!string.IsNullOrWhiteSpace(formula))
                        {
                            Console.WriteLine(
                                $"Page: {page.Name} | Shape ID: {shape.ID} | Shape Name: {shape.Name} | Event: {eventName} | Formula: {formula}");
                        }
                    }

                    // Check each supported event cell and output its formula if defined
                    PrintEvent("EventXFMod", shape.Event.EventXFMod.Ufe.F);
                    PrintEvent("EventDblClick", shape.Event.EventDblClick.Ufe.F);
                    PrintEvent("EventDrop", shape.Event.EventDrop.Ufe.F);
                    PrintEvent("EventMultiDrop", shape.Event.EventMultiDrop.Ufe.F);
                    PrintEvent("TheText", shape.Event.TheText.Ufe.F);
                    PrintEvent("TheData", shape.Event.TheData.Ufe.F);
                }
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
