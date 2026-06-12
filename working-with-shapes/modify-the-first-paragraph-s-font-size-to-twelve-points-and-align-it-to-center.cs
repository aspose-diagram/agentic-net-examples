using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram (replace with actual file path)
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Access the first page
                Page firstPage = diagram.Pages[0];

                // Access the first shape on the page
                Shape firstShape = firstPage.Shapes[0];

                // Ensure the shape contains text and at least one paragraph
                if (firstShape.Text != null && !string.IsNullOrWhiteSpace(firstShape.Text.Value.ToString()) && firstShape.Paras.Count > 0)
                {
                    // Center align the first paragraph
                    firstShape.Paras[0].HorzAlign.Value = HorzAlignValue.Center;

                    // Set font size of all characters in the shape to 12 points (12/72 inches)
                    double sizeInInches = 12.0 / 72.0;
                    foreach (Aspose.Diagram.Char ch in firstShape.Chars)
                    {
                        ch.Size.Value = sizeInInches;
                    }
                }
                else
                {
                    Console.WriteLine("The first shape does not contain text or paragraphs.");
                }

                // Save the modified diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                // Clean up
                diagram.Dispose();

                Console.WriteLine("Diagram processing completed.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }