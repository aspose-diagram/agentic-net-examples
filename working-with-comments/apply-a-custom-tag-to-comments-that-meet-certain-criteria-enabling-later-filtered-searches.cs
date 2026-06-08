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

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the annotations collection via the PageSheet
                    var annotations = page.PageSheet.Annotations;

                    // Process each annotation (comment)
                    foreach (Annotation annotation in annotations)
                    {
                        // Retrieve the current comment text
                        string currentText = annotation.Comment.Value ?? string.Empty;

                        // Example criteria: comment contains the word "TODO"
                        if (currentText.Contains("TODO", StringComparison.OrdinalIgnoreCase))
                        {
                            // Apply a custom tag by prefixing the comment text
                            string taggedText = "[Review] " + currentText;
                            annotation.Comment.Value = taggedText;
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