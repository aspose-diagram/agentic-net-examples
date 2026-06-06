using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Insert a CommandButton ActiveX control onto the page
            long shapeId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);
            Shape shape = page.Shapes.GetShape(shapeId);

            // Ensure the shape contains an ActiveX control
            if (shape?.ActiveXControl == null)
            {
                Console.WriteLine("Failed to add ActiveX control.");
                return;
            }

            // Cast to the specific CommandButton control type
            CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;

            // Log the initial value of the Caption property
            LogChange("Caption", "<none>", button.Caption);

            // Change the Caption property and log the change
            string oldCaption = button.Caption;
            button.Caption = "Submit";
            LogChange("Caption", oldCaption, button.Caption);

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Simple logger that writes property changes to the console
    static void LogChange(string propertyName, string oldValue, string newValue)
    {
        Console.WriteLine($"Property '{propertyName}' changed from '{oldValue}' to '{newValue}'.");
    }
}
