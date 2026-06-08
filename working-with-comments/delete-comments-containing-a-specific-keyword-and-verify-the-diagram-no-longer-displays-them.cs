using System;
using System.IO;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        string inputPath = "input.vsdx";
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        string outputPath = "output.vsdx";
        string keyword = "TODO";

        try
        {
            Diagram diagram = new Diagram(inputPath);

            foreach (Page page in diagram.Pages)
            {
                for (int i = page.PageSheet.Annotations.Count - 1; i >= 0; i--)
                {
                    Annotation annotation = page.PageSheet.Annotations[i];
                    string commentText = annotation.Comment.Value ?? string.Empty;

                    if (commentText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        page.PageSheet.Annotations.Remove(annotation);
                    }
                }
            }

            foreach (Page page in diagram.Pages)
            {
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    string commentText = annotation.Comment.Value ?? string.Empty;
                    if (commentText.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception($"Comment with keyword '{keyword}' still exists after deletion.");
                    }
                }
            }

            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine("Comments containing the keyword have been removed and the diagram saved successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}