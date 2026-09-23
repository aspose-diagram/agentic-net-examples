using System.IO;
using System;
using Aspose.Diagram;

class Program
{
    static void Main()
    {
        try
        {

            // Paths to the source and destination Visio files
            string inputPath = "input.vsdx";
            string outputPath = "output.vsdx";

            // Load the diagram from file
            Diagram diagram = new Diagram(inputPath);

            // Iterate through all pages and their annotations (comments)
            foreach (Page page in diagram.Pages)
            {
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Retrieve the original comment text
                    string originalText = annotation.Comment.Value;

                    // Translate the text (placeholder for an external service)
                    string translatedText = TranslateText(originalText);

                    // Update the comment with the translated text
                    annotation.Comment.Value = translatedText;
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

    // Simulated translation method – replace with actual service call as needed
    static string TranslateText(string text)
    {
        return $"[Translated] {text}";
    }
}
