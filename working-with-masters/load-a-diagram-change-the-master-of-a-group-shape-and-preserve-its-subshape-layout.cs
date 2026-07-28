using System;
using System.Collections.Generic;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramGroupMasterChange
{
    // Helper class to store sub‑shape geometry
    class SubShapeInfo
    {
        public long Id { get; set; }
        public double PinX { get; set; }
        public double PinY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public double Angle { get; set; }
    }

    class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Access the first page (index 0)
                Page page = diagram.Pages[0];

                // Find the first group shape on the page
                Aspose.Diagram.Shape? groupShape = null;
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group)
                    {
                        groupShape = shape;
                        break;
                    }
                }

                if (groupShape == null)
                {
                    throw new Exception("No group shape found on the page.");
                }

                // Preserve layout of sub‑shapes
                List<SubShapeInfo> subShapeInfos = new List<SubShapeInfo>();
                foreach (Aspose.Diagram.Shape subShape in groupShape.Shapes)
                {
                    SubShapeInfo info = new SubShapeInfo
                    {
                        Id = subShape.ID,
                        PinX = subShape.XForm.PinX.Value,
                        PinY = subShape.XForm.PinY.Value,
                        Width = subShape.XForm.Width.Value,
                        Height = subShape.XForm.Height.Value,
                        Angle = subShape.XForm.Angle.Value
                    };
                    subShapeInfos.Add(info);
                }

                // Specify the new master name (must exist in the diagram's masters collection)
                string newMasterName = "NewGroupMaster";

                // Retrieve the new master
                Master? newMaster = diagram.Masters.GetMasterByName(newMasterName);
                if (newMaster == null)
                {
                    throw new Exception($"Master \"{newMasterName}\" not found in the diagram.");
                }

                // Change the master of the group shape
                groupShape.Master = newMaster;

                // Re‑apply the preserved sub‑shape geometry
                foreach (SubShapeInfo info in subShapeInfos)
                {
                    Aspose.Diagram.Shape? subShape = groupShape.Shapes.GetShape(info.Id);
                    if (subShape != null)
                    {
                        subShape.XForm.PinX.Value = info.PinX;
                        subShape.XForm.PinY.Value = info.PinY;
                        subShape.XForm.Width.Value = info.Width;
                        subShape.XForm.Height.Value = info.Height;
                        subShape.XForm.Angle.Value = info.Angle;
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
}