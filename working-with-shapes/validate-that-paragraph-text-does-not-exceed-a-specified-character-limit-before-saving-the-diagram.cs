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

                // Path for the saved Visio file after validation
                string outputPath = "output.vsdx";

                // Maximum allowed characters for any shape's paragraph text
                const int maxParagraphLength = 200;

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Validate each shape's text content
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Retrieve plain concatenated text from the shape
                        string plainText = shape.Text.Value.ToString();

                        // If the text exceeds the limit, abort with an exception
                        if (plainText.Length > maxParagraphLength)
                        {
                            throw new Exception(
                                $"Shape ID {shape.ID} on page '{page.Name}' exceeds the allowed character limit of {maxParagraphLength} characters. Actual length: {plainText.Length}.");
                        }
                    }
                }

                // Save the diagram (no changes made, just re-saving after validation)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

                Console.WriteLine("Diagram validated and saved successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }