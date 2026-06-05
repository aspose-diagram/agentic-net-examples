using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Paths to the two diagram versions
                string oldDiagramPath = "oldDiagram.vsdx";
                string newDiagramPath = "newDiagram.vsdx";

                // Load diagrams
                Diagram oldDiagram = new Diagram(oldDiagramPath);
                Diagram newDiagram = new Diagram(newDiagramPath);

                // List of event cell names to compare
                var eventNames = new List<string>
                {
                    "EventXFMod",
                    "EventDblClick",
                    "EventDrop",
                    "EventMultiDrop",
                    "TheText",
                    "TheData"
                };

                // Iterate through pages by name to handle possible different ordering
                foreach (Page oldPage in oldDiagram.Pages)
                {
                    // Find matching page in new diagram by name
                    Page newPage = newDiagram.Pages.GetPage(oldPage.Name);
                    if (newPage == null)
                    {
                        Console.WriteLine($"Page '{oldPage.Name}' not found in new diagram.");
                        continue;
                    }

                    // Iterate through shapes on the old page
                    foreach (Shape oldShape in oldPage.Shapes)
                    {
                        // Try to get the same shape by ID on the new page
                        Shape newShape = newPage.Shapes.GetShape(oldShape.ID);
                        if (newShape == null)
                        {
                            Console.WriteLine($"Shape ID {oldShape.ID} on page '{oldPage.Name}' not found in new diagram.");
                            continue;
                        }

                        // Compare each event cell
                        foreach (string evName in eventNames)
                        {
                            string oldFormula = GetEventFormula(oldShape, evName);
                            string newFormula = GetEventFormula(newShape, evName);

                            // Treat null and empty as equivalent
                            oldFormula = oldFormula ?? string.Empty;
                            newFormula = newFormula ?? string.Empty;

                            if (!oldFormula.Equals(newFormula, StringComparison.Ordinal))
                            {
                                Console.WriteLine($"Change detected - Page: '{oldPage.Name}', Shape ID: {oldShape.ID}, Event: {evName}");
                                Console.WriteLine($"  Old: {oldFormula}");
                                Console.WriteLine($"  New: {newFormula}");
                            }
                        }
                    }
                }

                // Optional: keep console window open when run outside debugger
                Console.WriteLine("Comparison complete.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Helper method to retrieve the formula string for a given event cell name
        private static string GetEventFormula(Shape shape, string eventName)
        {
            if (shape?.Event == null)
                return null;

            // Use a switch to access the specific event cell
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
    }