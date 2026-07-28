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

            // Iterate through all pages and shapes to find ActiveX controls
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Only shapes that contain an ActiveX control have this property set
                    if (shape.ActiveXControl != null)
                    {
                        // Attempt to cast the generic ActiveXControl to a specific type,
                        // e.g., ComboBoxActiveXControl. This may throw InvalidCastException
                        // if the underlying control is of a different type.
                        try
                        {
                            ComboBoxActiveXControl comboBox = (ComboBoxActiveXControl)shape.ActiveXControl;

                            // Safe to use the specific control now
                            Console.WriteLine($"ComboBox control found on shape ID {shape.ID}");
                            // Example: read a property specific to ComboBoxActiveXControl
                            // Console.WriteLine($"ComboBox items count: {comboBox.Items.Count}");
                        }
                        catch (InvalidCastException)
                        {
                            // Handle the case where the control is not a ComboBox.
                            // You can log, ignore, or attempt a different cast here.
                            Console.WriteLine(
                                $"Shape ID {shape.ID} does not contain a ComboBox control. " +
                                $"Actual control type: {shape.ActiveXControl.GetType().Name}");
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
