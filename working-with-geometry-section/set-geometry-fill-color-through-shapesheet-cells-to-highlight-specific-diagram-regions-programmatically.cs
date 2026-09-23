using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output_highlighted.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the fill color to use for highlighting (hex color string)
                string highlightColor = "#FFFF00"; // Yellow

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Example condition: highlight shapes whose universal name is "Process"
                        // Adjust the condition as needed (e.g., by shape ID, master name, etc.)
                        if (shape.NameU != null && shape.NameU.Equals("Process", StringComparison.OrdinalIgnoreCase))
                        {
                            // Ensure the shape is not deleted
                            if (shape.Del == BOOL.False)
                            {
                                // Set solid fill pattern
                                shape.Fill.FillPattern.Value = 1; // 1 = solid fill
                                // Set the foreground fill color to the highlight color
                                shape.Fill.FillForegnd.Value = highlightColor;
                                // Optionally set background fill color (here set to white)
                                shape.Fill.FillBkgnd.Value = "#FFFFFF";
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }