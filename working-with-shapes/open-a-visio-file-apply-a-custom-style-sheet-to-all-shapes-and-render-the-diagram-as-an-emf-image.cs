using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";

                // Output EMF image path
                string outputPath = "output.emf";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // -------------------------------------------------
                // Create a custom style sheet
                // -------------------------------------------------
                StyleSheet customStyle = new StyleSheet();

                // Assign a unique ID (next available)
                customStyle.ID = diagram.StyleSheets.Count + 1;

                // Optional: give the style a name
                customStyle.Name = "CustomStyle";

                // Set line color to red
                customStyle.Line.LineColor.Value = "#FF0000";

                // Set fill foreground color to green
                customStyle.Fill.FillForegnd.Value = "#00FF00";

                // Define a character style (e.g., blue text, 12pt font)
                Aspose.Diagram.Char charStyle = new Aspose.Diagram.Char();
                charStyle.IX = 0;                                 // Character index
                charStyle.Color.Value = "#0000FF";                // Text color
                charStyle.Size.Value = 12.0 / 72.0;               // Font size in inches (12 pt)
                charStyle.Style.Value = StyleValue.Bold;         // Bold style (optional)
                customStyle.Chars.Add(charStyle);

                // Add the style sheet to the diagram
                diagram.StyleSheets.Add(customStyle);

                // -------------------------------------------------
                // Apply the custom style sheet to all shapes
                // -------------------------------------------------
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Apply the style to text, fill, and line formatting
                        shape.TextStyle = customStyle;
                        shape.FillStyle = customStyle;
                        shape.LineStyle = customStyle;
                    }
                }

                // -------------------------------------------------
                // Render the diagram as an EMF image
                // -------------------------------------------------
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf);
                // Export only the first page (index 0)
                saveOptions.PageIndex = 0;
                // Do not export hidden pages
                saveOptions.ExportHiddenPage = false;

                diagram.Save(outputPath, saveOptions);

                Console.WriteLine("Diagram processed and saved as EMF image successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }