using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect two arguments: input Visio file path and output Visio file path.
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioHyperlinkUpdater <input.vsdx> <output.vsdx>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and shapes.
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Ensure the Hyperlinks collection exists.
                    if (shape.Hyperlinks == null || shape.Hyperlinks.Count == 0)
                    {
                        // Build a default URL based on the shape's universal name.
                        // If NameU is empty, fall back to the shape's ID.
                        string shapeIdentifier = !string.IsNullOrWhiteSpace(shape.NameU) ? shape.NameU : shape.ID.ToString();
                        string defaultUrl = $"https://example.com/{shapeIdentifier}";

                        // Create and configure a new hyperlink.
                        Hyperlink link = new Hyperlink();
                        link.Name = "DefaultLink";
                        link.Address.Value = defaultUrl;
                        link.Description.Value = "Automatically added default hyperlink";

                        // Add the hyperlink to the shape.
                        shape.Hyperlinks.Add(link);

                        Console.WriteLine($"Added hyperlink to shape '{shapeIdentifier}' on page '{page.NameU}'.");
                    }
                }
            }

            // Save the updated diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved to '{outputPath}'.");
        }
    }