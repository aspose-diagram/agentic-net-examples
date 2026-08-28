using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Create a new empty diagram
                Diagram diagram = new Diagram();

                // -------------------------------------------------
                // Create a custom master that will contain a shape
                // -------------------------------------------------
                Master helpMaster = new Master();
                helpMaster.Name = "HelpMaster";
                helpMaster.NameU = "HelpMaster";

                // Create a shape that will be part of the master
                Shape masterShape = new Shape();
                masterShape.Type = TypeValue.Shape;                     // Define as a regular shape
                masterShape.XForm.PinX.Value = 2.0;                     // Center X (in inches)
                masterShape.XForm.PinY.Value = 2.0;                     // Center Y (in inches)
                masterShape.XForm.Width.Value = 2.0;                    // Width (in inches)
                masterShape.XForm.Height.Value = 1.0;                   // Height (in inches)

                // Add some visible text to the master shape
                masterShape.Text.Value.Clear();
                masterShape.Text.Value.Add(new Txt("Help Shape"));

                // Add the shape to the master's shape collection
                helpMaster.Shapes.Add(masterShape);

                // Add the master to the diagram's master collection
                diagram.Masters.Add(helpMaster);

                // -------------------------------------------------
                // Place an instance of the master on the active page
                // -------------------------------------------------
                // AddShape returns the shape ID (long)
                long instanceId = diagram.ActivePage.AddShape(4.0, 5.0, helpMaster.Name, false);
                Shape instanceShape = diagram.ActivePage.Shapes.GetShape(instanceId);

                // -------------------------------------------------
                // Add a hyperlink to the shape instance
                // -------------------------------------------------
                Hyperlink hyperlink = new Hyperlink();
                hyperlink.Name = "HelpLink";
                // Path to the help document (adjust as needed)
                hyperlink.Address.Value = "file:///C:/HelpDocs/help.pdf";
                hyperlink.Description.Value = "Open Help Document";

                // Attach the hyperlink to the shape
                instanceShape.Hyperlinks.Add(hyperlink);

                // -------------------------------------------------
                // Save the diagram to a VSDX file
                // -------------------------------------------------
                diagram.Save("HelpDiagram.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.NullReferenceException ex)
            {
                Console.Error.WriteLine($"[NullReferenceException] {ex.Message}");
            }
    }
    }