using System.IO;
using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the plain concatenated text of the shape
                    string originalText = shape.Text.Value.Text;

                    // Check if the text contains the target word
                    if (!string.IsNullOrEmpty(originalText) && originalText.Contains("Confidential"))
                    {
                        // Replace 'Confidential' with 'Public'
                        string updatedText = originalText.Replace("Confidential", "Public");

                        // Clear existing text runs and add the updated text
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(updatedText));
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
