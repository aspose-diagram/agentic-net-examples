using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        // Define input file path
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load an existing Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find the CheckBox ActiveX control
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains an ActiveX control of type CheckBox
                    if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CheckBox)
                    {
                        // Cast to the specific CheckBox control type
                        CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;

                        // Disable user interaction by setting the value to unchecked (no Enabled property exists)
                        checkBox.Value = (CheckValueType)0; // 0 corresponds to unchecked state

                        // Optionally break after first match if only one checkbox is expected
                        // break;
                    }
                }
            }

            // Define output file path
            string outputPath = "output.vsdx";
            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any Aspose or I/O errors to the error console
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}