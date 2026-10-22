using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using Aspose.Diagram;

class Program
{
    static void Main(string[] args)
    {
        // Expect input and output file paths as arguments.
        if (args.Length < 2)
        {
            Console.Error.WriteLine("Usage: Program <inputVisioPath> <outputVisioPath>");
            return;
        }

        string inputPath = args[0];
        string outputPath = args[1];

        // Guard: verify the input file exists.
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Ensure the output directory exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outputDir) && !Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        try
        {
            // Load the Visio diagram inside a using block as required.
            using (Diagram diagram = new Diagram(inputPath))
            {
                // Iterate through each page in the diagram.
                foreach (Page page in diagram.Pages)
                {
                    // Iterate through each annotation (comment) on the page.
                    foreach (Annotation ann in page.PageSheet.Annotations)
                    {
                        // Retrieve the original comment text.
                        string originalComment = ann.Comment.Value;

                        // Translate the comment using an external service.
                        string translatedComment = TranslateText(originalComment);

                        // Update the annotation with the translated text.
                        ann.Comment.Value = translatedComment;
                    }
                }

                // Save the updated diagram to the specified output path.
                diagram.Save(outputPath, SaveFileFormat.Vsdx);
            }
        }
        catch (Exception ex)
        {
            // Write any errors to the error console.
            Console.Error.WriteLine($"Error processing diagram: {ex.Message}");
        }
    }

    /// <summary>
    /// Translates the given text using a free public translation API.
    /// Falls back to the original text if the service fails.
    /// </summary>
    static string TranslateText(string text)
    {
        try
        {
            using (HttpClient client = new HttpClient())
            {
                // Encode the text for URL usage.
                string encodedText = Uri.EscapeDataString(text);
                // Example translation from English to Spanish using MyMemory API.
                string requestUrl = $"https://api.mymemory.translated.net/get?q={encodedText}&langpair=en|es";

                // Synchronously get the response.
                HttpResponseMessage response = client.GetAsync(requestUrl).Result;
                response.EnsureSuccessStatusCode();

                // Read the JSON response.
                string json = response.Content.ReadAsStringAsync().Result;

                // Parse JSON to extract the translated text.
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
            // Log translation errors but continue with original text.
            Console.Error.WriteLine($"Translation failed: {ex.Message}");
        }

        // Return the original text if translation was not successful.
        return text;
    }
}