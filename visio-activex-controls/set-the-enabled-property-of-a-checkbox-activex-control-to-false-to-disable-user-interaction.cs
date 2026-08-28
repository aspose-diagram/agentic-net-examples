using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            string inputPath = "input.vsdx";
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find a CheckBox ActiveX control
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the shape contains an ActiveX control
                    if (shape.ActiveXControl != null && shape.ActiveXControl.Type == ControlType.CheckBox)
                    {
                        // Cast to the specific CheckBox control type
                        CheckBoxActiveXControl checkBox = (CheckBoxActiveXControl)shape.ActiveXControl;

                        // Disable user interaction by setting the control to not enabled
                        checkBox.IsEnabled = false;

                        // Optionally, also uncheck the box (if desired)
                        // checkBox.Value = (CheckValueType)0; // Unchecked state

                        // Since we found and modified the control, we can exit the loops
                        break;
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
