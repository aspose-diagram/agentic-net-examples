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

            // Path to the original Visio file
            string filePath = "input.vsdx";

            // Load the diagram from the file
            Diagram diagram = new Diagram(filePath);

            // Example field operation: update the first field of the first shape that contains a field
            if (diagram.Pages.Count > 0)
            {
                Page page = diagram.Pages[0];
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Fields.Count > 0)
                    {
                        // Access the first field
                        Field field = shape.Fields[0];

                        // Update the field's value
                        field.Value.Val = "Updated value";

                        // Clear any existing format strings (optional)
                        field.Format.Val = "";
                        field.Format.Ufev.F = "";
                        field.Format.Ufev.Unit = MeasureConst.Undefined;

                        // Exit after updating one field
                        break;
                    }
                }
            }

            // Save the modified diagram back to its original location
            diagram.Save(filePath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
