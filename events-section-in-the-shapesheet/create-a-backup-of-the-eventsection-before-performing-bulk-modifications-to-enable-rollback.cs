using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    // List of event cell names to backup/restore
    private static readonly string[] EventCellNames = new[]
    {
        "EventXFMod",
        "EventDblClick",
        "EventDrop",
        "EventMultiDrop",
        "TheText",
        "TheData"
    };

    static void Main()
    {
        try
        {

            // Load the diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Backup EventSection for all shapes on all pages
            var eventBackup = BackupEventSections(diagram);

            try
            {
                // Perform bulk modifications on EventSection
                ApplyBulkModifications(diagram);

                // Save the modified diagram
                string modifiedPath = "modified.vsdx";
                diagram.Save(modifiedPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved after modifications: {modifiedPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during modification: {ex.Message}");
                // Rollback to original EventSection values
                RestoreEventSections(diagram, eventBackup);
                // Save the rolled‑back diagram
                string rollbackPath = "rollback.vsdx";
                diagram.Save(rollbackPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Rollback performed. Diagram saved: {rollbackPath}");
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Creates a deep copy of all event cell formulas for every shape
    private static Dictionary<long, Dictionary<string, string>> BackupEventSections(Diagram diagram)
    {
        var backup = new Dictionary<long, Dictionary<string, string>>();

        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                var shapeBackup = new Dictionary<string, string>();
                foreach (string cellName in EventCellNames)
                {
                    // Access the event cell via the Event property
                    // Use reflection-like switch because each event has a distinct property
                    string formula = GetEventCellFormula(shape, cellName);
                    shapeBackup[cellName] = formula;
                }
                backup[shape.ID] = shapeBackup;
            }
        }

        return backup;
    }

    // Restores event cell formulas from the backup
    private static void RestoreEventSections(Diagram diagram, Dictionary<long, Dictionary<string, string>> backup)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                if (!backup.TryGetValue(shape.ID, out var shapeBackup))
                    continue;

                foreach (var kvp in shapeBackup)
                {
                    SetEventCellFormula(shape, kvp.Key, kvp.Value);
                }
            }
        }
    }

    // Example bulk modification: set a new formula for EventDblClick on every shape
    private static void ApplyBulkModifications(Diagram diagram)
    {
        foreach (Page page in diagram.Pages)
        {
            foreach (Shape shape in page.Shapes)
            {
                // New formula example – show an alert when double‑clicked
                SetEventCellFormula(shape, "EventDblClick", "CALLTHIS(\"ThisDocument.ShowAlert\")");
            }
        }
    }

    // Helper to get the formula string of a specific event cell
    private static string GetEventCellFormula(Shape shape, string eventName)
    {
        switch (eventName)
        {
            case "EventXFMod":
                return shape.Event.EventXFMod.Ufe.F;
            case "EventDblClick":
                return shape.Event.EventDblClick.Ufe.F;
            case "EventDrop":
                return shape.Event.EventDrop.Ufe.F;
            case "EventMultiDrop":
                return shape.Event.EventMultiDrop.Ufe.F;
            case "TheText":
                return shape.Event.TheText.Ufe.F;
            case "TheData":
                return shape.Event.TheData.Ufe.F;
            default:
                throw new ArgumentException($"Unsupported event cell name: {eventName}");
        }
    }

    // Helper to set the formula string of a specific event cell
    private static void SetEventCellFormula(Shape shape, string eventName, string formula)
    {
        switch (eventName)
        {
            case "EventXFMod":
                shape.Event.EventXFMod.Ufe.F = formula;
                break;
            case "EventDblClick":
                shape.Event.EventDblClick.Ufe.F = formula;
                break;
            case "EventDrop":
                shape.Event.EventDrop.Ufe.F = formula;
                break;
            case "EventMultiDrop":
                shape.Event.EventMultiDrop.Ufe.F = formula;
                break;
            case "TheText":
                shape.Event.TheText.Ufe.F = formula;
                break;
            case "TheData":
                shape.Event.TheData.Ufe.F = formula;
                break;
            default:
                throw new ArgumentException($"Unsupported event cell name: {eventName}");
        }
    }
}
