using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input Visio file, logo image file, output PNG file
            if (args.Length != 3)
            {
                Console.WriteLine("Usage: VisioLogoReplacer <input.vsdx> <logo.png> <output.png>");
                return;
            }

            string inputPath = args[0];
            string logoPath = args[1];
            string outputPath = args[2];

            // Load the Visio diagram
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes to find placeholders
            foreach (Page page in diagram.Pages)
            {
                // Collect shapes to process to avoid modifying collection during iteration
                var shapesToReplace = new System.Collections.Generic.List<Shape>();

                foreach (Shape shape in page.Shapes)
                {
                    // Identify placeholder shapes by name (adjust condition as needed)
                    if (!string.IsNullOrEmpty(shape.NameU) && shape.NameU.Contains("Placeholder"))
                    {
                        shapesToReplace.Add(shape);
                    }
                }

                // Replace each placeholder with the logo image
                foreach (Shape placeholder in shapesToReplace)
                {
                    double pinX = placeholder.XForm.PinX.Value;
                    double pinY = placeholder.XForm.PinY.Value;
                    double width = placeholder.XForm.Width.Value;
                    double height = placeholder.XForm.Height.Value;

                    // Hide the original placeholder shape
                    placeholder.XForm.Width.Value = 0;
                    placeholder.XForm.Height.Value = 0;

                    // Insert the logo image at the same position and size
                    using (FileStream logoStream = new FileStream(logoPath, FileMode.Open, FileAccess.Read))
                    {
                        // AddShape returns the new shape ID (long)
                        page.AddShape(pinX, pinY, width, height, logoStream);
                    }
                }
            }

            // Configure high‑resolution PNG export
            ImageSaveOptions pngOptions = new ImageSaveOptions(SaveFileFormat.Png);
            pngOptions.Resolution = 300f;               // 300 DPI for high quality
            pngOptions.ExportHiddenPage = false;        // Do not export hidden pages

            // Save the modified diagram as PNG
            diagram.Save(outputPath, pngOptions);

            Console.WriteLine($"Diagram saved to '{outputPath}' with high‑resolution PNG.");
        }
    }