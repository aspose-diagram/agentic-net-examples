using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the source Visio diagram
                Diagram diagram = new Diagram("input.vsdx");

                // Assume the master shape is on the first page and has a known name
                Page page = diagram.Pages[0];
                string masterShapeName = "MasterShape"; // replace with actual master shape name

                // Locate the master shape
                Shape? masterShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == masterShapeName)
                    {
                        masterShape = shape;
                        break;
                    }
                }

                if (masterShape == null)
                {
                    throw new Exception($"Master shape '{masterShapeName}' not found.");
                }

                // Capture the event formulas from the master shape
                var eventFormulas = new Dictionary<string, string>
                {
                    { "EventXFMod",   masterShape.Event.EventXFMod.Ufe.F },
                    { "EventDblClick",masterShape.Event.EventDblClick.Ufe.F },
                    { "EventDrop",    masterShape.Event.EventDrop.Ufe.F },
                    { "EventMultiDrop",masterShape.Event.EventMultiDrop.Ufe.F },
                    { "TheText",      masterShape.Event.TheText.Ufe.F },
                    { "TheData",      masterShape.Event.TheData.Ufe.F }
                };

                // Clone the event section to all duplicate shapes that share the same master
                foreach (Shape shape in page.Shapes)
                {
                    // Skip the original master shape
                    if (shape.ID == masterShape.ID)
                        continue;

                    // Ensure the shape uses the same master (i.e., is a duplicate)
                    if (shape.Master != null && masterShape.Master != null && shape.Master.Name == masterShape.Master.Name)
                    {
                        // Apply each captured event formula to the duplicate shape
                        shape.Event.EventXFMod.Ufe.F    = eventFormulas["EventXFMod"];
                        shape.Event.EventDblClick.Ufe.F = eventFormulas["EventDblClick"];
                        shape.Event.EventDrop.Ufe.F    = eventFormulas["EventDrop"];
                        shape.Event.EventMultiDrop.Ufe.F = eventFormulas["EventMultiDrop"];
                        shape.Event.TheText.Ufe.F      = eventFormulas["TheText"];
                        shape.Event.TheData.Ufe.F      = eventFormulas["TheData"];
                    }
                }

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }