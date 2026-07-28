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

            // Path to the source Visio file
            string inputPath = "input.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the plain text of the shape
                    string currentText = shape.Text.Value.ToString();

                    // If the text is empty or whitespace, populate it with a unique identifier
                    if (string.IsNullOrWhiteSpace(currentText))
                    {
                        string uniqueId = Guid.NewGuid().ToString();

                        // Clear any existing text runs and add the new identifier
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(uniqueId));
                    }
                }
            }

            // Save the modified diagram
            string outputPath = "output.vsdx";
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
