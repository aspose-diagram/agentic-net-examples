using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Determine input file path (first argument or default)
        string inputPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Guard: ensure the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Determine output file path (second argument or default)
        string outputPath = args.Length > 1 ? args[1] : "output.vsdx";
        // No need to check existence for output; it will be created/overwritten

        // Load the Visio diagram inside a try/catch to capture Aspose errors
        Diagram diagram;
        try
        {
            // Load diagram from the specified file
            diagram = new Diagram(inputPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error loading diagram: {ex.Message}");
            return;
        }

        // Process each page and its annotations inside a try/catch block
        try
        {
            foreach (Page page in diagram.Pages)
            {
                // Iterate over all annotations (comments) on the page
                foreach (Annotation annotation in page.PageSheet.Annotations)
                {
                    // Retrieve the shape associated with the comment via ShapeID
                    Shape shape = page.Shapes.GetShape(annotation.ShapeID);
                    if (shape == null)
                    {
                        // If the shape cannot be found, skip this annotation
                        continue;
                    }

                    // Extract the original comment text
                    string originalComment = annotation.Comment.Value;
                    if (string.IsNullOrWhiteSpace(originalComment))
                    {
                        // Skip empty comments
                        continue;
                    }

                    // Translate the comment text (e.g., to Spanish)
                    string translatedComment = TranslateText(originalComment, "es");
                    if (translatedComment == null)
                    {
                        // If translation failed, keep the original comment
                        translatedComment = originalComment;
                    }

                    // Update the annotation with the translated text
                    annotation.Comment.Value = translatedComment;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing annotations: {ex.Message}");
            return;
        }

        // Save the updated diagram to the output file
        try
        {
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
            Console.WriteLine($"Diagram saved successfully to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error saving diagram: {ex.Message}");
        }
    }

    // Simple translation using the MyMemory free translation API
    static string TranslateText(string text, string targetLanguage)
    {
        // Build request URL (source language auto-detected)
        string requestUrl = $"https://api.mymemory.translated.net/get?q={Uri.EscapeDataString(text)}&langpair=en|{targetLanguage}";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Synchronously get the response
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;
                response.EnsureSuccessStatusCode();

                // Read response content as string
                string json = response.Content.ReadAsStringAsync().Result;

                // Parse JSON to extract the translated text
                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    JsonElement root = doc.RootElement;
                    if (root.TryGetProperty("responseData", out JsonElement responseData) &&
                        responseData.TryGetProperty("translatedText", out JsonElement translated))
                    {
                        return translated.GetString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log translation errors but do not abort processing
            Console.Error.WriteLine($"Translation error: {ex.Message}");
        }

        // Return null if translation could not be performed
        return null;
    }
}