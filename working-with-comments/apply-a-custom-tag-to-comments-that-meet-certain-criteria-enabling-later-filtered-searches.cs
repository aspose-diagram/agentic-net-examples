using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio file
                const string inputPath = "input.vsdx";
                // Path for the modified Visio file
                const string outputPath = "output.vsdx";

                // Load the diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Access the annotations (comments) collection via the PageSheet
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Example criteria: comment text contains the word "Review"
                        if (annotation.Comment.Value != null && annotation.Comment.Value.Contains("Review"))
                        {
                            // Apply a custom tag if it hasn't been added already
                            const string tag = "[CustomTag] ";
                            if (!annotation.Comment.Value.StartsWith(tag))
                            {
                                annotation.Comment.Value = tag + annotation.Comment.Value;
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