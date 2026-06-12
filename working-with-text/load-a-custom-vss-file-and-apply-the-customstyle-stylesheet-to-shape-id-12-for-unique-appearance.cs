using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source diagram, custom stencil (.vss) and output file.
                string diagramPath = "input.vsdx";
                string stencilPath = "customStencil.vss";
                string outputPath = "output.vsdx";

                // Load the main diagram.
                Diagram diagram = new Diagram(diagramPath);

                // Load the custom stencil file (.vss). This demonstrates loading a .vss file.
                // The stencil can be used later to add masters if needed.
                Diagram stencil = new Diagram(stencilPath);

                // Retrieve the shape with ID 12 from the first page.
                // Shape IDs are of type long.
                Shape targetShape = diagram.Pages[0].Shapes.GetShape(12L);
                if (targetShape == null)
                {
                    throw new Exception("Shape with ID 12 was not found in the diagram.");
                }

                // Locate the stylesheet named "CustomStyle" in the diagram's stylesheet collection.
                StyleSheet customStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "CustomStyle")
                    {
                        customStyle = ss;
                        break;
                    }
                }

                // If the stylesheet does not exist, create a simple one and add it to the diagram.
                if (customStyle == null)
                {
                    customStyle = new StyleSheet
                    {
                        Name = "CustomStyle",
                        ID = diagram.StyleSheets.Count + 1
                    };

                    // Example: set a red fill foreground and a thick black line.
                    customStyle.Fill.FillForegnd.Value = "#FF0000"; // Red fill.
                    customStyle.Line.LineColor.Value = "#000000"; // Black line.
                    customStyle.Line.LineWeight.Value = 0.05; // Thick line (in inches).

                    diagram.StyleSheets.Add(customStyle);
                }

                // Apply the stylesheet to the shape.
                // The style can be assigned to text, fill, and line formatting.
                targetShape.TextStyle = customStyle;
                targetShape.FillStyle = customStyle;
                targetShape.LineStyle = customStyle;

                // Save the modified diagram.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Custom style applied to shape ID 12 and diagram saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }