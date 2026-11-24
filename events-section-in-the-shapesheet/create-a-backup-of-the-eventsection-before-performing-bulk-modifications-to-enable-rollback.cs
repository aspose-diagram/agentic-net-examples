using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> [modifiedOutputFile] [rollbackOutputFile]");
            return;
        }

        string inputPath = args[0];
        string modifiedOutputPath = args.Length > 1 ? args[1] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "modified.vsdx");
        string rollbackOutputPath = args.Length > 2 ? args[2] : Path.Combine(Path.GetDirectoryName(inputPath) ?? "", "rollback.vsdx");

        // Guard against missing input file
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the input file
            Diagram diagram = new Diagram(inputPath);

            // Backup dictionary: shape ID (long) -> event name -> formula
            var eventBackup = new Dictionary<long, Dictionary<string, string>>();

            // Iterate through all pages and shapes to backup EventSection formulas
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    var shapeEvents = new Dictionary<string, string>();

                    // Backup each supported event cell formula (use empty string if null)
                    shapeEvents["EventDrop"] = shape.Event.EventDrop.Ufe.F ?? string.Empty;
                    shapeEvents["EventDblClick"] = shape.Event.EventDblClick.Ufe.F ?? string.Empty;
                    shapeEvents["EventXFMod"] = shape.Event.EventXFMod.Ufe.F ?? string.Empty;
                    shapeEvents["EventMultiDrop"] = shape.Event.EventMultiDrop.Ufe.F ?? string.Empty;
                    shapeEvents["TheText"] = shape.Event.TheText.Ufe.F ?? string.Empty;
                    shapeEvents["TheData"] = shape.Event.TheData.Ufe.F ?? string.Empty;

                    eventBackup[shape.ID] = shapeEvents;
                }
            }

            // Perform bulk modifications: set a sample formula for each event cell
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"OnDrop\")";
                    shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"OnDblClick\")";
                    shape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"OnXFMod\")";
                    shape.Event.EventMultiDrop.Ufe.F = "CALLTHIS(\"OnMultiDrop\")";
                    shape.Event.TheText.Ufe.F = "CALLTHIS(\"OnTheText\")";
                    shape.Event.TheData.Ufe.F = "CALLTHIS(\"OnTheData\")";
                }
            }

            // Save the modified diagram
            diagram.Save(modifiedOutputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Modified diagram saved to: {modifiedOutputPath}");

            // --- Rollback demonstration ---
            // Restore the original event formulas from the backup
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (eventBackup.TryGetValue(shape.ID, out var originalEvents))
                    {
                        shape.Event.EventDrop.Ufe.F = originalEvents["EventDrop"];
                        shape.Event.EventDblClick.Ufe.F = originalEvents["EventDblClick"];
                        shape.Event.EventXFMod.Ufe.F = originalEvents["EventXFMod"];
                        shape.Event.EventMultiDrop.Ufe.F = originalEvents["EventMultiDrop"];
                        shape.Event.TheText.Ufe.F = originalEvents["TheText"];
                        shape.Event.TheData.Ufe.F = originalEvents["TheData"];
                    }
                }
            }

            // Save the rolled‑back diagram
            diagram.Save(rollbackOutputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Rollback diagram saved to: {rollbackOutputPath}");
        }
        catch (Exception ex)
        {
            // Log any Aspose or IO errors
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}