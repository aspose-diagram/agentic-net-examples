using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Load the diagram (replace with your file path)
        string inputPath = "input.vsdx";
        // Verify input file exists
        if (!File.Exists(inputPath)) { Console.Error.WriteLine($"File not found: {inputPath}"); return; }

        Diagram diagram;
        try
        {
            // Load diagram inside try/catch to capture loading errors
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Backup dictionary: Shape ID (long) -> (Event Cell Name -> Formula)
        var eventBackup = new Dictionary<long, Dictionary<string, string>>();

        try
        {
            // Iterate through all pages and shapes to backup event formulas
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    var shapeEvents = new Dictionary<string, string>();

                    // Backup known event cells (add more if needed)
                    shapeEvents["EventXFMod"] = shape.Event.EventXFMod.Ufe.F;
                    shapeEvents["EventDblClick"] = shape.Event.EventDblClick.Ufe.F;
                    shapeEvents["EventDrop"] = shape.Event.EventDrop.Ufe.F;
                    shapeEvents["EventMultiDrop"] = shape.Event.EventMultiDrop.Ufe.F;
                    shapeEvents["TheText"] = shape.Event.TheText.Ufe.F;
                    shapeEvents["TheData"] = shape.Event.TheData.Ufe.F;

                    // Store backup using shape.ID (long)
                    eventBackup[shape.ID] = shapeEvents;
                }
            }

            // -------------------------
            // Perform bulk modifications
            // -------------------------
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Example modification: set a double-click event to call a custom macro
                    shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"MyCustomMacro\")";

                    // Example modification: clear the drop event
                    shape.Event.EventDrop.Ufe.F = "";
                }
            }

            // -------------------------
            // Optional: Rollback to original event formulas
            // -------------------------
            bool needRollback = false; // set to true if rollback is required

            if (needRollback)
            {
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        if (eventBackup.TryGetValue(shape.ID, out var savedEvents))
                        {
                            shape.Event.EventXFMod.Ufe.F = savedEvents["EventXFMod"];
                            shape.Event.EventDblClick.Ufe.F = savedEvents["EventDblClick"];
                            shape.Event.EventDrop.Ufe.F = savedEvents["EventDrop"];
                            shape.Event.EventMultiDrop.Ufe.F = savedEvents["EventMultiDrop"];
                            shape.Event.TheText.Ufe.F = savedEvents["TheText"];
                            shape.Event.TheData.Ufe.F = savedEvents["TheData"];
                        }
                    }
                }
            }

            // Save the modified (or rolled‑back) diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Capture any errors during processing or saving
            Console.Error.WriteLine($"Processing error: {ex.Message}");
        }
        finally
        {
            // Cleanup resources
            diagram?.Dispose();
        }

        Console.WriteLine("Processing completed.");
    }
}