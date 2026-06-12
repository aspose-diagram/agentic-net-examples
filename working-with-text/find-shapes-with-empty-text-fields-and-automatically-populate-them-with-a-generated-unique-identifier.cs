using System.IO;
using System;
using Aspose.Diagram;

public class Program
{
    public static void Main()
    {
        try
        {

            // Input and output file paths (adjust as needed)
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Skip shapes that are marked as deleted
                    if (shape.Del == BOOL.True)
                        continue;

                    // Retrieve the plain text of the shape
                    string currentText = shape.Text.Value.ToString();

                    // If the shape has no text, assign a new unique identifier
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
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
