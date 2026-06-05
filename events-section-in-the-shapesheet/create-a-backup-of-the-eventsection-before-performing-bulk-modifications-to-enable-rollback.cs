using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        // Helper to retrieve the formula of a specific event cell
        static string GetEventFormula(Shape shape, string eventName)
        {
            switch (eventName)
            {
                case "EventXFMod":
                    return shape.Event.EventXFMod?.Ufe?.F ?? string.Empty;
                case "EventDblClick":
                    return shape.Event.EventDblClick?.Ufe?.F ?? string.Empty;
                case "EventDrop":
                    return shape.Event.EventDrop?.Ufe?.F ?? string.Empty;
                case "EventMultiDrop":
                    return shape.Event.EventMultiDrop?.Ufe?.F ?? string.Empty;
                case "TheText":
                    return shape.Event.TheText?.Ufe?.F ?? string.Empty;
                case "TheData":
                    return shape.Event.TheData?.Ufe?.F ?? string.Empty;
                default:
                    return string.Empty;
            }
        }

        // Helper to set the formula of a specific event cell
        static void SetEventFormula(Shape shape, string eventName, string formula)
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
            }
        }

        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output_modified.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Backup dictionary: Shape ID -> (Event Name -> Formula)
                Dictionary<long, Dictionary<string, string>> eventBackup = new Dictionary<long, Dictionary<string, string>>();

                // List of event cell names to backup
                string[] eventNames = new string[]
                {
                    "EventXFMod",
                    "EventDblClick",
                    "EventDrop",
                    "EventMultiDrop",
                    "TheText",
                    "TheData"
                };

                // Iterate through all pages and shapes to backup event formulas
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        var shapeBackup = new Dictionary<string, string>();
                        foreach (string evName in eventNames)
                        {
                            string formula = GetEventFormula(shape, evName);
                            shapeBackup[evName] = formula;
                        }
                        eventBackup[shape.ID] = shapeBackup;
                    }
                }

                // -----------------------------------------------------------------
                // Perform bulk modifications on the EventSection here.
                // Example: set a double-click event on all shapes (placeholder).
                // -----------------------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example modification: set a simple double-click event
                        shape.Event.EventDblClick.Ufe.F = "CALLTHIS(\"MyMacro\")";
                    }
                }

                // -----------------------------------------------------------------
                // If a rollback is required, restore the original event formulas.
                // -----------------------------------------------------------------
                bool needRollback = false; // Set to true to trigger rollback

                if (needRollback)
                {
                    foreach (Page page in diagram.Pages)
                    {
                        foreach (Shape shape in page.Shapes)
                        {
                            if (eventBackup.TryGetValue(shape.ID, out var savedEvents))
                            {
                                foreach (var kvp in savedEvents)
                                {
                                    SetEventFormula(shape, kvp.Key, kvp.Value);
                                }
                            }
                        }
                    }
                }

                // Save the modified (or rolled‑back) diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }