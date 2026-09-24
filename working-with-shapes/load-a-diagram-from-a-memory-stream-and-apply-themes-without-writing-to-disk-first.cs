using System;
using System.IO;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Assume diagramBytes contains the raw bytes of a Visio file (e.g., VSDX).
                // In a real scenario these bytes could come from a database, network stream, etc.
                byte[] diagramBytes = GetDiagramBytes();

                // Load the diagram from a memory stream.
                using (MemoryStream inputStream = new MemoryStream(diagramBytes))
                {
                    Diagram diagram = new Diagram(inputStream);

                    // Apply a preset theme to every page in the diagram.
                    foreach (Page page in diagram.Pages)
                    {
                        // Set the theme and a variant. Both properties are write‑only.
                        page.PresetTheme = PresetThemeValue.Bubble;
                        page.PresetThemeVariant = PresetThemeVariantValue.Variant1;
                    }

                    // Save the themed diagram back to a memory stream (no disk I/O).
                    using (MemoryStream outputStream = new MemoryStream())
                    {
                        diagram.Save(outputStream, SaveFileFormat.Vsdx);

                        // At this point outputStream contains the themed diagram.
                        // It can be returned, sent over a network, or written to a file later.
                        Console.WriteLine($"Themed diagram saved to memory stream ({outputStream.Length} bytes).");
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Placeholder method to obtain diagram bytes.
        // Replace with actual logic to retrieve the Visio file content.
        static byte[] GetDiagramBytes()
        {
            // For demonstration, read from a file path.
            // In production, replace this with the appropriate source.
            const string path = "input.vsdx";
            if (!File.Exists(path))
                throw new FileNotFoundException($"Diagram file not found: {path}");

            return File.ReadAllBytes(path);
        }
    }