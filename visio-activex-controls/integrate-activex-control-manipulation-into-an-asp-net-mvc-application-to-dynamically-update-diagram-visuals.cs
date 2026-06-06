using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = @"C:\Diagrams\input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = @"C:\Diagrams\output.vsdx";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];

            long controlShapeId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);
            Shape controlShape = page.Shapes.GetShape(controlShapeId);
            CommandButtonActiveXControl button = (CommandButtonActiveXControl)controlShape.ActiveXControl;

            button.Caption = "Click Me!";
            button.Width = 1.5;
            button.Height = 0.5;

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}