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
                string filePath = "input.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(filePath, LoadFileFormat.Vsdx);

                bool allImagesValid = true;

                // Iterate through all pages and shapes
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Identify embedded images (foreign shapes)
                        if (shape.Type == TypeValue.Foreign)
                        {
                            // Retrieve the raw image bytes
                            byte[] imageData = shape.ForeignData.Value;

                            if (imageData == null || imageData.Length == 0)
                            {
                                Console.WriteLine($"Shape ID {shape.ID} has no image data.");
                                allImagesValid = false;
                                continue;
                            }

                            // Load the image using Aspose.Drawing
                            using (MemoryStream ms = new MemoryStream(imageData))
                            using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                            {
                                float hDpi = img.HorizontalResolution;
                                float vDpi = img.VerticalResolution;

                                if (hDpi < 300f || vDpi < 300f)
                                {
                                    Console.WriteLine($"Shape ID {shape.ID} fails DPI check: {hDpi}x{vDpi} DPI.");
                                    allImagesValid = false;
                                }
                            }
                        }
                    }
                }

                if (allImagesValid)
                {
                    Console.WriteLine("All embedded images meet the minimum 300 DPI requirement.");
                }
                else
                {
                    throw new Exception("One or more embedded images do not meet the minimum 300 DPI requirement.");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }