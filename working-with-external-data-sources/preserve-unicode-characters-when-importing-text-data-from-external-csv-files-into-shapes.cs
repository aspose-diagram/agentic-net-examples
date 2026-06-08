using System;
using System.IO;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Paths – adjust as needed
            string csvPath = "data.csv";
            string outputPath = "output.vsdx";

            // Load or create a new diagram
            Diagram diagram = new Diagram();

            // Starting Y position for shapes
            double startY = 1.0;
            double offsetY = 1.5; // space between shapes

            // Read CSV file with UTF-8 encoding to preserve Unicode characters
            using (var reader = new StreamReader(csvPath, Encoding.UTF8))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    // Add a rectangle shape for each CSV line
                    long shapeId = diagram.ActivePage.AddShape(1.0, startY, "Rectangle");
                    Shape shape = diagram.ActivePage.Shapes.GetShape(shapeId);

                    // Clear any existing text and set the Unicode text from CSV
                    shape.Text.Value.Clear();
                    shape.Text.Value.Add(new Txt(line));

                    // Move to next vertical position
                    startY += offsetY;
                }
            }

            // Save the diagram with a Unicode‑compatible fallback font
            DiagramSaveOptions saveOptions = new DiagramSaveOptions();
            saveOptions.DefaultFont = "Arial Unicode MS";

            diagram.Save(outputPath, saveOptions);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
