using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect two arguments: path to the diagram file and path to the custom .vss stencil file.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <DiagramPath> <StencilPath>");
            return;
        }

        string diagramPath = args[0];
        string stencilPath = args[1];

        // Guard against missing files.
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        if (!File.Exists(stencilPath))
        {
            Console.Error.WriteLine($"Stencil file not found: {stencilPath}");
            return;
        }

        try
        {
            // Load the main diagram.
            Diagram diagram = new Diagram(diagramPath);

            // Load the stencil (VSS) file.
            Diagram stencil = new Diagram(stencilPath, LoadFileFormat.Vss);

            // Find the stylesheet named "CustomStyle" in the stencil.
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
                Console.Error.WriteLine("Stylesheet 'CustomStyle' not found in the stencil file.");
                return;
            }

            // Add the found stylesheet to the main diagram's stylesheet collection.
            // Ensure the ID is unique within the diagram.
            customStyle.ID = diagram.StyleSheets.Count + 1;
            diagram.StyleSheets.Add(customStyle);

            // Retrieve shape with ID 12 from the first page.
            if (diagram.Pages.Count == 0)
            {
                Console.Error.WriteLine("The diagram contains no pages.");
                return;
            }

            Page page = diagram.Pages[0];
            Shape shape = page.Shapes.GetShape(12);
            if (shape == null)
            {
                Console.Error.WriteLine("Shape with ID 12 not found on the first page.");
                return;
            }

            // Apply the custom stylesheet to the shape's text, fill, and line styles.
            shape.TextStyle = customStyle;
            shape.FillStyle = customStyle;
            shape.LineStyle = customStyle;

            // Save the modified diagram.
            string outputPath = Path.Combine(Path.GetDirectoryName(diagramPath) ?? "", "output.vsdx");
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Capture any Aspose.Diagram related errors.
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}