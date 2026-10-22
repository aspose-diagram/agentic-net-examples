using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate command‑line arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard against missing input file.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate over all pages in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate over all shapes on the current page.
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify image shapes (foreign data).
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData?.Value != null)
                        {
                            // Extract the raw image bytes.
                            byte[] imageBytes = shape.ForeignData.Value;

                            // Convert the image bytes to a Base64 string.
                            string base64 = Convert.ToBase64String(imageBytes);

                            // Build a data URI (assuming PNG format; adjust if needed).
                            string dataUri = $"data:image/png;base64,{base64}";

                            // Embed the data URI back into the shape as a comment/alt‑text.
                            // The Misc.Comment cell holds the comment string.
                            if (shape.Misc != null && shape.Misc.Comment != null)
                            {
                                shape.Misc.Comment.Value = dataUri;
                            }
                        }
                    }
                }

                // Save the modified diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error stream.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }
}