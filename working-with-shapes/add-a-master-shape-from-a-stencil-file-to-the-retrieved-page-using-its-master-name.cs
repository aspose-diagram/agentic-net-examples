using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load an existing Visio diagram (or create a new one)
                // Replace with your actual diagram file path
                string diagramPath = "input.vsdx";
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Import a master shape from a stencil file.
                    // Replace with the actual stencil file path and master name you need.
                    string stencilPath = "stencil.vssx";
                    string masterName = "Rectangle"; // Example master name present in the stencil
                    diagram.AddMaster(stencilPath, masterName);

                    // Retrieve the first page of the diagram.
                    // You can also use diagram.Pages.GetPage("Page-1") to fetch by name.
                    Page page = diagram.Pages[0];

                    // Add a shape based on the imported master to the page.
                    // PinX and PinY are the coordinates (in inches) where the shape will be placed.
                    double pinX = 2.0; // X coordinate
                    double pinY = 2.0; // Y coordinate
                    long shapeId = page.AddShape(pinX, pinY, masterName);

                    // Optionally retrieve the created shape to modify its properties.
                    Shape shape = page.Shapes.GetShape(shapeId);
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt("Added via master"));

                    // Save the modified diagram.
                    // Replace with your desired output path.
                    string outputPath = "output.vsdx";
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Master shape added and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }