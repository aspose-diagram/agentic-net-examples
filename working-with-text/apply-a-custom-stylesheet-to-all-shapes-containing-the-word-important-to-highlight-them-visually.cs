using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // -------------------------------------------------
                // Create a custom stylesheet to highlight shapes
                // -------------------------------------------------
                StyleSheet highlightStyle = new StyleSheet();
                // Assign a unique ID (next available)
                highlightStyle.ID = diagram.StyleSheets.Count + 1;

                // Text formatting: black color
                Aspose.Diagram.Char textChar = new Aspose.Diagram.Char();
                textChar.IX = 0; // first character run
                textChar.Color.Value = "#000000"; // black
                highlightStyle.Chars.Add(textChar);

                // Line formatting: red border
                highlightStyle.Line.LineColor.Value = "#FF0000"; // red

                // Fill formatting: yellow background
                highlightStyle.Fill.FillForegnd.Value = "#FFFF00"; // yellow

                // Add the stylesheet to the diagram's collection
                diagram.StyleSheets.Add(highlightStyle);

                // -------------------------------------------------
                // Apply the stylesheet to all shapes containing "Important"
                // -------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text of the shape
                        string shapeText = shape.Text.Value.ToString();

                        // Check if the text contains the keyword (case‑insensitive)
                        if (!string.IsNullOrEmpty(shapeText) &&
                            shapeText.IndexOf("Important", StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            // Apply the custom stylesheet to text, fill, and line
                            shape.TextStyle = highlightStyle;
                            shape.FillStyle = highlightStyle;
                            shape.LineStyle = highlightStyle;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }