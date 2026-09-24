using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            // Determine input and output file paths.
            string inputPath;
            string outputPath;

            if (args.Length >= 2)
            {
                inputPath = args[0];
                outputPath = args[1];
            }
            else
            {
                Console.WriteLine("Enter the full path to the Visio file to process:");
                inputPath = Console.ReadLine();

                Console.WriteLine("Enter the full path for the updated Visio file to save:");
                outputPath = Console.ReadLine();
            }

            // Load the Visio diagram.
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages.
            foreach (Page page in diagram.Pages)
            {
                // Iterate through all shapes on the current page.
                foreach (Shape shape in page.Shapes)
                {
                    // Skip deleted shapes.
                    if (shape.Del == BOOL.True)
                        continue;

                    // Determine if the shape already has at least one hyperlink.
                    bool hasHyperlink = shape.Hyperlinks != null && shape.Hyperlinks.Count > 0;

                    if (!hasHyperlink)
                    {
                        // Build a default URL based on the shape's universal name.
                        // If NameU is empty, fall back to the shape's ID.
                        string baseName = !string.IsNullOrWhiteSpace(shape.NameU) ? shape.NameU : shape.ID.ToString();
                        string defaultUrl = $"https://example.com/{baseName}";

                        // Create and configure a new hyperlink.
                        Hyperlink link = new Hyperlink();
                        link.Name = "DefaultLink";
                        link.Address.Value = defaultUrl;
                        link.Description.Value = $"Auto‑generated link for shape '{baseName}'";

                        // Add the hyperlink to the shape.
                        shape.Hyperlinks.Add(link);
                    }
                }
            }

            // Save the modified diagram.
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

            Console.WriteLine($"Processing complete. Updated file saved to: {outputPath}");
        }
    }