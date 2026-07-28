using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Paths to the source Visio file and the output file
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the new PNG image into a byte array (in-memory)
                byte[] newPngData = File.ReadAllBytes("newImage.png");

                // Load the existing Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes to find the placeholder shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify the placeholder shape by its universal name and ensure it is an image (Foreign) shape
                        if (shape.NameU == "Placeholder" && shape.Type == TypeValue.Foreign)
                        {
                            // Replace the embedded image data with the new PNG bytes
                            shape.ForeignData.Value = newPngData;
                        }
                    }
                }

                // Save the modified diagram back to VSDX format
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }