using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the custom stencil file (.vss)
                string stencilPath = "customStencil.vss";

                // Load the stencil as a Diagram object
                Diagram diagram = new Diagram(stencilPath);

                // Retrieve the shape with ID 12 from the first page of the stencil
                // Ensure the page exists and the shape ID is valid
                if (diagram.Pages.Count == 0)
                {
                    throw new Exception("The loaded stencil does not contain any pages.");
                }

                Page page = diagram.Pages[0];
                Shape shape = page.Shapes.GetShape(12);
                if (shape == null)
                {
                    throw new Exception("Shape with ID 12 was not found in the stencil.");
                }

                // Locate the stylesheet named "CustomStyle"
                StyleSheet customStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "CustomStyle")
                    {
                        customStyle = ss;
                        break;
                    }
                }

                if (customStyle == null)
                {
                    throw new Exception("Stylesheet 'CustomStyle' was not found in the stencil.");
                }

                // Apply the stylesheet to the shape.
                // Assign the stylesheet to text, fill, and line style collections.
                shape.TextStyle = customStyle;
                shape.FillStyle = customStyle;
                shape.LineStyle = customStyle;

                // Save the modified stencil (or export to a Visio drawing) to verify the changes.
                // Here we save as a VSDX file.
                string outputPath = "StyledStencil.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Stylesheet 'CustomStyle' applied to shape ID 12 and saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }