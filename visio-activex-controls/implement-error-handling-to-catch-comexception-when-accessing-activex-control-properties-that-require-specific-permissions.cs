using System;
using System.IO;
using System.Runtime.InteropServices;
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
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file does not exist: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page (zero‑based index).
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip shapes that do not contain an ActiveX control.
                    if (shape.ActiveXControl == null)
                        continue;

                    // Retrieve the generic ActiveX control.
                    var activeX = shape.ActiveXControl;

                    // Example 1: Enable the control (IsEnabled is a bool, not Aspose BOOL).
                    try
                    {
                        activeX.IsEnabled = true;
                        Console.WriteLine($"Shape ID {shape.ID}: ActiveX control enabled.");
                    }
                    catch (COMException comEx)
                    {
                        // Permission‑related COM exception.
                        Console.Error.WriteLine($"COMException while setting IsEnabled on shape ID {shape.ID}: {comEx.Message}");
                    }
                    catch (Exception ex)
                    {
                        // Any other unexpected error.
                        Console.Error.WriteLine($"Error while setting IsEnabled on shape ID {shape.ID}: {ex.Message}");
                    }

                    // Example 2: Access control‑specific properties safely.
                    try
                    {
                        // Determine the concrete control type.
                        if (activeX.Type == ControlType.CheckBox)
                        {
                            // Cast to CheckBoxActiveXControl.
                            var checkBox = (CheckBoxActiveXControl)activeX;
                            // Set the checkbox state to Checked.
                            checkBox.Value = CheckValueType.Checked;
                            Console.WriteLine($"Shape ID {shape.ID}: CheckBox set to Checked.");
                        }
                        else if (activeX.Type == ControlType.CommandButton)
                        {
                            // Cast to CommandButtonActiveXControl.
                            var button = (CommandButtonActiveXControl)activeX;
                            // Update the button caption.
                            button.Caption = "Processed";
                            Console.WriteLine($"Shape ID {shape.ID}: CommandButton caption updated.");
                        }
                        else if (activeX.Type == ControlType.SpinButton)
                        {
                            // Cast to SpinButtonActiveXControl.
                            var spin = (SpinButtonActiveXControl)activeX;
                            // Set the spin button position.
                            spin.Position = 10;
                            Console.WriteLine($"Shape ID {shape.ID}: SpinButton position set to 10.");
                        }
                        // Additional control types can be handled here as needed.
                    }
                    catch (COMException comEx)
                    {
                        // Permission‑related COM exception for specific properties.
                        Console.Error.WriteLine($"COMException while accessing control‑specific property on shape ID {shape.ID}: {comEx.Message}");
                    }
                    catch (Exception ex)
                    {
                        // Any other unexpected error.
                        Console.Error.WriteLine($"Error while accessing control‑specific property on shape ID {shape.ID}: {ex.Message}");
                    }
                }
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // General error handling for diagram loading/saving.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}