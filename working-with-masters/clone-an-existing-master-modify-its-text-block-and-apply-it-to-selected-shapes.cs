using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – adjust as needed
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Name of the master to clone
                string masterToCloneName = "Rectangle";

                // Load the existing diagram
                Diagram diagram = new Diagram(inputPath);

                // Retrieve the original master by name
                Master? originalMaster = diagram.Masters.GetMasterByName(masterToCloneName);
                if (originalMaster == null)
                {
                    throw new Exception($"Master '{masterToCloneName}' not found in the diagram.");
                }

                // Clone the master (deep copy)
                Master clonedMaster = (Master)originalMaster.Clone();

                // Give the cloned master a unique name
                clonedMaster.Name = masterToCloneName + "_Cloned";

                // Add the cloned master to the diagram's master collection
                diagram.Masters.Add(clonedMaster);

                // Modify the TextBlock of each shape inside the cloned master
                foreach (Shape masterShape in clonedMaster.Shapes)
                {
                    // Example modifications: set margins, text direction, vertical alignment
                    masterShape.TextBlock.LeftMargin = new DoubleValue(4, MeasureConst.PT);
                    masterShape.TextBlock.RightMargin = new DoubleValue(4, MeasureConst.PT);
                    masterShape.TextBlock.TopMargin = new DoubleValue(2, MeasureConst.PT);
                    masterShape.TextBlock.BottomMargin = new DoubleValue(2, MeasureConst.PT);

                    masterShape.TextBlock.TextDirection.Value = TextDirectionValue.Horizontal;
                    masterShape.TextBlock.VerticalAlign.Value = VerticalAlignValue.Middle;
                }

                // Apply the cloned master to selected shapes.
                // In this example, we replace all shapes that originally used the master we cloned.
                Page firstPage = diagram.Pages[0];
                foreach (Shape shape in firstPage.Shapes)
                {
                    if (shape.Master != null && shape.Master.Name == masterToCloneName)
                    {
                        // Assign the cloned master to the shape
                        shape.Master = clonedMaster;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }