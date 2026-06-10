using System;
using System.IO;
using System.Text.Json;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

namespace DiagramPageTitleUpdater
{
    // Model representing the expected JSON structure.
    public class PageMetadata
    {
        public string Title { get; set; }
    }

    public class Program
    {
        public static void Main()
        {
            try
            {

                // Paths to the Visio diagram and the JSON metadata file.
                string diagramPath = "input.vsdx";
                string jsonPath = "metadata.json";
                string outputPath = "output.vsdx";

                // Load the JSON metadata.
                if (!File.Exists(jsonPath))
                {
                    throw new FileNotFoundException($"Metadata file not found: {jsonPath}");
                }

                string jsonContent = File.ReadAllText(jsonPath);
                PageMetadata metadata = JsonSerializer.Deserialize<PageMetadata>(jsonContent);
                if (metadata == null || string.IsNullOrWhiteSpace(metadata.Title))
                {
                    throw new InvalidOperationException("Invalid or missing Title in metadata.");
                }

                // Load the Visio diagram.
                if (!File.Exists(diagramPath))
                {
                    throw new FileNotFoundException($"Diagram file not found: {diagramPath}");
                }

                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Update the Name property of each page with the title from metadata.
                    foreach (Page page in diagram.Pages)
                    {
                        page.Name = metadata.Title;
                    }

                    // Save the updated diagram.
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                }

                Console.WriteLine("Diagram page titles updated successfully.");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }
}