using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Assign a CSS class (as a custom property) to each shape
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked for deletion
                    if (shape.Del == BOOL.True)
                        continue;

                    // Look for an existing "Class" property
                    bool classFound = false;
                    foreach (Prop prop in shape.Props)
                    {
                        if (prop.Name == "Class")
                        {
                            prop.Value.Val = $"shape-{shape.ID}";
                            classFound = true;
                            break;
                        }
                    }

                    // If not found, create a new custom property named "Class"
                    if (!classFound)
                    {
                        Prop classProp = new Prop();
                        classProp.Name = "Class";
                        classProp.Value.Val = $"shape-{shape.ID}";
                        shape.Props.Add(classProp);
                    }
                }
            }

            // Configure SVG export options
            SVGSaveOptions svgOptions = new SVGSaveOptions
            {
                ExportHiddenPage = false,      // Do not export hidden pages
                ExportGuideShapes = false,     // Do not export guide shapes
                SVGFitToViewPort = true        // Fit the SVG to the viewport
            };

            // Path for the exported SVG file
            string outputPath = "output.svg";

            // Export the diagram to SVG
            diagram.Save(outputPath, svgOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
