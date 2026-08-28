using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Expected arguments:
            // args[0] - input Visio file path (e.g., "input.vsdx")
            // args[1] - output Visio file path (e.g., "output.vsdx")
            // args[2] - placeholder shape name to replace (e.g., "PlaceholderImage")
            // args[3] - path to the new PNG image (will be loaded into memory)

            if (args.Length < 4)
            {
                Console.WriteLine("Usage: ReplaceShapeImage <inputVisio> <outputVisio> <placeholderShapeName> <pngImagePath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            string placeholderName = args[2];
            string pngPath = args[3];

            // Load the PNG image into a byte array (memory)
            byte[] pngBytes;
            try
            {
                pngBytes = File.ReadAllBytes(pngPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to read PNG file: {ex.Message}");
                return;
            }

            // Load the Visio diagram
            Diagram diagram;
            try
            {
                diagram = new Diagram(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load Visio file: {ex.Message}");
                return;
            }

            bool replacementMade = false;

            // Iterate through all pages and shapes to find the placeholder
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Identify foreign (image) shapes by TypeValue.Foreign
                    if (shape.Type == TypeValue.Foreign && shape.Name == placeholderName)
                    {
                        // Replace the embedded image data
                        shape.ForeignData.Value = pngBytes;

                        // Optionally adjust the shape size to match the image dimensions.
                        // Here we simply keep the existing size; adjust as needed.
                        replacementMade = true;
                        Console.WriteLine($"Replaced image in shape ID {shape.ID} on page '{page.Name}'.");
                    }
                }
            }

            if (!replacementMade)
            {
                Console.WriteLine($"No shape named '{placeholderName}' with TypeValue.Foreign was found.");
            }

            // Save the modified diagram
            try
            {
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine($"Diagram saved to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save Visio file: {ex.Message}");
            }
        }
    }