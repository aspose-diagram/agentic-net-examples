using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Input and output file paths (adjust as needed)
                string inputPath = "input.vsdx";
                string outputPath = "output_tagged.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Define the keyword to search for and the tag to apply
                string keyword = "TODO";
                string tag = "[Reviewed] ";

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the annotations (comments) on the page
                    var annotations = page.PageSheet.Annotations;

                    // Iterate over each annotation
                    foreach (Annotation annotation in annotations)
                    {
                        // Get the current comment text
                        string currentText = annotation.Comment.Value ?? string.Empty;

                        // Check if the comment meets the criteria (contains the keyword)
                        if (currentText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply the custom tag if it's not already present
                            if (!currentText.StartsWith(tag, StringComparison.Ordinal))
                            {
                                annotation.Comment.Value = tag + currentText;
                            }
                        }
                    }
                }

                // Save the modified diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }