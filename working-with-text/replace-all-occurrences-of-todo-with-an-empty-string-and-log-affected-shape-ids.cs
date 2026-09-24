using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Load the Visio diagram
                string inputPath = "input.vsdx";
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        bool shapeModified = false;

                        // Iterate through the text runs of the shape
                        foreach (var item in shape.Text.Value)
                        {
                            if (item is Txt txt && txt.Text != null && txt.Text.Contains("TODO"))
                            {
                                // Replace all occurrences of 'TODO' with an empty string
                                txt.Text = txt.Text.Replace("TODO", string.Empty);
                                shapeModified = true;
                            }
                        }

                        // Log the ID of any shape that was modified
                        if (shapeModified)
                        {
                            Console.WriteLine($"Modified shape ID: {shape.ID}");
                        }
                    }
                }

                // Save the updated diagram
                string outputPath = "output.vsdx";
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }