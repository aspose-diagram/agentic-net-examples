using System.IO;
using System;
using System.Collections.Generic;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram (uses the provided load rule)
            Diagram diagram = new Diagram("input.vsdx");

            // Backup the Event section of each shape across all pages
            // The key is the shape ID, the value is a deep clone of its Event object
            var eventBackup = new Dictionary<long, Event>();

            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Event != null)
                    {
                        // Event.Clone creates a deep copy (as per documentation)
                        eventBackup[shape.ID] = (Event)shape.Event.Clone();
                    }
                }
            }

            // -----------------------------------------------------------------
            // Perform bulk modifications on the diagram here.
            // Example: shape.Event = null; // (placeholder for actual changes)
            // -----------------------------------------------------------------

            // If a rollback is needed, restore the original Event objects from the backup
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (eventBackup.TryGetValue(shape.ID, out Event originalEvent))
                    {
                        // Clone again to avoid sharing the same instance between shapes
                        shape.Event = (Event)originalEvent.Clone();
                    }
                    else
                    {
                        // No original event existed for this shape; ensure it's cleared
                        shape.Event = null;
                    }
                }
            }

            // Save the modified (or rolled‑back) diagram (uses the provided save rule)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
