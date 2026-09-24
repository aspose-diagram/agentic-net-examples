using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with your actual file path)
                Diagram diagram = new Diagram("input.vsdx");

                // Locate the built‑in style sheet named "Subtitle"
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
                    Console.WriteLine("Subtitle style sheet not found. No changes will be applied.");
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
                        string plainText = shape.Text.Value.Text;

                        // Check if the text starts with a numeric character (ignoring leading whitespace)
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
                diagram.Save("output.vsdx", SaveFileFormat.Vsdx);
                Console.WriteLine("Processing complete. Diagram saved as output.vsdx.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }