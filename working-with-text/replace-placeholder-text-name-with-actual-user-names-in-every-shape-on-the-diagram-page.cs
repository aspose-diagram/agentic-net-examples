using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        try
        {

            // Input and output file paths
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // The actual user name to replace the placeholder with
            string userName = "John Doe";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Get the current plain text of the shape
                    string currentText = shape.Text.Value.Text;

                    // If the placeholder exists, replace it with the actual name
                    if (!string.IsNullOrEmpty(currentText) && currentText.Contains("[Name]"))
                    {
                        string newText = currentText.Replace("[Name]", userName);

                        // Clear existing text runs and add the updated text
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(newText));
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
