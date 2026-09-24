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

                // Input Visio file path (adjust as needed)
                string inputPath = "input.vsdx";

                // Output thumbnail paths
                string beforePath = "thumbnail_before.png";
                string afterPath = "thumbnail_after.png";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // -----------------------------------------------------------------
                // Render thumbnail before applying any theme
                // -----------------------------------------------------------------
                ImageSaveOptions beforeOptions = new ImageSaveOptions(SaveFileFormat.Png);
                beforeOptions.PageIndex = 0;               // first page
                beforeOptions.PageCount = 1;               // only one page
                beforeOptions.ExportHiddenPage = false;    // ignore hidden pages
                diagram.Save(beforePath, beforeOptions);

                // -----------------------------------------------------------------
                // Apply a preset theme to the first page
                // -----------------------------------------------------------------
                Page firstPage = diagram.Pages[0];
                firstPage.PresetTheme = PresetThemeValue.Bubble;
                firstPage.PresetThemeVariant = PresetThemeVariantValue.Variant1;

                // -----------------------------------------------------------------
                // Render thumbnail after applying the theme
                // -----------------------------------------------------------------
                ImageSaveOptions afterOptions = new ImageSaveOptions(SaveFileFormat.Png);
                afterOptions.PageIndex = 0;
                afterOptions.PageCount = 1;
                afterOptions.ExportHiddenPage = false;
                diagram.Save(afterPath, afterOptions);

                // -----------------------------------------------------------------
                // Compare the two thumbnail files byte‑by‑byte
                // -----------------------------------------------------------------
                byte[] beforeBytes = File.ReadAllBytes(beforePath);
                byte[] afterBytes = File.ReadAllBytes(afterPath);

                bool areEqual = beforeBytes.Length == afterBytes.Length;
                if (areEqual)
                {
                    for (int i = 0; i < beforeBytes.Length; i++)
                    {
                        if (beforeBytes[i] != afterBytes[i])
                        {
                            areEqual = false;
                            break;
                        }
                    }
                }

                if (areEqual)
                {
                    Console.WriteLine("The shape appearance did not change after applying the theme.");
                }
                else
                {
                    Console.WriteLine("The shape appearance changed after applying the theme.");
                    Console.WriteLine($"Before thumbnail saved to: {Path.GetFullPath(beforePath)}");
                    Console.WriteLine($"After thumbnail saved to: {Path.GetFullPath(afterPath)}");
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }