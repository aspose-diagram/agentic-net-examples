using System;
using System.IO;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the Visio file to be processed
            string inputPath = "input.vsdx";

            // Path where the CSV file will be created
            string csvPath = "shapes.csv";

            // Load the diagram from the specified file
            Diagram diagram = new Diagram(inputPath);

            // Create a StreamWriter to write CSV content
            using (StreamWriter writer = new StreamWriter(csvPath))
            {
                // Write CSV header
                writer.WriteLine("PageName,ShapeID,Name,NameU,Data1,Data2,Data3,Text");

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the current page
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip shapes that are marked as deleted
                        if (shape.Del == BOOL.True)
                            continue;

                        // Retrieve plain text from the shape
                        string plainText = shape.Text.Value.Text;

                        // Escape double quotes and wrap the text in quotes to handle commas
                        plainText = plainText.Replace("\"", "\"\"");
                        plainText = $"\"{plainText}\"";

                        // Build a CSV line with the required shape information
                        string line = $"{page.Name},{shape.ID},{shape.Name},{shape.NameU},{shape.Data1},{shape.Data2},{shape.Data3},{plainText}";

                        // Write the line to the CSV file
                        writer.WriteLine(line);
                    }
                }
            }

            // Optional: Save the diagram back to a file if modifications were made
            // diagram.Save("output.vsdx", SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
