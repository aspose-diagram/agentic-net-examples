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

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Create a custom style sheet
                StyleSheet customStyle = new StyleSheet();
                // Assign a unique ID (next available)
                customStyle.ID = diagram.StyleSheets.Count + 1;

                // Example: set a character style (first character)
                Aspose.Diagram.Char charStyle = new Aspose.Diagram.Char();
                charStyle.IX = 0; // character index
                charStyle.Color.Value = "#0066CC"; // text color
                charStyle.Size.Value = 0.12; // font size in inches (~9pt)
                charStyle.Style.Value = StyleValue.Bold; // bold text
                customStyle.Chars.Add(charStyle);

                // Example: set line style
                customStyle.Line.LineColor.Value = "#FF0000"; // red line
                customStyle.Line.LineWeight.Value = 0.02; // line weight in inches

                // Example: set fill style
                customStyle.Fill.FillForegnd.Value = "#FFFF99"; // light yellow fill
                customStyle.Fill.FillPattern.Value = 1; // solid fill

                // Add the style sheet to the diagram
                diagram.StyleSheets.Add(customStyle);

                // Apply the custom style to all shapes on each page
                foreach (Page page in diagram.Pages)
                {
                    // Apply the style sheet to text, line, and fill of all shapes on the page
                    page.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);
                }

                // Configure image save options for EMF format
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf);
                // Export only the first page (adjust as needed)
                saveOptions.PageIndex = 0;
                saveOptions.PageCount = 1;
                // Do not export hidden pages
                saveOptions.ExportHiddenPage = false;

                // Output EMF file path
                string outputPath = "output.emf";

                // Save the diagram as an EMF image
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine("Diagram exported to EMF successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }