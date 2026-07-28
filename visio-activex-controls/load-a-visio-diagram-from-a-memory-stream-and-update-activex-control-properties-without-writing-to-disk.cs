using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Obtain the Visio file bytes from any source (e.g., database, network).
        byte[] visioData = GetVisioFileBytes();

        // Guard against missing or empty input data.
        if (visioData == null || visioData.Length == 0)
        {
            Console.Error.WriteLine("Visio data is empty or null.");
            return;
        }

        // Load the diagram from a memory stream.
        using (MemoryStream inputStream = new MemoryStream(visioData))
        {
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputStream);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to load diagram: {ex.Message}");
                return;
            }

            // Ensure the diagram contains at least one page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("Diagram contains no pages.");
                return;
            }

            // Work with the first page of the diagram.
            Page page = diagram.Pages[0];

            // Add a CommandButton ActiveX control to the page.
            long controlShapeId = page.AddActiveXControl(
                ControlType.CommandButton, // type of control
                2.0,   // PinX (in inches)
                2.0,   // PinY (in inches)
                1.5,   // Width (in inches)
                0.5);  // Height (in inches)

            // Retrieve the shape that represents the ActiveX control.
            Shape controlShape = page.Shapes.GetShape(controlShapeId);

            // Cast the generic ActiveXControl to the specific CommandButton type.
            CommandButtonActiveXControl cmdButton = (CommandButtonActiveXControl)controlShape.ActiveXControl;

            // Update properties of the command button.
            cmdButton.Caption = "Click Me";

            // Optionally reposition the control on the page.
            controlShape.XForm.PinX.Value = 3.0;
            controlShape.XForm.PinY.Value = 3.0;

            // Save the modified diagram back to a memory stream (no disk I/O).
            using (MemoryStream outputStream = new MemoryStream())
            {
                try
                {
                    diagram.Save(outputStream, SaveFileFormat.Vsdx);
                    Console.WriteLine($"Updated diagram size: {outputStream.Length} bytes");
                    // The outputStream now contains the updated Visio file.
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine($"Failed to save diagram: {ex.Message}");
                }
            }
        }
    }

    // Placeholder method – replace with actual logic to retrieve Visio file bytes.
    static byte[] GetVisioFileBytes()
    {
        // Example: return an empty array for demonstration purposes.
        return new byte[0];
    }
}