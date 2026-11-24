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
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find ActiveX controls.
            foreach (Page page in diagram.Pages)
            {
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Only process shapes that contain an ActiveX control.
                    if (shape.ActiveXControl == null)
                        continue;

                    // Retrieve the base control.
                    var baseControl = shape.ActiveXControl;

                    // Handle CommandButton controls.
                    if (baseControl.Type == ControlType.CommandButton)
                    {
                        var button = (CommandButtonActiveXControl)baseControl;

                        // Attempt to modify the Caption property safely.
                        TrySetProperty(
                            () => button.Caption = "New Caption",
                            "Caption",
                            button);

                        // Attempt to modify Width and Height (read‑write properties).
                        TrySetProperty(
                            () => button.Width = 2.0,
                            "Width",
                            button);
                        TrySetProperty(
                            () => button.Height = 0.5,
                            "Height",
                            button);
                    }
                    // Handle Image controls.
                    else if (baseControl.Type == ControlType.Image)
                    {
                        var imageCtrl = (ImageActiveXControl)baseControl;

                        // Attempt to set the Picture property (may be read‑only in some contexts).
                        TrySetProperty(
                            () => imageCtrl.Picture = File.ReadAllBytes("placeholder.png"),
                            "Picture",
                            imageCtrl);
                    }
                    // Add handling for other control types as needed.
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Log any unexpected errors.
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Attempts to set a property on an ActiveX control.
    /// If the property is read‑only, catches the exception and logs a warning.
    /// </summary>
    /// <param name="setAction">Delegate that performs the property assignment.</param>
    /// <param name="propertyName">Name of the property being set (for logging).</param>
    /// <param name="control">The ActiveX control instance.</param>
    private static void TrySetProperty(Action setAction, string propertyName, object control)
    {
        try
        {
            setAction();
            Console.WriteLine($"Property '{propertyName}' set successfully on {control.GetType().Name}.");
        }
        catch (Exception ex)
        {
            // Log a warning instead of propagating the exception.
            Console.Error.WriteLine(
                $"Warning: Unable to modify read‑only property '{propertyName}' on {control.GetType().Name}. {ex.Message}");
        }
    }
}