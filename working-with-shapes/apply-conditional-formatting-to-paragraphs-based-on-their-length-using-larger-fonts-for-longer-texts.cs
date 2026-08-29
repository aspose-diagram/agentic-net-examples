using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path (change as needed)
                string inputPath = "input.vsdx";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Ensure the shape has text
                        if (shape.Text != null && !string.IsNullOrWhiteSpace(shape.Text.Value.Text))
                        {
                            // Get the total length of the shape's text
                            int textLength = shape.Text.Value.Text.Length;

                            // Determine font size based on length (size is in inches)
                            // Base size 0.1 inch (~7.2 pt). Increase by 0.02 inch per 50 characters.
                            double fontSize = 0.1 + (textLength / 50.0) * 0.02;

                            // Ensure there is at least one Char object to apply formatting
                            if (shape.Chars.Count == 0)
                            {
                                Aspose.Diagram.Char newChar = new Aspose.Diagram.Char();
                                newChar.IX = 0; // start index
                                shape.Chars.Add(newChar);
                            }

                            // Apply the calculated font size to all character runs in the shape
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                ch.Size.Value = fontSize;
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