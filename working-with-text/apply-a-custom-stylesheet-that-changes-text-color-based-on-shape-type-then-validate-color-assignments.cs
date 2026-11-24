using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Input and output file paths
        string inputPath = "input.vsdx";
        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }
        string outputPath = "output.vsdx";

        // Guard clauses for file existence
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Create style sheets for different shape types
            // Rectangle style (red text)
            StyleSheet rectStyle = new StyleSheet();
            rectStyle.ID = diagram.StyleSheets.Count + 1;
            Aspose.Diagram.Char rectChar = new Aspose.Diagram.Char();
            rectChar.IX = 0; // first character style
            rectChar.Color.Value = "#FF0000"; // red
            rectStyle.Chars.Add(rectChar);
            diagram.StyleSheets.Add(rectStyle);

            // Ellipse style (green text)
            StyleSheet ellipseStyle = new StyleSheet();
            ellipseStyle.ID = diagram.StyleSheets.Count + 1;
            Aspose.Diagram.Char ellipseChar = new Aspose.Diagram.Char();
            ellipseChar.IX = 0;
            ellipseChar.Color.Value = "#00FF00"; // green
            ellipseStyle.Chars.Add(ellipseChar);
            diagram.StyleSheets.Add(ellipseStyle);

            // Connector style (blue text)
            StyleSheet connectorStyle = new StyleSheet();
            connectorStyle.ID = diagram.StyleSheets.Count + 1;
            Aspose.Diagram.Char connectorChar = new Aspose.Diagram.Char();
            connectorChar.IX = 0;
            connectorChar.Color.Value = "#0000FF"; // blue
            connectorStyle.Chars.Add(connectorChar);
            diagram.StyleSheets.Add(connectorStyle);

            // Iterate through all pages and shapes, applying the appropriate style
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine shape type via master name (fallback to shape name if master is null)
                    string masterName = shape.Master?.Name ?? string.Empty;
                    string shapeName = shape.Name ?? string.Empty;
                    string typeKey = masterName + shapeName;

                    // Apply style based on identified type
                    if (typeKey.IndexOf("Rectangle", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        shape.TextStyle = rectStyle;
                    }
                    else if (typeKey.IndexOf("Ellipse", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             typeKey.IndexOf("Oval", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        shape.TextStyle = ellipseStyle;
                    }
                    else if (typeKey.IndexOf("Connector", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             typeKey.IndexOf("Dynamic connector", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        shape.TextStyle = connectorStyle;
                    }
                    // Shapes that do not match any rule keep their existing style
                }
            }

            // Validation: ensure each shape's text color matches the expected style
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.True)
                        continue;

                    string expectedColor = null;
                    string masterName = shape.Master?.Name ?? string.Empty;
                    string shapeName = shape.Name ?? string.Empty;
                    string typeKey = masterName + shapeName;

                    if (typeKey.IndexOf("Rectangle", StringComparison.OrdinalIgnoreCase) >= 0)
                        expectedColor = "#FF0000";
                    else if (typeKey.IndexOf("Ellipse", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             typeKey.IndexOf("Oval", StringComparison.OrdinalIgnoreCase) >= 0)
                        expectedColor = "#00FF00";
                    else if (typeKey.IndexOf("Connector", StringComparison.OrdinalIgnoreCase) >= 0 ||
                             typeKey.IndexOf("Dynamic connector", StringComparison.OrdinalIgnoreCase) >= 0)
                        expectedColor = "#0000FF";

                    // If we have an expectation, verify it
                    if (expectedColor != null)
                    {
                        // Retrieve the applied color from the shape's text style
                        string actualColor = null;
                        if (shape.TextStyle != null && shape.TextStyle.Chars.Count > 0)
                        {
                            actualColor = shape.TextStyle.Chars[0].Color.Value;
                        }

                        if (!string.Equals(actualColor, expectedColor, StringComparison.OrdinalIgnoreCase))
                        {
                            string msg = $"Color mismatch on shape ID {shape.ID} (Master: {masterName}). Expected: {expectedColor}, Actual: {actualColor ?? "null"}";
                            Console.Error.WriteLine(msg);
                            // Optionally, throw to stop execution
                            throw new Exception(msg);
                        }
                    }
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any Aspose or runtime errors to the error stream
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}