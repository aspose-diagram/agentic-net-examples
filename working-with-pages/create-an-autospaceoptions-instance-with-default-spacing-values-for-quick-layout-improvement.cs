using System;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main()
        {
            try
            {

                // Create AutoSpaceOptions with default spacing values (in inches)
                AutoSpaceOptions autoSpaceOptions = new AutoSpaceOptions();
                autoSpaceOptions.DistanceInHorizontal = 0.5; // default horizontal distance
                autoSpaceOptions.DistanceInVertical = 0.5;   // default vertical distance

                // Load an existing diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Apply auto-spacing to the first page of the diagram
                Page page = diagram.Pages[0];
                page.AutoSpaceShapes(page.Shapes, autoSpaceOptions);

                // Save the updated diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }