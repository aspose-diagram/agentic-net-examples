using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.ActiveXControls;

class Program
{
    static void Main(string[] args)
    {
        // Path to the source Visio file
        string inputPath = "input.vsdx";
        // Verify the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Iterate over each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Iterate over each shape on the current page
                foreach (Shape shape in page.Shapes)
                {
                    // Process only shapes that contain an ActiveX control
                    if (shape.ActiveXControl != null)
                    {
                        // Output basic shape information (ID and universal name)
                        Console.WriteLine($"Shape ID: {shape.ID}, NameU: {shape.NameU}");

                        // Retrieve the control type of the ActiveX control
                        ControlType ctrlType = shape.ActiveXControl.Type;
                        Console.WriteLine($"ActiveX Control Type: {ctrlType}");

                        // Example handling based on specific control types
                        if (ctrlType == ControlType.CommandButton)
                        {
                            // Cast to the concrete command button control
                            CommandButtonActiveXControl btn = (CommandButtonActiveXControl)shape.ActiveXControl;
                            Console.WriteLine($"Button Caption: {btn.Caption}");
                        }
                        else if (ctrlType == ControlType.CheckBox)
                        {
                            // Cast to the concrete checkbox control
                            CheckBoxActiveXControl chk = (CheckBoxActiveXControl)shape.ActiveXControl;
                            string value = chk.Value == CheckValueType.Checked ? "Checked" : "Unchecked";
                            Console.WriteLine($"CheckBox Value: {value}");
                        }
                        // Additional control-specific processing can be added here
                    }
                }
            }

            // Save the diagram after processing (optional)
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}