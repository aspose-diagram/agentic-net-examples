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
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Only shapes that contain an ActiveX control have a non‑null ActiveXControl property
                    if (shape.ActiveXControl != null)
                    {
                        ActiveXControl control = shape.ActiveXControl;

                        // Attempt to cast the generic ActiveXControl to a specific type (ComboBoxActiveXControl)
                        try
                        {
                            ComboBoxActiveXControl comboBox = (ComboBoxActiveXControl)control;

                            // If the cast succeeds, you can work with ComboBox‑specific members
                            Console.WriteLine($"ComboBox control found on shape ID {shape.ID} (Page {page.ID}).");
                        }
                        catch (InvalidCastException)
                        {
                            // The control is not a ComboBox; handle the incompatibility gracefully
                            Console.WriteLine($"Shape ID {shape.ID} contains an ActiveX control of type {control.Type}, which cannot be cast to ComboBoxActiveXControl.");
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
