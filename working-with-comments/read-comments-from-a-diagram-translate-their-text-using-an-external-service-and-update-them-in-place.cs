using System.IO;
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
                // Iterate through each comment (annotation) on the page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Retrieve the original comment text
                    string originalText = annotation.Comment.Value;

                    // Translate the text using an external service (placeholder implementation)
                    string translatedText = TranslateText(originalText);

                    // Update the comment with the translated text
                    annotation.Comment.Value = translatedText;
                }
            }

            // Save the updated diagram back to a file
            diagram.Save(outputPath, SaveFileFormat.Vsdx);

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }

    // Placeholder translation method – replace with actual external service call as needed
    static string TranslateText(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return text;

        // Example: prepend a marker to indicate translation
        return "[Translated] " + text;
    }
}
