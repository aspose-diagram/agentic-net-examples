using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the Visio diagram; can be passed as a command‑line argument.
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Iterate through all pages.
                foreach (Page page in diagram.Pages)
                {
                    Console.WriteLine($"Page: {page.Name} (ID: {page.ID})");

                    // Iterate through all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        var eventList = new List<(string EventName, string Formula)>();

                        // Check each supported event cell for a non‑empty formula.
                        if (!string.IsNullOrWhiteSpace(shape.Event.EventXFMod?.Ufe?.F))
                            eventList.Add(("EventXFMod", shape.Event.EventXFMod.Ufe.F));

                        if (!string.IsNullOrWhiteSpace(shape.Event.EventDblClick?.Ufe?.F))
                            eventList.Add(("EventDblClick", shape.Event.EventDblClick.Ufe.F));

                        if (!string.IsNullOrWhiteSpace(shape.Event.EventDrop?.Ufe?.F))
                            eventList.Add(("EventDrop", shape.Event.EventDrop.Ufe.F));

                        if (!string.IsNullOrWhiteSpace(shape.Event.EventMultiDrop?.Ufe?.F))
                            eventList.Add(("EventMultiDrop", shape.Event.EventMultiDrop.Ufe.F));

                        if (!string.IsNullOrWhiteSpace(shape.Event.TheText?.Ufe?.F))
                            eventList.Add(("TheText", shape.Event.TheText.Ufe.F));

                        if (!string.IsNullOrWhiteSpace(shape.Event.TheData?.Ufe?.F))
                            eventList.Add(("TheData", shape.Event.TheData.Ufe.F));

                        // If the shape has any custom event formulas, output them.
                        if (eventList.Count > 0)
                        {
                            Console.WriteLine($"  Shape: {shape.Name} (ID: {shape.ID})");
                            foreach (var ev in eventList)
                            {
                                Console.WriteLine($"    {ev.EventName}: \"{ev.Formula}\"");
                            }
                        }
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }