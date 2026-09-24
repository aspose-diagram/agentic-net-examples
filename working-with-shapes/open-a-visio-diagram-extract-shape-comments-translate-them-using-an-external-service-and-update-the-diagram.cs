using System;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

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
                        // Retrieve the shape associated with the comment
                        int shapeId = annotation.ShapeID;
                        Shape shape = page.Shapes.GetShape(shapeId);

                        // Get the original comment text
                        string originalComment = annotation.Comment.Value;

                        // Translate the comment text (placeholder implementation)
                        string translatedComment = Translate(originalComment);

                        // Update the comment with the translated text
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

        // Placeholder translation method.
        // Replace this with a real call to an external translation service if needed.
        static string Translate(string text)
        {
            // Example: append a suffix to indicate translation.
            return text + " (translated)";
        }
    }