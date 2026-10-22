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
            Console.Error.WriteLine("Error: No diagram file path provided.");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the file exists before proceeding
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: File not found - {diagramPath}");
            return;
        }

        try
        {
            // Load the Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Access the Event section of the shape
                    Event eventSection = shape.Event;

                    // Prepare a flag to determine if any custom event is present
                    bool hasCustomEvent = false;

                    // Collect event formulas
                    string eventDropFormula = eventSection.EventDrop?.Ufe?.F ?? string.Empty;
                    string eventDblClickFormula = eventSection.EventDblClick?.Ufe?.F ?? string.Empty;
                    string eventXFModFormula = eventSection.EventXFMod?.Ufe?.F ?? string.Empty;
                    string eventMultiDropFormula = eventSection.EventMultiDrop?.Ufe?.F ?? string.Empty;
                    string theTextFormula = eventSection.TheText?.Ufe?.F ?? string.Empty;
                    string theDataFormula = eventSection.TheData?.Ufe?.F ?? string.Empty;

                    // Determine if any of the event formulas are non‑empty
                    if (!string.IsNullOrWhiteSpace(eventDropFormula) ||
                        !string.IsNullOrWhiteSpace(eventDblClickFormula) ||
                        !string.IsNullOrWhiteSpace(eventXFModFormula) ||
                        !string.IsNullOrWhiteSpace(eventMultiDropFormula) ||
                        !string.IsNullOrWhiteSpace(theTextFormula) ||
                        !string.IsNullOrWhiteSpace(theDataFormula))
                    {
                        hasCustomEvent = true;
                    }

                    // If the shape has custom event configurations, output them
                    if (hasCustomEvent)
                    {
                        Console.WriteLine("--------------------------------------------------");
                        Console.WriteLine($"Page: {page.NameU} (ID: {page.ID})");
                        Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");

                        if (!string.IsNullOrWhiteSpace(eventDropFormula))
                            Console.WriteLine($"  EventDrop Formula   : {eventDropFormula}");

                        if (!string.IsNullOrWhiteSpace(eventDblClickFormula))
                            Console.WriteLine($"  EventDblClick Formula: {eventDblClickFormula}");

                        if (!string.IsNullOrWhiteSpace(eventXFModFormula))
                            Console.WriteLine($"  EventXFMod Formula   : {eventXFModFormula}");

                        if (!string.IsNullOrWhiteSpace(eventMultiDropFormula))
                            Console.WriteLine($"  EventMultiDrop Formula: {eventMultiDropFormula}");

                        if (!string.IsNullOrWhiteSpace(theTextFormula))
                            Console.WriteLine($"  TheText Formula      : {theTextFormula}");

                        if (!string.IsNullOrWhiteSpace(theDataFormula))
                            Console.WriteLine($"  TheData Formula      : {theDataFormula}");
                    }
                }
            }

            // Dispose the diagram to release resources
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            // Log any exceptions that occur during processing
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}