using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            try
            {

                // Input and output file paths
                string inputPath = "input.vsdx";
                string outputPath = "output.vsdx";

                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through each page in the diagram
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each annotation (comment) on the page
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the original comment text
                        string originalComment = annotation.Comment.Value;

                        // Translate the comment using an external service (placeholder implementation)
                        string translatedComment = Translate(originalComment);

                        // Update the annotation with the translated text
                        annotation.Comment.Value = translatedComment;
                    }
                }

                // Save the updated diagram
                diagram.Save(outputPath, SaveFileFormat.Vsdx);

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        // Placeholder translation method – replace with actual service call as needed
        static string Translate(string text)
        {
            // Simulate translation by appending a suffix
            return text + " (translated)";
        }
    }