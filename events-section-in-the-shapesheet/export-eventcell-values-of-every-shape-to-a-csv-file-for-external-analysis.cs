using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the Visio file to be processed
                string visioPath = "input.vsdx";

                // Path to the CSV file that will contain the exported event formulas
                string csvPath = "shape_events.csv";

                // Load the Visio diagram
                Diagram diagram = new Diagram(visioPath);

                // Open a StreamWriter for the CSV output
                using (StreamWriter writer = new StreamWriter(csvPath))
                {
                    // Write CSV header
                    writer.WriteLine("PageIndex,ShapeID,ShapeNameU,EventXFMod,EventDblClick,EventDrop,EventMultiDrop,TheText,TheData");

                    // Iterate through all pages
                    for (int pageIndex = 0; pageIndex < diagram.Pages.Count; pageIndex++)
                    {
                        Page page = diagram.Pages[pageIndex];

                        // Iterate through all shapes on the current page
                        foreach (Shape shape in page.Shapes)
                        {
                            // Skip shapes that are marked as deleted
                            if (shape.Del == BOOL.True)
                                continue;

                            // Helper to safely retrieve an event formula; returns empty string if not set
                            string GetEventFormula(Func<string> getter)
                            {
                                try
                                {
                                    string formula = getter();
                                    return string.IsNullOrEmpty(formula) ? "" : formula.Replace(",", ";");
                                }
                                catch
                                {
                                    return "";
                                }
                            }

                            // Retrieve event formulas using the Ufe.F property
                            string eventXFMod = GetEventFormula(() => shape.Event.EventXFMod.Ufe.F);
                            string eventDblClick = GetEventFormula(() => shape.Event.EventDblClick.Ufe.F);
                            string eventDrop = GetEventFormula(() => shape.Event.EventDrop.Ufe.F);
                            string eventMultiDrop = GetEventFormula(() => shape.Event.EventMultiDrop.Ufe.F);
                            string theText = GetEventFormula(() => shape.Event.TheText.Ufe.F);
                            string theData = GetEventFormula(() => shape.Event.TheData.Ufe.F);

                            // Compose CSV line
                            string line = string.Join(",",
                                pageIndex,
                                shape.ID,
                                shape.NameU,
                                eventXFMod,
                                eventDblClick,
                                eventDrop,
                                eventMultiDrop,
                                theText,
                                theData);

                            writer.WriteLine(line);
                        }
                    }
                }

                Console.WriteLine($"Event cell values exported to '{csvPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }