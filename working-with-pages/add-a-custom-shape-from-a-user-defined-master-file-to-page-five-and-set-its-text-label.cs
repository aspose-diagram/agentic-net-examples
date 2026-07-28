using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source diagram and the user‑defined master (stencil) file
            string diagramPath = "input.vsdx";
            string masterFilePath = "customMaster.vssx";
            string masterName = "MyCustomShape"; // name of the master inside the stencil

            // Load the existing Visio diagram
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Import the custom master from the stencil file into the diagram
                diagram.AddMaster(masterFilePath, masterName);

                // Ensure that page five (index 4) exists; add blank pages if necessary
                int targetPageIndex = 4; // zero‑based index for page five
                while (diagram.Pages.Count <= targetPageIndex)
                {
                    diagram.Pages.Add(new Page());
                }

                // Retrieve page five
                Page page = diagram.Pages[targetPageIndex];

                // Add the shape based on the imported master at the desired location
                double pinX = 2.0; // X coordinate in inches
                double pinY = 2.0; // Y coordinate in inches
                long shapeId = page.AddShape(pinX, pinY, masterName);

                // Get the concrete Shape object (cast ID to int as required by GetShape)
                Shape shape = page.Shapes.GetShape((int)shapeId);

                // Set the text label of the newly added shape
                shape.Text.Value.Clear();
                shape.Text.Value.Add(new Txt("Custom Shape Label"));

                // Save the modified diagram
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
