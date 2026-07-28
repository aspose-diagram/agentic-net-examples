using System;
using System.Runtime.InteropServices;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Add a CommandButton ActiveX control to the first page
                Page page = diagram.Pages[0];
                long shapeId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);
                Shape shape = page.Shapes.GetShape(shapeId);

                // Cast the generic ActiveXControl to the specific CommandButton type
                CommandButtonActiveXControl button = (CommandButtonActiveXControl)shape.ActiveXControl;

                // Set some basic properties
                button.Caption = "Click Me";

                // Attempt to access a property that may require special permissions
                try
                {
                    // Accessing Height may throw COMException if permissions are insufficient
                    double height = button.Height;
                    Console.WriteLine($"Button Height: {height}");
                }
                catch (COMException comEx)
                {
                    // Handle the COMException gracefully
                    Console.WriteLine("COMException caught while accessing Height property:");
                    Console.WriteLine(comEx.Message);
                }

                // Attempt to modify a property that could also be restricted
                try
                {
                    // Setting IsEnabled might require elevated permissions
                    button.IsEnabled = false;
                    Console.WriteLine("Button IsEnabled set to false successfully.");
                }
                catch (COMException comEx)
                {
                    Console.WriteLine("COMException caught while setting IsEnabled property:");
                    Console.WriteLine(comEx.Message);
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