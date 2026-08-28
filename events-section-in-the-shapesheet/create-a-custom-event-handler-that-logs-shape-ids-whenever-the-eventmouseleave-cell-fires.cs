using System;
using System.IO;
using Aspose.Diagram;

namespace CustomEventHandlerExample
{
    // Simple logger class to encapsulate logging logic
    public static class Logger
    {
        // Logs the shape ID when the event is (simulated) triggered
        public static void LogShapeId(long shapeId)
        {
            Console.WriteLine($"Event triggered for Shape ID: {shapeId}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Path to the input Visio diagram
            string diagramPath = "input.vsdx";
            // Guard: ensure the input file exists before proceeding
            if (!File.Exists(diagramPath))
            {
                Console.Error.WriteLine($"File not found: {diagramPath}");
                return;
            }

            // Path for the optional output diagram
            string outputPath = "output.vsdx";

            try
            {
                // Load the diagram from the specified file
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages and shapes to attach an event formula
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Use a valid event cell (EventDblClick) as a placeholder for EventMouseLeave
                        // This sets a CALLTHIS formula that would invoke a macro in Visio.
                        shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"LogShapeLeave\")";

                        // Simulate the event firing by directly invoking the logger
                        // (Aspose.Diagram cannot raise UI events in a console application)
                        Logger.LogShapeId(shape.ID);
                    }
                }

                // Save the modified diagram (optional)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Write any Aspose or I/O errors to the error stream
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}