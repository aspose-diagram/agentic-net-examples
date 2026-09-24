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

            // Load an existing Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Create a stylesheet for Rectangle shapes (text color red)
            StyleSheet rectStyle = new StyleSheet();
            rectStyle.ID = diagram.StyleSheets.Count + 1;
            Aspose.Diagram.Char rectChar = new Aspose.Diagram.Char();
            rectChar.IX = 0;
            rectChar.Color.Value = "#FF0000"; // Red
            rectStyle.Chars.Add(rectChar);
            diagram.StyleSheets.Add(rectStyle);

            // Create a stylesheet for Ellipse shapes (text color green)
            StyleSheet ellipseStyle = new StyleSheet();
            ellipseStyle.ID = diagram.StyleSheets.Count + 1;
            Aspose.Diagram.Char ellipseChar = new Aspose.Diagram.Char();
            ellipseChar.IX = 0;
            ellipseChar.Color.Value = "#00FF00"; // Green
            ellipseStyle.Chars.Add(ellipseChar);
            diagram.StyleSheets.Add(ellipseStyle);

            // Create a stylesheet for Connector shapes (text color blue)
            StyleSheet connectorStyle = new StyleSheet();
            connectorStyle.ID = diagram.StyleSheets.Count + 1;
            Aspose.Diagram.Char connectorChar = new Aspose.Diagram.Char();
            connectorChar.IX = 0;
            connectorChar.Color.Value = "#0000FF"; // Blue
            connectorStyle.Chars.Add(connectorChar);
            diagram.StyleSheets.Add(connectorStyle);

            // Apply the appropriate stylesheet to each shape based on its type
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Determine shape type
                    string masterName = shape.Master?.Name ?? string.Empty;

                    if (masterName.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                    {
                        shape.TextStyle = rectStyle;
                    }
                    else if (masterName.Equals("Ellipse", StringComparison.OrdinalIgnoreCase))
                    {
                        shape.TextStyle = ellipseStyle;
                    }
                    else if (shape.OneD) // Connectors are 1-D shapes
                    {
                        shape.TextStyle = connectorStyle;
                    }
                    // For other shapes, no style change
                }
            }

            // Save the modified diagram
            diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

            // Validation: ensure each shape's text color matches the expected color
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes without text characters
                    if (shape.Chars == null || shape.Chars.Count == 0)
                        continue;

                    // Expected color based on shape type
                    string expectedColor = null;
                    string masterName = shape.Master?.Name ?? string.Empty;

                    if (masterName.Equals("Rectangle", StringComparison.OrdinalIgnoreCase))
                    {
                        expectedColor = "#FF0000";
                    }
                    else if (masterName.Equals("Ellipse", StringComparison.OrdinalIgnoreCase))
                    {
                        expectedColor = "#00FF00";
                    }
                    else if (shape.OneD) // Connector
                    {
                        expectedColor = "#0000FF";
                    }

                    if (expectedColor != null)
                    {
                        // Retrieve the actual color from the first character run
                        string actualColor = shape.Chars[0].Color.Value;

                        if (!string.Equals(actualColor, expectedColor, StringComparison.OrdinalIgnoreCase))
                        {
                            string message = $"Shape ID {shape.ID} on page '{page.Name}' has text color '{actualColor}' but expected '{expectedColor}'.";
                            Console.WriteLine(message);
                            throw new Exception(message);
                        }
                        else
                        {
                            Console.WriteLine($"Shape ID {shape.ID} validated with correct color '{actualColor}'.");
                        }
                    }
                }
            }

            Console.WriteLine("All shape text colors validated successfully.");

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
