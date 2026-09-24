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

                // Example input diagram bytes (replace with actual data)
                byte[] inputBytes = File.ReadAllBytes("input.vsdx");

                // Load diagram from memory stream
                using (MemoryStream inputStream = new MemoryStream(inputBytes))
                {
                    Diagram diagram = new Diagram(inputStream);

                    // Modify size of all shapes on the first page
                    Page firstPage = diagram.Pages[0];
                    foreach (Shape shape in firstPage.Shapes)
                    {
                        // Set new width and height (in inches)
                        shape.XForm.Width.Value = 2.0;   // example width
                        shape.XForm.Height.Value = 1.0;  // example height
                    }

                    // Save the modified diagram to a byte array
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        diagram.Save(outputStream, SaveFileFormat.Vsdx);
                        byte[] resultBytes = outputStream.ToArray();

                        // Example usage of the resulting byte array
                        Console.WriteLine($"Modified diagram size: {resultBytes.Length} bytes");
                        // Optionally write to a file for verification
                        File.WriteAllBytes("output.vsdx", resultBytes);
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }