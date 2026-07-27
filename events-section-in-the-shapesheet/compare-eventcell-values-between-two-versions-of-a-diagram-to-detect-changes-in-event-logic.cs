using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two diagram versions (replace with actual file paths)
                string oldDiagramPath = "oldDiagram.vsdx";
                string newDiagramPath = "newDiagram.vsdx";

                // Load diagrams
                Diagram oldDiagram = new Diagram(oldDiagramPath);
                Diagram newDiagram = new Diagram(newDiagramPath);

                // Compare each page by index (assumes same page order)
                int pageCount = Math.Min(oldDiagram.Pages.Count, newDiagram.Pages.Count);
                for (int p = 0; p < pageCount; p++)
                {
                    Page oldPage = oldDiagram.Pages[p];
                    Page newPage = newDiagram.Pages[p];

                    // Build a lookup of shapes in the new diagram by ID for quick access
                    var newShapesById = new System.Collections.Generic.Dictionary<long, Shape>();
                    foreach (Shape ns in newPage.Shapes)
                    {
                        newShapesById[ns.ID] = ns;
                    }

                    // Iterate shapes in the old diagram
                    foreach (Shape oldShape in oldPage.Shapes)
                    {
                        if (!newShapesById.TryGetValue(oldShape.ID, out Shape newShape))
                        {
                            Console.WriteLine($"Shape ID {oldShape.ID} exists in old diagram but not in new diagram (Page: {oldPage.Name}).");
                            continue;
                        }

                        // Compare each supported event cell
                        CompareEvent(oldShape, newShape, oldPage.Name, "EventXFMod");
                        CompareEvent(oldShape, newShape, oldPage.Name, "EventDblClick");
                        CompareEvent(oldShape, newShape, oldPage.Name, "EventDrop");
                        CompareEvent(oldShape, newShape, oldPage.Name, "EventMultiDrop");
                        CompareEvent(oldShape, newShape, oldPage.Name, "TheText");
                        CompareEvent(oldShape, newShape, oldPage.Name, "TheData");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Retrieves the formula string for a given event name from a shape.
        private static string GetEventFormula(Shape shape, string eventName)
        {
            switch (eventName)
            {
                case "EventXFMod":
                    return shape.Event.EventXFMod?.Ufe?.F;
                case "EventDblClick":
                    return shape.Event.EventDblClick?.Ufe?.F;
                case "EventDrop":
                    return shape.Event.EventDrop?.Ufe?.F;
                case "EventMultiDrop":
                    return shape.Event.EventMultiDrop?.Ufe?.F;
                case "TheText":
                    return shape.Event.TheText?.Ufe?.F;
                case "TheData":
                    return shape.Event.TheData?.Ufe?.F;
                default:
                    return null;
            }
        }

        // Compares a specific event between two shapes and reports differences.
        private static void CompareEvent(Shape oldShape, Shape newShape, string pageName, string eventName)
        {
            string oldFormula = GetEventFormula(oldShape, eventName);
            string newFormula = GetEventFormula(newShape, eventName);

            // Normalize nulls to empty strings for comparison
            oldFormula = oldFormula ?? string.Empty;
            newFormula = newFormula ?? string.Empty;

            if (!oldFormula.Equals(newFormula, StringComparison.Ordinal))
            {
                Console.WriteLine($"Page '{pageName}', Shape ID {oldShape.ID}: Event '{eventName}' changed.");
                Console.WriteLine($"    Old: \"{oldFormula}\"");
                Console.WriteLine($"    New: \"{newFormula}\"");
            }
        }
    }