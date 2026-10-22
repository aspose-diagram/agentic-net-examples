using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Prompt user for the input Visio diagram path (VSDX)
        Console.Write("Enter the full path to the input Visio diagram (VSDX): ");
        string inputPath = Console.ReadLine() ?? "";

        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found at '{inputPath}'.");
            return;
        }

        // Prompt user for the output Visio diagram path (VSDX)
        Console.Write("Enter the full path for the output Visio diagram (VSDX): ");
        string outputPath = Console.ReadLine() ?? "";

        // Prompt user for the caption to set on the CommandButton ActiveX control
        Console.Write("Enter the caption text for the CommandButton ActiveX control: ");
        string buttonCaption = Console.ReadLine() ?? "";

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Ensure the diagram contains at least one page
                if (diagram.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Error: The diagram contains no pages.");
                    return;
                }

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Define position and size for the new CommandButton ActiveX control (in inches)
                double posX = 2.0;   // X coordinate
                double posY = 2.0;   // Y coordinate
                double width = 1.5;  // Width
                double height = 0.5; // Height

                // Insert a CommandButton ActiveX control onto the page.
                // AddActiveXControl returns the shape ID (long), so retrieve the Shape object.
                long controlShapeId = page.AddActiveXControl(ControlType.CommandButton, posX, posY, width, height);
                Shape controlShape = page.Shapes.GetShape(controlShapeId);

                // Retrieve the ActiveX control from the shape and cast to the specific type
                CommandButtonActiveXControl commandButton = (CommandButtonActiveXControl)controlShape.ActiveXControl;

                // Set properties of the CommandButton ActiveX control
                commandButton.Caption = buttonCaption;   // Set displayed caption
                commandButton.Width = width;             // Set control width
                commandButton.Height = height;           // Set control height

                // Enable the control if the IsEnabled property is available
                // (If the property does not exist, this line will be ignored at runtime)
                try
                {
                    commandButton.IsEnabled = true; // BOOL not required for this property
                }
                catch (Exception)
                {
                    // Property not supported in this version; continue without setting it.
                }

                // Save the modified diagram to the specified output path in VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine($"Diagram saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Write any errors that occur during Aspose operations to the error stream
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
