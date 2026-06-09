using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the VSDX file to validate
                string diagramPath = "input.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(diagramPath, LoadFileFormat.Vsdx);

                // Minimum required DPI
                const double minDpi = 300.0;
                bool allImagesValid = true;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify embedded images (foreign shapes)
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            byte[] imageBytes = shape.ForeignData.Value;

                            // Load image from raw byte data
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                            {
                                double hDpi = img.HorizontalResolution;
                                double vDpi = img.VerticalResolution;

                                // Validate DPI
                                if (hDpi < minDpi || vDpi < minDpi)
                                {
                                    allImagesValid = false;
                                    Console.WriteLine($"Image in shape ID {shape.ID} on page {page.ID} has insufficient resolution: {hDpi}x{vDpi} DPI.");
                                }
                            }
                        }
                    }
                }

                if (allImagesValid)
                {
                    Console.WriteLine("All embedded images meet the minimum resolution of 300 DPI.");
                }
                else
                {
                    throw new Exception("One or more embedded images do not meet the minimum resolution requirement.");
                }

                // Optionally save the diagram (unchanged) to ensure lifecycle compliance
                diagram.Save("validated_output.vsdx", SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }