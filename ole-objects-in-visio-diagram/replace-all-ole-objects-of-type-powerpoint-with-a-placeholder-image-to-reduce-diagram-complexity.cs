using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input Visio file path
                string inputPath = "input.vsdx";
                // Placeholder image file path (PNG, JPEG, etc.)
                string placeholderImagePath = "placeholder.png";
                // Output Visio file path
                string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape is an OLE object
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.ForeignType == ForeignType.Object)
                        {
                            // Determine if the OLE object is a PowerPoint file
                            string sourceName = shape.ForeignData.ObjectSourceFullName;
                            if (!string.IsNullOrEmpty(sourceName) &&
                                (sourceName.EndsWith(".ppt", StringComparison.OrdinalIgnoreCase) ||
                                 sourceName.EndsWith(".pptx", StringComparison.OrdinalIgnoreCase)))
                            {
                                // Load placeholder image bytes
                                byte[] imageBytes = File.ReadAllBytes(placeholderImagePath);

                                // Replace the OLE binary data with the placeholder image data
                                shape.ForeignData.ObjectData = imageBytes;

                                // Optionally display the OLE object as an icon
                                shape.ForeignData.ShowAsIcon = BOOL.True;
                            }
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