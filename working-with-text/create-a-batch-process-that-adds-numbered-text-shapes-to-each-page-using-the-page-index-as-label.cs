using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (replace with actual path)
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page and add a numbered text shape
                int pageNumber = 0;
                foreach (Page page in diagram.Pages)
                {
                    pageNumber++;

                    // Retrieve page dimensions (in inches)
                    double pageWidth = page.PageSheet.PageProps.PageWidth.Value;
                    double pageHeight = page.PageSheet.PageProps.PageHeight.Value;

                    // Define position and size for the text shape
                    double pinX = 1.0;                     // X coordinate (center) from left
                    double pinY = pageHeight - 1.0;        // Y coordinate (center) from top
                    double shapeWidth = 2.0;               // Width of the text shape
                    double shapeHeight = 0.5;              // Height of the text shape

                    // Create label using the page index
                    string label = $"Page {pageNumber}";

                    // Add the text shape to the current page
                    Shape textShape = page.AddText(pinX, pinY, shapeWidth, shapeHeight, label);

                    // Optional: set font size and color (using shape's text collection)
                    textShape.Text.Value.Clear();
                    textShape.Text.Value.Add(new Txt(label));
                    // Example of setting font size (in inches) and color
                    // textShape.Chars[0].Size.Value = 12.0 / 72.0; // 12 pt
                    // textShape.Chars[0].Color.Value = "#000000"; // Black
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