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
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> <outputDiagramPath>");
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
                // Ensure there is at least one page to work with.
                Page page;
                if (diagram.Pages.Count > 0)
                {
                    page = diagram.Pages[0];
                }
                else
                {
                    page = new Page();
                    diagram.Pages.Add(page);
                }

                // Insert a CommandButton ActiveX control onto the page.
                // AddActiveXControl returns the shape ID (long), not a Shape object.
                long controlId = page.AddActiveXControl(ControlType.CommandButton, 2.0, 2.0, 1.5, 0.5);

                // Retrieve the Shape instance using the returned ID.
                Shape controlShape = page.Shapes.GetShape(controlId);

                // Verify that the shape and its ActiveX control were created successfully.
                if (controlShape != null && controlShape.ActiveXControl != null &&
                    controlShape.ActiveXControl.Type == ControlType.CommandButton)
                {
                    // Cast to the specific control class to access its properties.
                    CommandButtonActiveXControl button = (CommandButtonActiveXControl)controlShape.ActiveXControl;

                    // Set the caption that will appear on the button.
                    button.Caption = "Click Me";

                    // Width and Height are already defined by AddActiveXControl; they can be adjusted if needed.
                    // Example (optional):
                    // button.Width = 2.0;
                    // button.Height = 0.6;
                }
                else
                {
                    Console.Error.WriteLine("Failed to create or retrieve the CommandButton ActiveX control.");
                }

                // Save the modified diagram to the specified output path in VSDX format.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}