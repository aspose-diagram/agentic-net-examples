using System;
using Aspose.Diagram;

class Program
    {
        // Mock translation method – replace with actual service call as needed
        static string TranslateText(string text)
        {
            // Example: prepend a marker to indicate translation
            return "[Translated] " + text;
        }

        static void Main(string[] args)
        {
            // Validate arguments
            if (args.Length < 2)
            {
                Console.WriteLine("Usage: VisioCommentTranslator <inputVisioPath> <outputVisioPath>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];

            try
            {
                // Load the Visio diagram
                Diagram diagram = new Diagram(inputPath);

                // Iterate through all pages
                foreach (Page page in diagram.Pages)
                {
                    // Access the annotations collection on the page sheet
                    foreach (Annotation annotation in page.PageSheet.Annotations)
                    {
                        // Retrieve the original comment text
                        string originalComment = annotation.Comment.Value;

                        // Translate the comment text
                        string translatedComment = TranslateText(originalComment);

                        // Update the annotation with the translated text
                        annotation.Comment.Value = translatedComment;

                        // Optional: log the update
                        Console.WriteLine($"Page '{page.Name}' - ShapeID {annotation.ShapeID}: \"{originalComment}\" => \"{translatedComment}\"");
                    }
                }

                // Save the updated diagram (preserve original format)
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
                Console.WriteLine("Diagram saved successfully to: " + outputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw;
            }
        }
    }