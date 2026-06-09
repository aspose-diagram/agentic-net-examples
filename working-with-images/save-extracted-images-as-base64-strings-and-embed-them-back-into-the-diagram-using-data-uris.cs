using System;
using System.IO;
using System.Text;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Path to the source Visio file
            string inputPath = "input.vsdx";
            // Path for the output Visio file
            string outputPath = "output.vsdx";

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the page
                foreach (Shape shape in page.Shapes)
                {
                    // Identify image (foreign) shapes
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        // Extract the raw image bytes
                        byte[] imageBytes = shape.ForeignData.Value;

                        // Convert to Base64 string
                        string base64 = Convert.ToBase64String(imageBytes);

                        // Build a data URI (assuming PNG; adjust if needed)
                        string dataUri = $"data:image/png;base64,{base64}";

                        // Embed the data URI back into the shape as its text content
                        shape.Text.Value.Clear();
                        shape.Text.Value.Add(new Txt(dataUri));
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
