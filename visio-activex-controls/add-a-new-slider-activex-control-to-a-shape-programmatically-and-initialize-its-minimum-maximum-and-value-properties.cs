using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
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

        // Load the Visio diagram inside a using block.
        using (Diagram diagram = new Diagram(inputPath))
        {
            try
            {
                // Access the first page of the diagram.
                Page page = diagram.Pages[0];

                // Insert a SpinButton ActiveX control (used as a slider alternative).
                // Parameters: control type, pinX, pinY, width, height (all in inches).
                long shapeId = page.AddActiveXControl(ControlType.SpinButton, 2.0, 2.0, 2.0, 0.5);

                // Retrieve the newly added shape by its structural ID.
                Shape shape = page.Shapes.GetShape(shapeId);

                // Cast the generic ActiveXControl to the specific SpinButton control.
                SpinButtonActiveXControl spinButton = shape.ActiveXControl as SpinButtonActiveXControl;
                if (spinButton == null)
                {
                    Console.Error.WriteLine("Failed to retrieve SpinButtonActiveXControl from the shape.");
                    return;
                }

                // Set the current value (position) of the control.
                spinButton.Position = 50;

                // TODO: If the SpinButtonActiveXControl exposes Minimum and Maximum properties,
                // initialize them here (e.g., spinButton.Minimum = 0; spinButton.Maximum = 100;).
                // The current API documentation does not list such members.

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
            catch (Exception ex)
            {
                // Write any errors that occur during processing.
                Console.Error.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}