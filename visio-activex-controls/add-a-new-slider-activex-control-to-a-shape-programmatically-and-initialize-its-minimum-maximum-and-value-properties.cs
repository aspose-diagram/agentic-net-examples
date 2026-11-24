using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioFile> <outputVisioFile>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    // Add a new blank page.
                    diagram.Pages.Add(new Page());
                }

                // Use the first page.
                Page page = diagram.Pages[0];

                // Define position and size for the ActiveX control (in inches).
                double posX = 2.0;   // X position
                double posY = 2.0;   // Y position
                double width = 3.0;  // Width
                double height = 0.5; // Height

                // Insert a SpinButton ActiveX control (closest to a slider).
                long controlShapeId = page.AddActiveXControl(ControlType.SpinButton, posX, posY, width, height);

                // Retrieve the shape that hosts the ActiveX control.
                Shape controlShape = page.Shapes.GetShape(controlShapeId);

                // Cast the generic ActiveXControl to the specific SpinButton type.
                SpinButtonActiveXControl spinButton = controlShape.ActiveXControl as SpinButtonActiveXControl;
                if (spinButton == null)
                {
                    Console.Error.WriteLine("Failed to cast the ActiveX control to SpinButtonActiveXControl.");
                }
                else
                {
                    // Set the current value of the spin button.
                    spinButton.Position = 50;

                    // NOTE: The SpinButtonActiveXControl does not expose Minimum/Maximum properties
                    // in the current Aspose.Diagram API, so only the Position (value) can be set.
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}