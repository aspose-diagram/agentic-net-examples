using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
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
                            // Extract raw image bytes
                            byte[] imageBytes = shape.ForeignData.Value;

                            // Convert to Base64 string
                            string base64String = Convert.ToBase64String(imageBytes);

                            // Assume PNG format for the data URI (adjust if needed)
                            string mimeType = "image/png";

                            // Build the data URI
                            string dataUri = $"data:{mimeType};base64,{base64String}";

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