using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram (replace with your actual file path)
            Diagram diagram = new Diagram("input.vsdx");

            // Assume the pentagon is on the first page
            Page page = diagram.Pages[0];
            Shape pentagon = null;

            // Find the first non‑deleted shape whose master name is "Pentagon"
            foreach (Shape shape in page.Shapes)
            {
                if (shape.Master != null && shape.Master.Name == "Pentagon" && shape.Del == BOOL.False)
                {
                    pentagon = shape;
                    break;
                }
            }

            if (pentagon == null)
                throw new Exception("Pentagon shape not found in the diagram.");

            // Clear any existing text and add the annotation label
            pentagon.Text.Value.Clear();
            pentagon.Text.Value.Add(new Txt("Annotation"));

            // Center the text block inside the pentagon
            pentagon.TextXForm.TxtPinX.Value = 0.5; // 50% of shape width
            pentagon.TextXForm.TxtPinY.Value = 0.5; // 50% of shape height

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
