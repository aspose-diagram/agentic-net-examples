using System;
using System.IO;
using System.Linq;
using Aspose.Diagram;
using Aspose.Diagram.Saving;
using Aspose.Drawing.Text;

class Program
    {
        static void Main()
        {
            try
            {
                // Prompt user for input diagram path, output image path and desired font name
                Console.Write("Enter the path to the Visio diagram file: ");
                string diagramPath = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(diagramPath) || !File.Exists(diagramPath))
                {
                    throw new Exception("Diagram file not found.");
                }

                Console.Write("Enter the desired font name (e.g., Arial): ");
                string fontName = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(fontName))
                {
                    throw new Exception("Font name cannot be empty.");
                }

                Console.Write("Enter the output image file path (PNG will be used): ");
                string outputPath = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(outputPath))
                {
                    throw new Exception("Output path cannot be empty.");
                }

                // Verify that the requested font is installed on the system using Aspose.Drawing.Text
                InstalledFontCollection fontCollection = new InstalledFontCollection();
                bool fontExists = fontCollection.Families.Any(f => f.Name.Equals(fontName, StringComparison.OrdinalIgnoreCase));
                if (!fontExists)
                {
                    throw new Exception($"The font \"{fontName}\" is not installed on this system.");
                }

                // Configure Aspose.Diagram to use the system font folder (required before rendering)
                string systemFontFolder = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
                FontConfigs.SetFontFolder(systemFontFolder, true);
                FontConfigs.DefaultFontName = fontName;

                // Load the diagram
                Diagram diagram = new Diagram(diagramPath);

                // Apply the specified font to all text characters in every shape
                foreach (Page page in diagram.Pages)
                {
                    foreach (Aspose.Diagram.Shape shape in page.Shapes)
                    {
                        // Skip shapes without text
                        if (shape.Text == null || shape.Text.Value.Count == 0)
                            continue;

                        // Apply font to each character formatting run
                        foreach (Aspose.Diagram.Char ch in shape.Chars)
                        {
                            ch.FontName.Value = fontName;
                        }
                    }
                }

                // Prepare image save options (PNG preview of the first page)
                ImageSaveOptions saveOptions = new ImageSaveOptions(SaveFileFormat.Png);
                saveOptions.PageIndex = 0;          // first page
                saveOptions.PageCount = 1;          // only one page
                saveOptions.DefaultFont = fontName; // fallback font for rendering

                // Save the preview image
                diagram.Save(outputPath, saveOptions);

                Console.WriteLine($"Preview image saved successfully to \"{outputPath}\".");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
    }