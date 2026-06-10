using System.IO;
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

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Name of the master that will replace the current group master
            string newMasterName = "NewGroupMaster";

            // Load the diagram from file
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Access the first page (adjust if needed)
                Page page = diagram.Pages[0];

                // Locate a group shape on the page
                Shape? groupShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group)
                    {
                        groupShape = shape;
                        break;
                    }
                }

                if (groupShape == null)
                {
                    Console.WriteLine("No group shape found on the page.");
                    return;
                }

                // Preserve the layout of all sub‑shapes inside the group
                var layoutMap = new Dictionary<long, (double pinX, double pinY, double width, double height, double angle)>();
                foreach (Shape subShape in groupShape.Shapes)
                {
                    layoutMap[subShape.ID] = (
                        subShape.XForm.PinX.Value,
                        subShape.XForm.PinY.Value,
                        subShape.XForm.Width.Value,
                        subShape.XForm.Height.Value,
                        subShape.XForm.Angle.Value
                    );
                }

                // Verify that the target master exists in the diagram
                if (!diagram.Masters.IsExist(newMasterName))
                {
                    Console.WriteLine($"Master \"{newMasterName}\" does not exist in the diagram.");
                    return;
                }

                // Change the master of the group shape
                Master newMaster = diagram.Masters.GetMasterByName(newMasterName);
                groupShape.Master = newMaster;

                // Re‑apply the preserved layout to each sub‑shape
                foreach (Shape subShape in groupShape.Shapes)
                {
                    if (layoutMap.TryGetValue(subShape.ID, out var vals))
                    {
                        subShape.XForm.PinX.Value = vals.pinX;
                        subShape.XForm.PinY.Value = vals.pinY;
                        subShape.XForm.Width.Value = vals.width;
                        subShape.XForm.Height.Value = vals.height;
                        subShape.XForm.Angle.Value = vals.angle;
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }

            Console.WriteLine("Diagram saved successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
