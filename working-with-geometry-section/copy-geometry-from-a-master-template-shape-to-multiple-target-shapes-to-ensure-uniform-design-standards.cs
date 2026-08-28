using System;
using System.IO;
using Aspose.Diagram;

class CopyGeometryExample
{
    static void Main()
    {
        try
        {

            // Load the existing Visio diagram (replace with your file path)
            Diagram diagram = new Diagram("InputDiagram.vsdx");

            // -----------------------------------------------------------------
            // 1. Retrieve the master that contains the template shape.
            //    Assume the master is identified by its universal name "TemplateMaster".
            // -----------------------------------------------------------------
            Master templateMaster = null;
            foreach (Master m in diagram.Masters)
            {
                if (string.Equals(m.NameU, "TemplateMaster", StringComparison.OrdinalIgnoreCase))
                {
                    templateMaster = m;
                    break;
                }
            }

            if (templateMaster == null)
            {
                Console.WriteLine("Template master not found.");
                return;
            }

            // -----------------------------------------------------------------
            // 2. Get the source shape from the master.
            //    Here we take the first shape inside the master as the template.
            // -----------------------------------------------------------------
            if (templateMaster.Shapes.Count == 0)
            {
                Console.WriteLine("No shapes found in the template master.");
                return;
            }

            Shape sourceShape = templateMaster.Shapes[0];

            // -----------------------------------------------------------------
            // 3. Identify target shapes that need to receive the geometry.
            //    For demonstration, we select shapes on the first page whose
            //    NameU starts with "TargetShape".
            // -----------------------------------------------------------------
            Page firstPage = diagram.Pages[0];
            foreach (Shape targetShape in firstPage.Shapes)
            {
                if (!string.IsNullOrEmpty(targetShape.NameU) &&
                    targetShape.NameU.StartsWith("TargetShape", StringComparison.OrdinalIgnoreCase))
                {
                    // -----------------------------------------------------------------
                    // 4. Copy geometry (and related properties) from the source shape.
                    //    The Copy method copies all shape data, including Geoms.
                    // -----------------------------------------------------------------
                    targetShape.Copy(sourceShape);
                }
            }

            // -----------------------------------------------------------------
            // 5. Save the modified diagram (replace with your desired output path).
            // -----------------------------------------------------------------
            diagram.Save("OutputDiagram.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
