using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source and destination Visio files
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Find the built‑in "Subtitle" style sheet
                StyleSheet subtitleStyle = null;
                foreach (StyleSheet ss in diagram.StyleSheets)
                {
                    if (ss.Name == "Subtitle")
                    {
                        subtitleStyle = ss;
                        break;
                    }
                }

                if (subtitleStyle == null)
                {
                    Console.WriteLine("Subtitle style not found in the document.");
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

                        // Trim leading whitespace and check for numeric prefix
                        if (!string.IsNullOrWhiteSpace(plainText))
                        {
                            string trimmed = plainText.TrimStart();
                            if (trimmed.Length > 0 && char.IsDigit(trimmed[0]))
                            {
                                // Apply the "Subtitle" style to the shape's text
                                shape.TextStyle = subtitleStyle;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved with updated styles.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }