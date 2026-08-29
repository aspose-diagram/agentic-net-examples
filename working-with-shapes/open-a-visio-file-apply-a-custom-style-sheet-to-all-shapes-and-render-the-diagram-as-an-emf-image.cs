using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio file path (modify as needed)
        string inputPath = "input.vsdx";
        // Guard: ensure the input file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Output EMF image file path (modify as needed)
        string outputPath = "output.emf";

        try
        {
            // Load the Visio diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // ---------- Create a custom style sheet ----------
            // Initialize a new StyleSheet instance
            StyleSheet customStyle = new StyleSheet();

            // Assign a unique ID based on the current count of style sheets
            customStyle.ID = diagram.StyleSheets.Count + 1;

            // ----- Define character (text) formatting -----
            // Create a Char object to set text color and style
            Aspose.Diagram.Char textChar = new Aspose.Diagram.Char
            {
                IX = 0,                         // Index of the character run
                Color = { Value = "#FF0000" }, // Red text color (hex)
                Style = { Value = StyleValue.Bold } // Bold style
            };
            // Add the Char definition to the style sheet
            customStyle.Chars.Add(textChar);

            // ----- Define line formatting -----
            // Set line color to blue and a dash pattern
            customStyle.Line.LineColor.Value = "#0000FF"; // Blue line
            customStyle.Line.LinePattern.Value = LinePatternValue.Dash; // Dashed line
            customStyle.Line.LineWeight.Value = 0.02; // Thin line (in inches)

            // ----- Define fill formatting -----
            // Set fill foreground color to light gray
            customStyle.Fill.FillForegnd.Value = "#CCCCCC";
            // Use solid fill pattern (value 1)
            customStyle.Fill.FillPattern.Value = 1;

            // Add the custom style sheet to the diagram's collection
            diagram.StyleSheets.Add(customStyle);

            // ---------- Apply the custom style sheet to all shapes ----------
            // Iterate through each page in the diagram
            foreach (Page page in diagram.Pages)
            {
                // Apply the style sheet IDs for text, line, and fill to the page
                page.ApplyStyle(customStyle.ID, customStyle.ID, customStyle.ID);
            }

            // ---------- Render the diagram as an EMF image ----------
            // Configure image save options for EMF format
            ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Emf)
            {
                // Export only the first page (index 0); adjust if needed
                PageIndex = 0,
                // Ensure hidden pages are not exported
                ExportHiddenPage = false
            };

            // Save the diagram to the EMF file using the configured options
            diagram.Save(outputPath, saveOptions);

            // Inform the user of successful completion
            Console.WriteLine($"Diagram saved as EMF image to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Write any errors encountered during processing to the error stream
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}