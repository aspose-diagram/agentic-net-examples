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

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a CommandButton ActiveX control to the page
            // Parameters: ControlType, PinX, PinY, Width, Height (all in inches)
            long controlId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);

            // Retrieve the shape that represents the newly added control
            Shape controlShape = page.Shapes.GetShape(controlId);

            // Cast the generic ActiveXControl to the specific CommandButton type
            CommandButtonActiveXControl button = (CommandButtonActiveXControl)controlShape.ActiveXControl;

            // Update properties of the ActiveX control
            button.Caption = "Submit";                     // Set the button caption
            controlShape.XForm.Width.Value = 2.0;          // Adjust width (in inches)
            controlShape.XForm.Height.Value = 0.5;         // Adjust height (in inches)

            // Save the modified diagram to a new file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
