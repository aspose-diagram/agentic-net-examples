using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);
            Page page = diagram.Pages[0];

            long controlShapeId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);
            Shape controlShape = page.Shapes.GetShape(controlShapeId);
            CommandButtonActiveXControl button = (CommandButtonActiveXControl)controlShape.ActiveXControl;

            button.Caption = "Submit";

            controlShape.XForm.Width.Value = 1.5;
            controlShape.XForm.Height.Value = 0.5;

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("ActiveX control updated and diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("An error occurred: " + ex.Message);
            throw;
        }
    }
}