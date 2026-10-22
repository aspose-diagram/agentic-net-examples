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
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure the input Visio file exists.
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
                // Shapes collection is zero‑based; iterate by index.
                for (int i = 0; i < page.Shapes.Count; i++)
                {
                    Shape shape = page.Shapes[i];

                    // Skip shapes that do not contain an ActiveX control.
                    if (shape.ActiveXControl == null)
                        continue;

                    // Identify CheckBox controls via the ControlType enumeration.
                    if (shape.ActiveXControl.Type == ControlType.CheckBox)
                    {
                        // Cast to the specific CheckBoxActiveXControl class.
                        CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;

                        // Disable user interaction. The IsEnabled property is a plain bool,
                        // so assign the C# literal false (not BOOL.False).
                        checkBox.IsEnabled = false;

                        Console.WriteLine($"Disabled CheckBox on page {page.ID}, shape index {i}.");
                        checkBoxFound = true;

                        // Break out of the inner loop if only the first checkbox should be disabled.
                        // Remove the break statements to disable all checkboxes.
                        break;
                    }
                }

                if (checkBoxFound)
                    break;
            }

            if (!checkBoxFound)
            {
                Console.WriteLine("No CheckBox ActiveX control found in the diagram.");
            }

            // Save the modified diagram in VSDX format.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}