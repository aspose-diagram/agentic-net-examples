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
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Ensure there are at least two pages (source and target)
                if (diagram.Pages.Count < 2)
                {
                    // Add a new blank page as the target page
                    diagram.Pages.Add(new Page());
                }

                // Source page (first page) and target page (second page)
                Page sourcePage = diagram.Pages[0];
                Page targetPage = diagram.Pages[1];

                // Get the first shape on the source page to clone
                // (In a real scenario, you would locate the shape by ID, name, etc.)
                Shape originalShape = sourcePage.Shapes.GetShape(1); // assumes shape with ID 1 exists

                // Retrieve geometry and master information from the original shape
                double pinX = originalShape.XForm.PinX.Value;
                double pinY = originalShape.XForm.PinY.Value;
                double width = originalShape.XForm.Width.Value;
                double height = originalShape.XForm.Height.Value;
                string masterName = originalShape.Master != null ? originalShape.Master.Name : "Rectangle";

                // Add a cloned shape on the target page using the same master and geometry
                // The fourth parameter 'isCalculate' must be a boolean
                long clonedShapeId = targetPage.AddShape(pinX, pinY, width, height, masterName, false);
                Shape clonedShape = targetPage.Shapes.GetShape(clonedShapeId);

                // Modify the width of the cloned shape (e.g., increase by 50%)
                double newWidth = width * 1.5;
                clonedShape.XForm.Width.Value = newWidth;

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }