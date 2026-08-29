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
                string sourcePath = "input.vsdx";

                // Path for the exported HTML file
                string htmlOutputPath = "exportedShape.html";

                // Load the diagram
                Diagram diagram = new Diagram(sourcePath);

                // Define the name of the shape to export (case‑sensitive)
                string targetShapeName = "MyShape";

                Shape targetShape = null;

                // Search for the shape by its universal name across all pages
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        if (shape.NameU == targetShapeName)
                        {
                            targetShape = shape;
                            break;
                        }
                    }

                    if (targetShape != null)
                        break;
                }

                // If the shape was not found, abort with an error
                if (targetShape == null)
                    throw new Exception($"Shape with NameU '{targetShapeName}' was not found.");

                // Configure HTML export options (optional settings can be added here)
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                htmlOptions.DefaultFont = "Arial";

                // Export the selected shape to HTML, preserving geometry and style
                targetShape.ToHTML(htmlOutputPath, htmlOptions);

                Console.WriteLine($"Shape '{targetShapeName}' exported successfully to '{htmlOutputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }