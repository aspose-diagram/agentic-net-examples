using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Validate arguments
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <input.vsdx> <output.vsdx>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: ensure input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram inside a using block as required
            using (Diagram diagram = new Diagram(inputPath))
            {
                // ------------------------------------------------------------
                // Create a custom StyleSheet that defines the desired font and background color
                // ------------------------------------------------------------
                StyleSheet customStyle = new StyleSheet();

                // Assign a unique ID and a name for the style sheet
                customStyle.ID = diagram.StyleSheets.Count + 1;
                customStyle.Name = "CustomCommentStyle";

                // Define character (font) formatting
                Aspose.Diagram.Char charFormat = new Aspose.Diagram.Char();
                charFormat.FontName.Value = "Calibri";               // Desired font
                charFormat.Style.Value = StyleValue.Bold;            // Example: bold text
                customStyle.Chars.Add(charFormat);

                // Define fill (background) formatting
                customStyle.Fill.FillForegnd.Value = "#FFCC00";       // Desired background color (orange)
                customStyle.Fill.FillPattern.Value = 1;              // Solid fill pattern

                // Add the custom style sheet to the diagram
                diagram.StyleSheets.Add(customStyle);

                // ------------------------------------------------------------
                // Apply the custom style to selected comments (annotations)
                // For demonstration, apply to all comments on the first page
                // ------------------------------------------------------------
                if (diagram.Pages.Count > 0)
                {
                    Page firstPage = diagram.Pages[0];

                    // Iterate through all annotations (comments) on the page
                    foreach (Annotation annotation in firstPage.PageSheet.Annotations)
                    {
                        // Only process annotations that are linked to a shape
                        if (annotation.ShapeID > 0)
                        {
                            // Retrieve the associated shape by its ID
                            Shape shape = firstPage.Shapes.GetShape(annotation.ShapeID);
                            if (shape != null)
                            {
                                // Apply the custom style to the shape's text and fill
                                shape.TextStyle = customStyle;
                                shape.FillStyle = customStyle;

                                // Optionally, update the comment text to indicate styling was applied
                                annotation.Comment.Value = "[Styled] " + annotation.Comment.Value;
                            }
                        }
                    }
                }

                // ------------------------------------------------------------
                // Save the modified diagram to the specified output path
                // ------------------------------------------------------------
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved successfully to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // Log any errors that occur during processing
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}