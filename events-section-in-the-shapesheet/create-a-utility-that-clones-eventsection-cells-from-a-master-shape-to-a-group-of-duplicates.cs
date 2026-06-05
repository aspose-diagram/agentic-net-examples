using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";
                // Name of the master shape whose Event cells will be cloned
                string masterName = "MyMaster";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the master by name
                Master master = diagram.Masters.GetMasterByName(masterName);
                if (master == null)
                    throw new Exception($"Master with name '{masterName}' not found.");

                // Ensure the master contains at least one shape (the master shape)
                if (master.Shapes.Count == 0)
                    throw new Exception($"Master '{masterName}' does not contain any shapes.");

                // The first shape in the master is the template shape
                Shape masterShape = master.Shapes[0];

                // Iterate through all pages and shapes to find duplicates of the master
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify shapes that are instances of the specified master
                        if (shape.Master != null && shape.Master.Name == master.Name)
                        {
                            // Clone Event cells from the master shape to the duplicate shape
                            // EventXFMod
                            if (masterShape.Event.EventXFMod != null && shape.Event.EventXFMod != null)
                                shape.Event.EventXFMod.Ufe.F = masterShape.Event.EventXFMod.Ufe.F;

                            // EventDblClick
                            if (masterShape.Event.EventDblClick != null && shape.Event.EventDblClick != null)
                                shape.Event.EventDblClick.Ufe.F = masterShape.Event.EventDblClick.Ufe.F;

                            // EventDrop
                            if (masterShape.Event.EventDrop != null && shape.Event.EventDrop != null)
                                shape.Event.EventDrop.Ufe.F = masterShape.Event.EventDrop.Ufe.F;

                            // EventMultiDrop
                            if (masterShape.Event.EventMultiDrop != null && shape.Event.EventMultiDrop != null)
                                shape.Event.EventMultiDrop.Ufe.F = masterShape.Event.EventMultiDrop.Ufe.F;

                            // TheText
                            if (masterShape.Event.TheText != null && shape.Event.TheText != null)
                                shape.Event.TheText.Ufe.F = masterShape.Event.TheText.Ufe.F;

                            // TheData
                            if (masterShape.Event.TheData != null && shape.Event.TheData != null)
                                shape.Event.TheData.Ufe.F = masterShape.Event.TheData.Ufe.F;
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