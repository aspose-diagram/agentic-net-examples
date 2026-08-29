using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the cleaned output file
                string outputPath = "output_cleaned.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the current page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve the plain text of the shape
                        string shapeText = shape.Text.Value.Text;

                        // Check if the text contains the placeholder "TODO"
                        if (!string.IsNullOrEmpty(shapeText) && shapeText.Contains("TODO"))
                        {
                            // Replace all occurrences of "TODO" with an empty string
                            string cleanedText = shapeText.Replace("TODO", string.Empty);

                            // Update the shape's text content
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt(cleanedText));

                            // Log the affected shape ID
                            Console.WriteLine($"Shape ID {shape.ID} had 'TODO' removed.");
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