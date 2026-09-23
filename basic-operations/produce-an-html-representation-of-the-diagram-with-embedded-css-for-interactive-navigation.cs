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

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output HTML file path
                string outputPath = "output.html";

                // Load the diagram from file
                Diagram diagram = new Diagram(inputPath);

                // Add a hyperlink to each shape for interactive navigation
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Create a new hyperlink that points to an anchor based on the shape ID
                        Hyperlink link = new Hyperlink();
                        link.Name = "ShapeLink";
                        link.Address.Value = $"#shape{shape.ID}";
                        link.Description.Value = $"Navigate to shape {shape.ID}";

                        // Add the hyperlink to the shape's collection
                        shape.Hyperlinks.Add(link);
                    }
                }

                // Configure HTML export options
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                // Do not export hidden pages (optional)
                htmlOptions.ExportHiddenPage = false;

                // Save the diagram as an HTML file with embedded CSS and navigation links
                diagram.Save(outputPath, htmlOptions);

                Console.WriteLine($"Diagram exported to HTML successfully: {Path.GetFullPath(outputPath)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }