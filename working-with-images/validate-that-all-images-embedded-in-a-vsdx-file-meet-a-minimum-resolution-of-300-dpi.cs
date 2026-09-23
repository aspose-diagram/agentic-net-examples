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

                // Minimum DPI requirement
                const float MinDpi = 300f;

                // Flag to track validation result
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
                            // Load the image from the foreign data byte array
                            using (MemoryStream ms = new MemoryStream(shape.ForeignData.Value))
                            using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                            {
                                float horizDpi = img.HorizontalResolution;
                                float vertDpi = img.VerticalResolution;

                                if (horizDpi < MinDpi || vertDpi < MinDpi)
                                {
                                    allImagesValid = false;
                                    Console.WriteLine($"Image in shape ID {shape.ID} on page {page.Name} has insufficient resolution: {horizDpi}x{vertDpi} DPI.");
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