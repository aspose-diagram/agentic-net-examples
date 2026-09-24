using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Access the first page (adjust index if needed)
            Page page = diagram.Pages[0];

            // Iterate through all shapes on the page
            foreach (Shape shape in page.Shapes)
            {
                // Identify dynamic connector shapes by master name
                if (shape.Master != null && shape.Master.Name == "Dynamic connector")
                {
                    // Enable reroute option by setting ConFixedCode to Undefined (the only valid value)
                    shape.Layout.ConFixedCode.Value = ConFixedCodeValue.Undefined;
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
