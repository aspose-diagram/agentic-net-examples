using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Define input and output file paths.
        string diagramPath = "input.vsdx";
        if (!System.IO.File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"File not found: {diagramPath}");
            return;
        }
        string stencilPath = "custom.vss";
        if (!System.IO.File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"File not found: {stencilPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard: ensure the diagram file exists.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Guard: ensure the stencil file exists.
        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        try
        {
            // Load the main Visio diagram.
            Diagram diagram = new Diagram(diagramPath, LoadFileFormat.Vsdx);

            // Load the custom stencil (.vss) which contains the stylesheet.
            Diagram stencil = new Diagram(stencilPath, LoadFileFormat.Vss);

            // Locate the stylesheet named "CustomStyle" in the stencil.
            StyleSheet customStyle = null;
            foreach (StyleSheet ss in stencil.StyleSheets)
            {
                if (ss.Name == "CustomStyle")
                {
                    customStyle = ss;
                    break;
                }
            }

            if (customStyle == null)
            {
                Console.Error.WriteLine("Stylesheet 'CustomStyle' not found in the stencil.");
                return;
            }

            // Add the stylesheet to the main diagram if it does not already exist.
            bool styleExists = false;
            foreach (StyleSheet ss in diagram.StyleSheets)
            {
                if (ss.Name == customStyle.Name)
                {
                    styleExists = true;
                    break;
                }
            }
            if (!styleExists)
            {
                diagram.StyleSheets.Add(customStyle);
            }

            // Retrieve the first page (assuming the shape is on page 0).
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }
            Page page = diagram.Pages[0];

            // Retrieve shape with ID 12.
            long targetShapeId = 12L;
            Shape shape = page.Shapes.GetShape(targetShapeId);
            if (shape == null)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} not found.");
                return;
            }

            // Ensure the shape is not logically deleted.
            if (shape.Del == BOOL.True)
            {
                Console.Error.WriteLine($"Shape with ID {targetShapeId} is marked as deleted.");
                return;
            }

            // Apply the custom stylesheet to the shape's text, fill, and line styles.
            shape.TextStyle = customStyle;
            shape.FillStyle = customStyle;
            shape.LineStyle = customStyle;

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or IO errors to the error console.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}