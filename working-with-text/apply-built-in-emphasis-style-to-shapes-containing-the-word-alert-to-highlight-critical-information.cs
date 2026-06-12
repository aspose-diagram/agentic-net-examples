using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Locate the built‑in "Emphasis" style sheet in the document
                StyleSheet emphasisStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "Emphasis")
                    {
                        emphasisStyle = ss;
                        break;
                    }
                }

                if (emphasisStyle == null)
                {
                    Console.WriteLine("Emphasis style not found in the diagram. No changes applied.");
                    return;
                }

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Get the plain text of the shape
                        string shapeText = shape.Text.Value.ToString();

                        // Apply the Emphasis style if the text contains the word "Alert"
                        if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains("Alert"))
                        {
                            shape.TextStyle = emphasisStyle;
                            shape.FillStyle = emphasisStyle;
                            shape.LineStyle = emphasisStyle;
                        }
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine($"Diagram saved with Emphasis style applied to shapes containing 'Alert' at: {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }