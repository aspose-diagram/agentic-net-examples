using System;
using System.Text.RegularExpressions;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths – replace with actual file locations as needed
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Locate the built‑in "Title" style sheet
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
                    Console.WriteLine("The 'Title' style sheet was not found in the document.");
                    return;
                }

                // Regular expression to detect simple date patterns (e.g., 12/31/2023 or 2023-12-31)
                string datePattern = @"\b\d{1,2}[/-]\d{1,2}[/-]\d{2,4}\b";

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text from the shape
                        string plainText = shape.Text.Value.Text;

                        if (string.IsNullOrWhiteSpace(plainText))
                            continue;

                        // Apply the Title style if the text matches the date pattern
                        if (Regex.IsMatch(plainText, datePattern))
                        {
                            shape.TextStyle = titleStyle;
                            shape.FillStyle = titleStyle;
                            shape.LineStyle = titleStyle;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }