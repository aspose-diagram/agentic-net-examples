using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

public class Program
{
    public static void Main()
    {
        try
        {

            // Path to the Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Specify the shape ID that contains the ActiveX control
            // Replace this with the actual ID of your target shape
            long shapeId = 1;

            // Retrieve the shape from the page
            Shape shape = page.Shapes.GetShape(shapeId);

            // Get the ActiveX control associated with the shape
            var activeX = shape.ActiveXControl;

            if (activeX == null)
            {
                Console.WriteLine("The specified shape does not contain an ActiveX control.");
                return;
            }

            // Process the control based on its specific type
            switch (activeX.Type)
            {
                case ControlType.CommandButton:
                    var commandButton = (CommandButtonActiveXControl)activeX;
                    Console.WriteLine($"CommandButton Caption: {commandButton.Caption}");
                    break;

                case ControlType.Image:
                    var imageControl = (ImageActiveXControl)activeX;
                    Console.WriteLine($"Image Control Width: {imageControl.Width}, Height: {imageControl.Height}");
                    break;

                case ControlType.CheckBox:
                    var checkBox = (CheckBoxActiveXControl)activeX;
                    Console.WriteLine($"CheckBox Value: {checkBox.Value}");
                    break;

                default:
                    Console.WriteLine($"ActiveX control type: {activeX.Type}");
                    break;
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
