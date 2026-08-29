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

                // Load diagram from a memory stream (replace with actual byte array source)
                byte[] inputBytes = File.ReadAllBytes("input.vsdx");
                using (MemoryStream inputStream = new MemoryStream(inputBytes))
                using (Diagram diagram = new Diagram(inputStream))
                {
                    // Access the first page
                    Page page = diagram.Pages[0];

                    // Retrieve the first shape on the page
                    long firstShapeId = 0;
                    foreach (Shape s in page.Shapes)
                    {
                        firstShapeId = s.ID;
                        break;
                    }

                    if (firstShapeId != 0)
                    {
                        Shape shape = page.Shapes.GetShape(firstShapeId);
                        // Modify shape size (width and height are in inches)
                        shape.XForm.Width.Value = 2.0;   // 2 inches wide
                        shape.XForm.Height.Value = 1.0;  // 1 inch tall
                    }

                    // Save the modified diagram to a byte array
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        diagram.Save(outputStream, SaveFileFormat.Vsdx);
                        byte[] outputBytes = outputStream.ToArray();

                        // Optional: write the result to a file
                        File.WriteAllBytes("output.vsdx", outputBytes);
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }