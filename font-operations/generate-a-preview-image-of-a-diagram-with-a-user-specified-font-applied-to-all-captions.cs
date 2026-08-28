using System;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        static void Main()
        {
            // Prompt user for input Visio file path
            Console.Write("Enter the path to the Visio file: ");
            string inputPath = Console.ReadLine();

            // Prompt user for output image file path
            Console.Write("Enter the desired output image path (e.g., preview.png): ");
            string outputPath = Console.ReadLine();

            // Prompt user for the font name to apply to all captions
            Console.Write("Enter the font name to apply to all captions: ");
            string fontName = Console.ReadLine();

            // Validate that the requested font is installed on the system
            InstalledFontCollection fontCollection = new InstalledFontCollection();
            bool fontExists = fontCollection.Families.Any(f => string.Equals(f.Name, fontName, StringComparison.OrdinalIgnoreCase));

            if (!fontExists)
            {
                throw new Exception($"The font \"{fontName}\" is not installed on this system.");
            }

            // Set the default font for the diagram rendering engine
            FontConfigs.DefaultFontName = fontName;

            // Load the diagram
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through all pages and shapes to apply the font to text captions
                foreach (Page page in diagram.Pages)
                {
                    foreach (Shape shape in page.Shapes)
                    {
                        // Check if the shape contains text
                        if (shape.Text != null && !string.IsNullOrEmpty(shape.Text.Value.Text))
                        {
                            // Ensure there is at least one character formatting entry
                            if (shape.Chars.Count == 0)
                            {
                                Aspose.Diagram.Char newChar = new Aspose.Diagram.Char();
                                newChar.IX = 0;
                                shape.Chars.Add(newChar);
                            }

                            // Apply the specified font to all character runs
                            foreach (Aspose.Diagram.Char ch in shape.Chars)
                            {
                                ch.FontName.Value = fontName;
                            }
                        }
                    }
                }

                // Configure image save options for PNG preview
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                // Export the first page (index 0) as a preview image
                saveOptions.PageIndex = 0;

                // Save the preview image
                diagram.Save(outputPath, saveOptions);
            }

            Console.WriteLine("Preview image generated successfully.");
        }
    }