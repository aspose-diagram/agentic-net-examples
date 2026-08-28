using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Name of the master shape whose EventSection cells will be cloned
            string masterName = "MasterShape";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Retrieve the master by name
            Master master = diagram.Masters.GetMasterByName(masterName);
            if (master == null)
            {
                throw new Exception($"Master \"{masterName}\" not found in the diagram.");
            }

            // Get the shape that defines the EventSection within the master
            Shape masterShape = null;
            foreach (Shape s in master.Shapes)
            {
                masterShape = s;
                break; // Assume the first shape in the master contains the events
            }
            if (masterShape == null)
            {
                throw new Exception($"No shape found inside master \"{masterName}\".");
            }

            // List of event cells to copy
            // Add or remove event names as required
            Action<Shape, Shape> copyEvents = (target, source) =>
            {
                // EventXFMod
                target.Event.EventXFMod.Ufe.F = source.Event.EventXFMod.Ufe.F;
                // EventDblClick
                target.Event.EventDblClick.Ufe.F = source.Event.EventDblClick.Ufe.F;
                // EventDrop
                target.Event.EventDrop.Ufe.F = source.Event.EventDrop.Ufe.F;
                // EventMultiDrop
                target.Event.EventMultiDrop.Ufe.F = source.Event.EventMultiDrop.Ufe.F;
                // TheText
                target.Event.TheText.Ufe.F = source.Event.TheText.Ufe.F;
                // TheData
                target.Event.TheData.Ufe.F = source.Event.TheData.Ufe.F;
            };

            // Iterate through all pages and shapes, cloning the EventSection to duplicates
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify duplicate shapes that are instances of the same master
                    if (shape.Master != null && shape.Master.Name == master.Name)
                    {
                        // Clone event cells from the master shape to this duplicate
                        copyEvents(shape, masterShape);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
