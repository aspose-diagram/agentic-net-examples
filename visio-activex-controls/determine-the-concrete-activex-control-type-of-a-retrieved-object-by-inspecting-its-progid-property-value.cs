using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Example shape ID – replace with the actual ID of the shape you want to inspect
                long shapeId = 1;

                // Retrieve the shape from the first page
                Shape shape = diagram.Pages[0].Shapes.GetShape(shapeId);

                // Ensure the shape contains an ActiveX control
                if (shape.ActiveXControl != null)
                {
                    // Determine the concrete control type using the Type property (ControlType enum)
                    ControlType controlType = shape.ActiveXControl.Type;

                    // Output the detected control type
                    Console.WriteLine($"Shape ID {shapeId} contains an ActiveX control of type: {controlType}");
                }
                else
                {
                    Console.WriteLine($"Shape ID {shapeId} does not contain an ActiveX control.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }