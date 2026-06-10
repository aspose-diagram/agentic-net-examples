using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source diagram and the master (stencil) file
                string diagramPath = "sourceDiagram.vsdx";
                string masterFilePath = "customMaster.vssx";
                string masterName = "MyCustomShape"; // Name of the master inside the stencil

                // Load the existing diagram
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Ensure the diagram has at least five pages (page index 4)
                    while (diagram.Pages.Count < 5)
                    {
                        Page newPage = new Page();
                        newPage.Name = $"Page{diagram.Pages.Count + 1}";
                        diagram.Pages.Add(newPage);
                    }

                    // Retrieve page five (zero‑based index)
                    Page pageFive = diagram.Pages[4];

                    // Import the custom master from the stencil file
                    // This adds the master to the diagram's Masters collection
                    diagram.AddMaster(masterFilePath, masterName);

                    // Add a shape based on the imported master to page five
                    // PinX and PinY are set to arbitrary coordinates (e.g., 2.0, 2.0 inches)
                    long shapeId = pageFive.AddShape(2.0, 2.0, masterName);

                    // Retrieve the newly added shape using its ID
                    Shape shape = pageFive.Shapes.GetShape(shapeId);

                    // Set the text label of the shape
                    shape.Text.Value.Clear();                     // Remove any existing text runs
                    shape.Text.Value.Add(new Txt("Custom Label")); // Add the desired label

                    // Save the modified diagram
                    diagram.Save("outputDiagram.vsdx", SaveFileFormat.Vsdx);
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }