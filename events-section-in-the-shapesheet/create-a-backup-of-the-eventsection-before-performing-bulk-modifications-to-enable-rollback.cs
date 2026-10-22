using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    // Holds backup of event formulas for a shape
    private class EventBackup
    {
        public string EventDrop;
        public string EventDblClick;
        public string EventXFMod;
        public string EventMultiDrop;
        public string TheText;
        public string TheData;
    }

    static void Main(string[] args)
    {
        // Input and output file paths (adjust as needed)
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output_modified.vsdx";

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Dictionary to store backups keyed by page index and shape ID (shape.ID is long)
        var backup = new Dictionary<(int pageIndex, long shapeId), EventBackup>();

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // ---------- Backup Phase ----------
            // Iterate through all pages and shapes to backup event formulas
            for (int p = 0; p < diagram.Pages.Count; p++)
            {
                Page page = diagram.Pages[p];
                foreach (Shape shape in page.Shapes)
                {
                    var eb = new EventBackup
                    {
                        EventDrop = shape.Event.EventDrop?.Ufe?.F,
                        EventDblClick = shape.Event.EventDblClick?.Ufe?.F,
                        EventXFMod = shape.Event.EventXFMod?.Ufe?.F,
                        EventMultiDrop = shape.Event.EventMultiDrop?.Ufe?.F,
                        TheText = shape.Event.TheText?.Ufe?.F,
                        TheData = shape.Event.TheData?.Ufe?.F
                    };
                    backup[(p, shape.ID)] = eb;
                }
            }

            // ---------- Bulk Modification Phase ----------
            // Example modification: set a generic event formula for all shapes
            for (int p = 0; p < diagram.Pages.Count; p++)
            {
                Page page = diagram.Pages[p];
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the event cells exist before assigning
                    if (shape.Event.EventDrop != null) shape.Event.EventDrop.Ufe.F = "CALLTHIS(\"MyMacro\")";
                    if (shape.Event.EventDblClick != null) shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"MyMacro\")";
                    if (shape.Event.EventXFMod != null) shape.Event.EventXFMod.Ufe.F = "CALLTHIS(\"MyMacro\")";
                    if (shape.Event.EventMultiDrop != null) shape.Event.EventMultiDrop.Ufe.F = "CALLTHIS(\"MyMacro\")";
                    if (shape.Event.TheText != null) shape.Event.TheText.Ufe.F = "CALLTHIS(\"MyMacro\")";
                    if (shape.Event.TheData != null) shape.Event.TheData.Ufe.F = "CALLTHIS(\"MyMacro\")";
                }
            }

            // ---------- Optional Rollback Phase ----------
            // Restore the original formulas from backup if needed
            for (int p = 0; p < diagram.Pages.Count; p++)
            {
                Page page = diagram.Pages[p];
                foreach (Shape shape in page.Shapes)
                {
                    if (backup.TryGetValue((p, shape.ID), out EventBackup eb))
                    {
                        if (shape.Event.EventDrop != null) shape.Event.EventDrop.Ufe.F = eb.EventDrop ?? "";
                        if (shape.Event.EventDblClick != null) shape.Event.EventDblClick.Ufe.F = eb.EventDblClick ?? "";
                        if (shape.Event.EventXFMod != null) shape.Event.EventXFMod.Ufe.F = eb.EventXFMod ?? "";
                        if (shape.Event.EventMultiDrop != null) shape.Event.EventMultiDrop.Ufe.F = eb.EventMultiDrop ?? "";
                        if (shape.Event.TheText != null) shape.Event.TheText.Ufe.F = eb.TheText ?? "";
                        if (shape.Event.TheData != null) shape.Event.TheData.Ufe.F = eb.TheData ?? "";
                    }
                }
            }

            // Save the (potentially rolled back) diagram to a new file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram or IO exceptions
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}