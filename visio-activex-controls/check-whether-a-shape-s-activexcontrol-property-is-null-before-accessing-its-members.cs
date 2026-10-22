using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 1)
        {
            Console.Error.WriteLine("Usage: Program <inputDiagramPath> [outputDiagramPath]");
            return;
        }

        string inputPath = args[0];
        string outputPath = args.Length >= 2 ? args[1] : "output.vsdx";

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

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Check whether the shape contains an ActiveX control.
                    if (shape.ActiveXControl != null)
                    {
                        // Retrieve the base ActiveX control.
                        var activeX = shape.ActiveXControl;

                        // Output basic information about the control.
                        Console.WriteLine($"Shape ID {shape.ID} has ActiveX control of type {activeX.Type}.");

                        // Example: if the control is a CommandButton, cast and read its Caption.
                        if (activeX.Type == ControlType.CommandButton)
                        {
                            // Safe cast because we verified the type.
                            CommandButtonActiveXControl button = (CommandButtonActiveXControl)activeX;
                            Console.WriteLine($"  Caption: {button.Caption}");
                        }
                        // Example: if the control is a CheckBox, cast and read its Value.
                        else if (activeX.Type == ControlType.CheckBox)
                        {
                            CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)activeX;
                            // The Value property uses the CheckValueType enum.
                            Console.WriteLine($"  Checked: {checkBox.Value == CheckValueType.Checked}");
                        }
                        // Add handling for other control types as needed.
                    }
                    else
                    {
                        // Shape does not have an ActiveX control.
                        Console.WriteLine($"Shape ID {shape.ID} has no ActiveX control.");
                    }
                }
            }

            // Save the (potentially unchanged) diagram to the output path.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}