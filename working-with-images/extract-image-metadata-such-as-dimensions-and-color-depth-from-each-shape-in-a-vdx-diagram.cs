using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Drawing;

class Program
    {
        static void Main(string[] args)
        {
            // Get the VDX file path from command line or prompt the user
            string filePath;
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
            {
                filePath = args[0];
            }
            else
            {
                Console.Write("Enter the path to the VDX diagram file: ");
                filePath = Console.ReadLine();
            }

            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                Console.WriteLine("Invalid file path.");
                return;
            }

            // Load the diagram using the VDX format
            Diagram diagram = new Diagram(filePath, LoadFileFormat.Vdx);

            // Iterate through each page in the diagram
            foreach (Aspose.Diagram.Page page in diagram.Pages)
            {
                // Iterate through each shape on the current page
                foreach (Aspose.Diagram.Shape shape in page.Shapes)
                {
                    // Identify image shapes (foreign objects)
                    if (shape.Type == TypeValue.Foreign && shape.ForeignData != null && shape.ForeignData.Value != null)
                    {
                        // Retrieve shape dimensions (in inches)
                        double widthInches = shape.XForm.Width.Value;
                        double heightInches = shape.XForm.Height.Value;

                        // Extract raw image bytes
                        byte[] imageBytes = shape.ForeignData.Value;

                        // Load the image using Aspose.Drawing to obtain pixel format information
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        using (Aspose.Drawing.Image img = Aspose.Drawing.Image.FromStream(ms))
                        {
                            // Get color depth (bits per pixel)
                            int bitsPerPixel = Aspose.Drawing.Image.GetPixelFormatSize(img.PixelFormat);

                            // Output the metadata
                            Console.WriteLine($"Page: {page.Name} | Shape ID: {shape.ID}");
                            Console.WriteLine($"  Dimensions: {widthInches:F2} in (W) x {heightInches:F2} in (H)");
                            Console.WriteLine($"  Color Depth: {bitsPerPixel} bits per pixel");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }