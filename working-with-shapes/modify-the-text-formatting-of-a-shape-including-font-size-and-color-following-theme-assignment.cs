using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load an existing Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page
                Page page = diagram.Pages[0];

                // Target shape name (adjust as needed)
                string targetShapeName = "TargetShape";

                // Iterate through shapes to find the target
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes
                    if (shape.Del == BOOL.True)
                        continue;

                    // Match by universal name
                    if (shape.NameU != null && shape.NameU.Equals(targetShapeName, StringComparison.OrdinalIgnoreCase))
                    {
                        // Ensure the shape has text
                        if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                        {
                            // Clear existing character formatting
                            shape.Chars.Clear();

                            // Create a new character formatting entry
                            Aspose.Diagram.Char ch = new Aspose.Diagram.Char();
                            ch.IX = 0; // Apply to the first character run
                            ch.FontName.Value = "Calibri";               // Font name
                            ch.Size.Value = 12.0 / 72.0;                 // Font size in inches (12 pt)
                            ch.Color.Value = "#FF0000";                  // Red color in HEX
                            ch.Style.Value = StyleValue.Bold;            // Example: make text bold

                            // Add the character formatting to the shape
                            shape.Chars.Add(ch);
                        }

                        // Apply a preset theme to the shape (write‑only properties)
                        shape.PresetTheme = PresetThemeValue.Bubble;
                        shape.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                        // No need to continue searching after the target is processed
                        break;
                    }
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }