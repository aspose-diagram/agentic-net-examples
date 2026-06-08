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

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Refresh data connections to ensure latest external data is loaded
                diagram.Refresh();

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each shape on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Skip deleted shapes
                        if (shape.Del == BOOL.True)
                            continue;

                        // Example: assume external data is stored in Data1 property
                        string externalValue = shape.Data1;

                        // If there is a value, apply transformation
                        if (!string.IsNullOrEmpty(externalValue))
                        {
                            // Sample transformation: trim, replace newlines and commas
                            string transformed = externalValue.Replace("\r\n", " ").Replace("\n", " ").Replace(",", " ").Trim();

                            // Update the shape's text with the transformed value
                            shape.Text.Value.Clear();
                            shape.Text.Value.Add(new Txt(transformed));
                        }
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }