using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input Visio file path (replace with actual path)
                string inputPath = "input.vsdx";

                // Output CSV file path
                string outputCsv = "output.csv";

                // Load the Visio diagram
                using (Diagram diagram = new Diagram(inputPath))
                {
                    // Prepare the CSV file for writing
                    using (StreamWriter writer = new StreamWriter(outputCsv, false))
                    {
                        // Write CSV header
                        writer.WriteLine("PageName,ShapeID,ShapeName,Text");

                        // Iterate through each page in the diagram
                        foreach (Page page in diagram.Pages)
                        {
                            // Iterate through each shape on the current page
                            foreach (Shape shape in page.Shapes)
                            {
                                // Retrieve plain text from the shape
                                string text = shape.Text.Value.Text;

                                // Clean the text: replace line breaks and commas to keep CSV format simple
                                if (!string.IsNullOrEmpty(text))
                                {
                                    text = text.Replace("\r\n", " ")
                                               .Replace("\n", " ")
                                               .Replace(",", " ");
                                }

                                // Write a CSV line with page name, shape ID, shape name, and cleaned text
                                writer.WriteLine($"{page.Name},{shape.ID},{shape.Name},{text}");
                            }
                        }
                    }
                }

                Console.WriteLine($"Text extraction completed. CSV saved to: {outputCsv}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }