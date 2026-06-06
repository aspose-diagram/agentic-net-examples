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

            // Load an existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through shapes on the first page (adjust as needed)
            foreach (Shape shape in diagram.Pages[0].Shapes)
            {
                // Verify the shape contains an ActiveX control
                if (shape.ActiveXControl != null)
                {
                    // Attempt to cast to a specific ActiveX control type
                    try
                    {
                        // This cast will throw InvalidCastException if the control is not a ComboBox
                        ComboBoxActiveXControl comboBox = (ComboBoxActiveXControl)shape.ActiveXControl;

                        // Example usage of the successfully cast control
                        Console.WriteLine($"Shape ID {shape.ID}: ComboBox width = {comboBox.Width}");
                    }
                    catch (InvalidCastException)
                    {
                        // Handle the incompatible cast gracefully
                        Console.WriteLine($"Shape ID {shape.ID}: Not a ComboBoxActiveXControl (actual type: {shape.ActiveXControl.GetType().Name})");

                        // Optional: try a safe cast to another control type using 'as'
                        CheckBoxActiveXControl checkBox = shape.ActiveXControl as CheckBoxActiveXControl;
                        if (checkBox != null)
                        {
                            Console.WriteLine($"Shape ID {shape.ID}: Found CheckBoxActiveXControl, IsEnabled = {checkBox.IsEnabled}");
                        }
                    }
                }
            }

            // Save the diagram (if any modifications were made)
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
