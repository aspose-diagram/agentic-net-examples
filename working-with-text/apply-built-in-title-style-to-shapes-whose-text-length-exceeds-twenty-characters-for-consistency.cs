using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Locate the built‑in "Title" style sheet once
                StyleSheet titleStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "Title")
                    {
                        titleStyle = ss;
                        break;
                    }
                }

                // If the "Title" style does not exist, exit early
                if (titleStyle == null)
                {
                    Console.WriteLine("Title style not found in the document.");
                    return;
                }

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip logically deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text of the shape
                        string plainText = shape.Text.Value.ToString();

                        // Apply the Title style if text length exceeds 20 characters
                        if (!string.IsNullOrEmpty(plainText) && plainText.Length > 20)
                        {
                            shape.TextStyle = titleStyle;
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to {outputPath}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }