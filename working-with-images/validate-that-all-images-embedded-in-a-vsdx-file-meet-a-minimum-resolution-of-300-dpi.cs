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
                string inputPath = "input.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath, LoadFileFormat.Vsdx);

                bool allImagesValid = true;

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through all shapes on the page
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify embedded images (foreign objects)
                        if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                        {
                            byte[] imageData = shape.ForeignData.Value;

                            using (MemoryStream ms = new MemoryStream(imageData))
                            using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                            {
                                // Retrieve image resolution (DPI)
                                float horizDpi = img.HorizontalResolution;
                                float vertDpi = img.VerticalResolution;

                                // Check against the minimum required DPI (300)
                                if (horizDpi < 300f || vertDpi < 300f)
                                {
                                    allImagesValid = false;
                                    Console.WriteLine($"[FAIL] Page '{page.Name}' Shape ID {shape.ID} has low resolution: {horizDpi}x{vertDpi} DPI.");
                                }
                                else
                                {
                                    Console.WriteLine($"[PASS] Page '{page.Name}' Shape ID {shape.ID} resolution: {horizDpi}x{vertDpi} DPI.");
                                }
                            }
                        }
                    }
                }

                if (!allImagesValid)
                {
                    throw new Exception("One or more embedded images do not meet the minimum 300 DPI resolution requirement.");
                }
                else
                {
                    Console.WriteLine("All embedded images meet the minimum 300 DPI resolution requirement.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }