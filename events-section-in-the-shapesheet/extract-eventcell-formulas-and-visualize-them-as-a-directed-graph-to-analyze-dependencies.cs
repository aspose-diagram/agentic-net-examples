using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Expect the input Visio file path as the first argument.
                if (args.Length == 0)
                {
                    Console.WriteLine("Usage: EventCellGraph <input-visio-file>");
                    return;
                }

                string inputPath = args[0];

                // Load the Visio diagram.
                Diagram diagram = new Diagram(inputPath);

                Console.WriteLine("Extracting EventCell formulas and building dependency graph...");

                // Iterate through all pages and shapes.
                foreach (Page page in diagram.Pages)
                {
                    // Skip any empty pages.
                    if (page.Shapes == null) continue;

                    foreach (Shape shape in page.Shapes)
                    {
                        // Collect event formulas for the current shape.
                        bool hasAnyEvent = false;

                        // Helper local function to process a single event cell.
                        void ProcessEvent(string eventName, string formula)
                        {
                            if (!string.IsNullOrWhiteSpace(formula))
                            {
                                if (!hasAnyEvent)
                                {
                                    Console.WriteLine($"Shape ID {shape.ID} (NameU: {shape.NameU}) events:");
                                    hasAnyEvent = true;
                                }
                                Console.WriteLine($"  {eventName} -> \"{formula}\"");
                            }
                        }

                        // Event cells are accessed via the Event property.
                        // Each event cell contains a Ufe (Universal Formula Expression) object with the formula string in its F property.
                        ProcessEvent("EventXFMod", shape.Event.EventXFMod?.Ufe?.F);
                        ProcessEvent("EventDblClick", shape.Event.EventDblClick?.Ufe?.F);
                        ProcessEvent("EventDrop", shape.Event.EventDrop?.Ufe?.F);
                        ProcessEvent("EventMultiDrop", shape.Event.EventMultiDrop?.Ufe?.F);
                        ProcessEvent("TheText", shape.Event.TheText?.Ufe?.F);
                        ProcessEvent("TheData", shape.Event.TheData?.Ufe?.F);
                    }
                }

                // Optionally, save the diagram (unchanged) to demonstrate proper lifecycle handling.
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }