using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths for input and output Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing diagram
                Diagram diagram = new Diagram(inputPath);

                // Name of the master to clone (must exist in the source diagram)
                string masterNameToClone = "Rectangle";

                // Retrieve the original master by name
                Master? originalMaster = diagram.Masters.GetMasterByName(masterNameToClone);
                if (originalMaster == null)
                {
                    throw new Exception($"Master '{masterNameToClone}' not found in the diagram.");
                }

                // Clone the master and give it a new unique name
                Master clonedMaster = (Master)originalMaster.Clone();
                clonedMaster.Name = originalMaster.Name + "_Clone";

                // Add the cloned master to the diagram's master collection
                diagram.Masters.Add(clonedMaster);

                // Modify the text block of the first shape inside the cloned master (if any)
                if (clonedMaster.Shapes.Count > 0)
                {
                    Shape masterShape = clonedMaster.Shapes[0];

                    // Example modifications to the text block
                    // Set left and right margins (0.1 inches)
                    masterShape.TextBlock.LeftMargin = new DoubleValue(0.1, MeasureConst.IN);
                    masterShape.TextBlock.RightMargin = new DoubleValue(0.1, MeasureConst.IN);

                    // Set vertical alignment to middle
                    masterShape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;

                    // Set a background color for the text block (yellow)
                    masterShape.TextBlock.TextBkgnd.Ufe.F = "RGB(255,255,0)";
                }

                // Define the IDs of shapes that should use the cloned master
                // In a real scenario these could be determined dynamically
                List<long> targetShapeIds = new List<long> { 5, 8, 12 };

                // Apply the cloned master to each selected shape
                Page page = diagram.Pages[0]; // Assuming shapes are on the first page
                foreach (long shapeId in targetShapeIds)
                {
                    Shape shape = page.Shapes.GetShape(shapeId);
                    if (shape != null)
                    {
                        shape.Master = clonedMaster;
                    }
                    else
                    {
                        Console.WriteLine($"Shape with ID {shapeId} not found on page '{page.Name}'.");
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram processing completed successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }