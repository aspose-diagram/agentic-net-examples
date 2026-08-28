using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        // Example: assume we have Visio file bytes in visioBytes
        byte[] visioBytes = GetVisioBytes(); // placeholder method
        using (MemoryStream inputStream = new MemoryStream(visioBytes))
        {
            // Load diagram from memory stream
            Diagram diagram = new Diagram(inputStream);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Update properties based on the control type
                        if (shape.ActiveXControl.Type == ControlType.CommandButton)
                        {
                            CommandButtonActiveXControl btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                            btn.Caption = "Updated Caption";
                            btn.Width = 2.0;   // inches
                            btn.Height = 0.5;  // inches
                        }
                        else if (shape.ActiveXControl.Type == ControlType.CheckBox)
                        {
                            CheckBoxActiveXControl chk = (CheckBoxActiveXControl)shape.ActiveXControl;
                            // Set the checkbox to checked state
                            chk.Value = CheckValueType.Checked;
                        }
                        // Additional control types can be handled here
                    }
                }
            }

            // Save the modified diagram back to a memory stream
            using (MemoryStream outputStream = new MemoryStream())
            {
                diagram.Save(outputStream, SaveFileFormat.Vsdx);
                byte[] updatedBytes = outputStream.ToArray();
                Console.WriteLine($"Updated diagram size: {updatedBytes.Length} bytes");
                // The updatedBytes array can now be used as needed (e.g., send over network, store in DB)
            }
        }
    }

    // Placeholder method to provide Visio file bytes.
    // Replace with actual data source in a real scenario.
    static byte[] GetVisioBytes()
    {
        // Create a simple diagram with a command button ActiveX control for demonstration.
        using (Diagram emptyDiagram = new Diagram())
        {
            Page page = emptyDiagram.Pages[0];
            long ctrlId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);
            Shape ctrlShape = page.Shapes.GetShape(ctrlId);
            CommandButtonActiveXControl btn = (CommandButtonActiveXControl)ctrlShape.ActiveXControl;
            btn.Caption = "Initial Caption";

            using (MemoryStream ms = new MemoryStream())
            {
                emptyDiagram.Save(ms, SaveFileFormat.Vsdx);
                return ms.ToArray();
            }
        }
    }
}
