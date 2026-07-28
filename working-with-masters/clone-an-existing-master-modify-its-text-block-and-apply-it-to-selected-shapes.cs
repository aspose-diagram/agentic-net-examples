using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing diagram
                Diagram diagram = new Diagram(inputPath);

                // -----------------------------------------------------------------
                // 1. Retrieve the master to be cloned (e.g., a rectangle master)
                // -----------------------------------------------------------------
                string masterToCloneName = "Rectangle";
                Master originalMaster = diagram.Masters.GetMasterByName(masterToCloneName);
                if (originalMaster == null)
                {
                    throw new Exception($"Master '{masterToCloneName}' not found in the diagram.");
                }

                // -----------------------------------------------------------------
                // 2. Clone the master and give it a new unique name
                // -----------------------------------------------------------------
                Master clonedMaster = (Master)originalMaster.Clone();
                clonedMaster.Name = "Rectangle_Clone";
                clonedMaster.NameU = "Rectangle_Clone";

                // Add the cloned master to the diagram's master collection
                diagram.Masters.Add(clonedMaster);

                // -----------------------------------------------------------------
                // 3. Modify the text block of the cloned master
                //    (Assuming the master contains at least one shape)
                // -----------------------------------------------------------------
                if (clonedMaster.Shapes.Count > 0)
                {
                    Shape masterShape = clonedMaster.Shapes[0];

                    // Clear any existing text
                    masterShape.Text.Value.Clear();

                    // Add new text to the master shape
                    masterShape.Text.Value.Add(new Txt("Cloned Master Text"));

                    // Example: set text direction to horizontal
                    masterShape.TextBlock.TextDirection.Value = TextDirectionValue.Horizontal;

                    // Example: set margins (4 points each)
                    double marginPoints = 4.0;
                    masterShape.TextBlock.LeftMargin.Value = marginPoints;
                    masterShape.TextBlock.RightMargin.Value = marginPoints;
                    masterShape.TextBlock.TopMargin.Value = marginPoints;
                    masterShape.TextBlock.BottomMargin.Value = marginPoints;
                }
                else
                {
                    throw new Exception("Cloned master does not contain any shapes to modify.");
                }

                // -----------------------------------------------------------------
                // 4. Apply the cloned master to selected shapes on the first page
                //    (Here we select shapes whose universal name is "TargetShape")
                // -----------------------------------------------------------------
                Page page = diagram.Pages[0];
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.NameU == "TargetShape")
                    {
                        // Assign the cloned master to the shape
                        shape.Master = clonedMaster;
                    }
                }

                // -----------------------------------------------------------------
                // 5. Save the modified diagram
                // -----------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }