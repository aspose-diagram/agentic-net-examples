using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Input Visio file (template or existing diagram)
            string inputPath = "template.vsdx";
            // Output Visio file after modifications
            string outputPath = "updated.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Ensure there is at least one page to work with
            Page page;
            if (diagram.Pages.Count > 0)
            {
                page = diagram.Pages[0];
            }
            else
            {
                page = new Page(1);
                diagram.Pages.Add(page);
            }

            // Add a CommandButton ActiveX control at (2, 2) inches, size 1.5 x 0.5 inches
            long btnShapeId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);
            Shape btnShape = page.Shapes.GetShape(btnShapeId);

            // Cast to the specific control type and set its caption
            CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)btnShape.ActiveXControl;
            cmdBtn.Caption = "Click Me";

            // Example: locate all TextBox ActiveX controls on the page and update their text
            foreach (Shape shape in page.Shapes)
            {
                if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.TextBox)
                {
                    TextBoxActiveXControl txtBox = (TextBoxActiveXControl)shape.ActiveXControl;
                    txtBox.Text = "Updated Text";
                }
            }

            // Save the diagram with auto‑fit to drawing content enabled
            DiagramSaveOptions saveOptions = new DiagramSaveOptions(SaveFileFormat.Vsdx);
            saveOptions.AutoFitPageToDrawingContent = true;
            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
