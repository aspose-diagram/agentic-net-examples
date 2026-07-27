using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Path to the input Visio file
        string inputPath = "input.vsdx";
        // Guard to ensure the file exists before loading
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        try
        {
            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Assign a Visio formula to a supported event cell.
                    // The original request was for EventMouseLeave, which is not exposed by Aspose.Diagram.
                    // Using EventDblClick as a representative event cell that exists.
                    shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"LogShapeLeave\")";
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}

// Placeholder class representing the macro that would be invoked by the CALLTHIS formula.
// In a real Visio environment, this macro would be defined in VBA.
// Here we provide a C# method for illustration; it will not be called automatically.
public static class EventHandlers
{
    public static void LogShapeLeave()
    {
        // Log the event – in a real scenario this could write to a file or console.
        Console.WriteLine("A shape's EventDblClick cell was triggered.");
    }
}