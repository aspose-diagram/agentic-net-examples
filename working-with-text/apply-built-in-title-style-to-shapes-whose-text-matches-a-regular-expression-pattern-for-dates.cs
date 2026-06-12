using System;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";
                // Path to the output Visio file
                const string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the regular expression pattern for dates (e.g., 12/31/2023)
                string datePattern = @"\b\d{1,2}/\d{1,2}/\d{4}\b";

                // Locate the built‑in "Title" style sheet (if it exists)
                StyleSheet titleStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "Title")
                    {
                        titleStyle = ss;
                        break;
                    }
                }

                if (titleStyle == null)
                {
                    Console.WriteLine("Title style not found in the document. No changes will be applied.");
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

                        // Retrieve plain text from the shape
                        string shapeText = shape.Text.Value.ToString();

                        // Apply the Title style if the text matches the date pattern
                        if (!string.IsNullOrWhiteSpace(shapeText) && Regex.IsMatch(shapeText, datePattern))
                        {
                            shape.TextStyle = titleStyle;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved with Title style applied to matching shapes.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }