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

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check if the shape contains an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        try
                        {
                            // Attempt to cast the control to a CommandButtonActiveXControl
                            CommandButtonActiveXControl commandButton = (CommandButtonActiveXControl)shape.ActiveXControl;

                            // Cast succeeded – modify a property as needed
                            commandButton.Caption = "Handled Button";
                            Console.WriteLine($"Shape ID {shape.ID}: CommandButton caption updated.");
                        }
                        catch (InvalidCastException)
                        {
                            // Cast failed – handle the incompatibility gracefully
                            Console.WriteLine($"Shape ID {shape.ID}: ActiveX control is not a CommandButton (type: {shape.ActiveXControl.Type}).");
                        }
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to {outputPath}");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
