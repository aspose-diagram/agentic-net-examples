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

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // First, collect the IDs of all group shapes on the page
                List<long> groupIds = new List<long>();
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Type == TypeValue.Group)
                    {
                        groupIds.Add(shape.ID);
                    }
                }

                // Process each group shape
                foreach (long groupId in groupIds)
                {
                    // Retrieve the group shape by its ID
                    Shape groupShape = page.Shapes.GetShape(groupId);

                    // Store the IDs of the sub‑shapes contained in the group
                    List<long> subShapeIds = new List<long>();
                    foreach (Shape sub in groupShape.Shapes)
                    {
                        subShapeIds.Add(sub.ID);
                    }

                    // Expand (ungroup) the group shape to expose its members
                    groupShape.Ungroup();

                    // Apply individual rotation to each now‑exposed sub‑shape
                    foreach (long subId in subShapeIds)
                    {
                        Shape subShape = page.Shapes.GetShape(subId);
                        double angleDeg = 45.0; // example rotation angle in degrees
                        double angleRad = Math.PI * angleDeg / 180.0; // convert to radians
                        subShape.SetAngle(angleRad);
                    }
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
