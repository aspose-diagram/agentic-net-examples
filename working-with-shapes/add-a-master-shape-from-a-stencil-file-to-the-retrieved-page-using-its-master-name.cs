using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source diagram, stencil file and the output diagram
            string diagramPath = "input.vsdx";
            string stencilPath = "shapes.vssx";
            string masterName = "Rectangle"; // name of the master shape in the stencil
            string outputPath = "output.vsdx";

            // Load an existing Visio diagram
            Diagram diagram = new Diagram(diagramPath);

            // Import the master shape from the stencil file into the diagram
            diagram.AddMaster(stencilPath, masterName);

            // Retrieve the first page of the diagram
            Page page = diagram.Pages[0];

            // Add a shape based on the imported master to the page
            // Parameters: pinX, pinY (position), master name, isCalculate flag
            long shapeId = page.AddShape(2.0, 2.0, masterName, false);

            // Optionally retrieve the shape object for further modifications
            Shape shape = page.Shapes.GetShape(shapeId);
            shape.Text.Value.Clear();
            shape.Text.Value.Add(new Txt("Added via master"));

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
