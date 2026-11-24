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

        // Guard: ensure the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram from the specified file.
            Diagram diagram = new Diagram(inputPath);

            bool checkBoxFound = false;

            // Iterate through all pages and shapes to locate CheckBox ActiveX controls.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that do not contain an ActiveX control.
                    if (shape.ActiveXControl == null)
                        continue;

                    // Identify CheckBox controls by their ControlType.
                    if (shape.ActiveXControl.Type == ControlType.CheckBox)
                    {
                        // Cast to the specific CheckBox control class.
                        CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;

                        // Disable user interaction. The IsEnabled property is a plain bool.
                        checkBox.IsEnabled = false;

                        Console.WriteLine($"Disabled CheckBox on shape ID {shape.ID} (Page {page.ID}).");
                        checkBoxFound = true;
                    }
                }
            }

            if (!checkBoxFound)
            {
                Console.WriteLine("No CheckBox ActiveX control found in the diagram.");
            }

            // Save the modified diagram to the output path in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}