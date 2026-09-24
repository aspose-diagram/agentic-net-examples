using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths
                string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the text to find and its replacement
                const string oldText = "Version 1.0";
                const string newText = "Version 2.0";

                // Get the first page (index 0)
                Page firstPage = diagram.Pages[0];

                // Iterate over all shapes on the first page
                foreach (Shape shape in firstPage.Shapes)
                {
                    // Ensure the shape has text
                    if (shape.Text == null || shape.Text.Value == null)
                        continue;

                    // Iterate through each text run (Txt) within the shape's text collection
                    foreach (var fmt in shape.Text.Value)
                    {
                        if (fmt is Txt txt && txt.Text != null && txt.Text.Contains(oldText))
                        {
                            // Replace the target substring
                            txt.Text = txt.Text.Replace(oldText, newText);
                        }
                    }
                }

                // Save the modified diagram (preserving the original format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }