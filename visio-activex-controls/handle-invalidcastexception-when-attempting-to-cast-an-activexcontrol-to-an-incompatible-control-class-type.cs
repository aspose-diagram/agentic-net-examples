using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string inputPath = "sample.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Ensure the diagram has at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            // Work with the first page.
            Page page = diagram.Pages[0];

            // Insert a CommandButton ActiveX control onto the page.
            // Parameters: control type, x, y, width, height.
            page.AddActiveXControl(ControlType.CommandButton, 2, 2, 1.5, 0.5);

            // Retrieve the newly added shape (last shape in the collection).
            if (page.Shapes.Count == 0)
            {
                Console.Error.WriteLine("No shapes were added to the page.");
                return;
            }

            // Shapes indexer uses zero‑based int index.
            Shape addedShape = page.Shapes[page.Shapes.Count - 1];

            // Attempt to cast the ActiveX control to an incompatible type (ImageActiveXControl).
            try
            {
                // This cast is expected to fail because the control is a CommandButton.
                ImageActiveXControl imageCtrl = (ImageActiveXControl)addedShape.ActiveXControl;
                // If the cast unexpectedly succeeds, we can use the control (not expected).
                Console.WriteLine("Unexpectedly cast to ImageActiveXControl.");
            }
            catch (InvalidCastException icex)
            {
                // Handle the invalid cast gracefully.
                Console.WriteLine("InvalidCastException caught: attempted to cast to an incompatible ActiveX control type.");
                Console.WriteLine($"Details: {icex.Message}");
            }

            // Correctly cast to the actual control type and modify a property.
            if (addedShape.ActiveXControl.Type == ControlType.CommandButton)
            {
                CommandButtonActiveXControl cmdBtn = (CommandButtonActiveXControl)addedShape.ActiveXControl;
                // Set the caption of the command button.
                cmdBtn.Caption = "Click Me";
                Console.WriteLine("CommandButton caption set successfully.");
            }
            else
            {
                Console.WriteLine("The added shape does not contain a CommandButton ActiveX control.");
            }

            // Save the modified diagram to the output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine("An error occurred while processing the diagram:");
            Console.Error.WriteLine(ex.Message);
        }
    }
}