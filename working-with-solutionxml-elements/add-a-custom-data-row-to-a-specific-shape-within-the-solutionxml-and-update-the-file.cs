using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (SolutionXML)
            Diagram diagram = new Diagram("SolutionXML.vsdx");

            // Identify the target page (e.g., first page)
            Page targetPage = diagram.Pages[0];

            // Locate the specific shape by its name (replace "MyShapeName" with the actual name)
            Shape targetShape = null;
            foreach (Shape shape in targetPage.Shapes)
            {
                if (shape.NameU == "MyShapeName")
                {
                    targetShape = shape;
                    break;
                }
            }

            if (targetShape == null)
            {
                Console.WriteLine("Shape not found.");
                return;
            }

            // Add custom data to the shape.
            // Visio provides three generic data fields: Data1, Data2, Data3.
            // Here we use Data1 to store the custom value.
            targetShape.Data1 = "CustomRowValue";

            // Refresh the shape to ensure the changes are applied.
            targetShape.RefreshData();

            // Save the updated diagram back to file.
            diagram.Save("SolutionXML_Updated.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
