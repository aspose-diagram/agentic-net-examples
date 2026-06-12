using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";

                // Paths for the before/after thumbnails
                string beforeThumbnail = "shape_before.png";
                string afterThumbnail = "shape_after.png";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Get the first non‑deleted shape on the first page
                Page page = diagram.Pages[0];
                Shape targetShape = null;
                foreach (Shape shape in page.Shapes)
                {
                    if (shape.Del == BOOL.False)
                    {
                        targetShape = shape;
                        break;
                    }
                }

                if (targetShape == null)
                    throw new Exception("No visible shape found on the first page.");

                // Render and save the thumbnail before applying a theme
                ImageSaveOptions imgOptions = new ImageSaveOptions(SaveFileFormat.Png);
                targetShape.ToImage(beforeThumbnail, imgOptions);

                // Apply a preset theme to the page (you can change the theme as needed)
                page.PresetTheme = PresetThemeValue.Bubble;
                page.PresetThemeVariant = PresetThemeVariantValue.Variant2;

                // Render and save the thumbnail after applying the theme
                targetShape.ToImage(afterThumbnail, imgOptions);

                // Output the result locations
                Console.WriteLine("Thumbnails generated:");
                Console.WriteLine($"Before theme: {Path.GetFullPath(beforeThumbnail)}");
                Console.WriteLine($"After theme: {Path.GetFullPath(afterThumbnail)}");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }