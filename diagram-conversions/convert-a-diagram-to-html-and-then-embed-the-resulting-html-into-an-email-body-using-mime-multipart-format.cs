using System;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file (uses the provided Diagram constructor)
            using (var diagram = new Diagram("input.vsdx"))
            {
                // StringBuilder to accumulate HTML generated for each shape
                var htmlBuilder = new StringBuilder();

                // HTML save options (uses the provided HTMLSaveOptions class)
                var htmlOptions = new HTMLSaveOptions
                {
                    // Example option: generate a single HTML file per shape (optional)
                    SaveAsSingleFile = true
                };

                // Iterate through all shapes on the active page (uses diagram.ActivePage.Shapes)
                foreach (var shape in diagram.ActivePage.Shapes)
                {
                    // Generate HTML for the current shape into a memory stream
                    using (var ms = new MemoryStream())
                    {
                        shape.ToHTML(ms, htmlOptions); // uses the provided Shape.ToHTML method
                        ms.Position = 0;
                        using (var reader = new StreamReader(ms))
                        {
                            htmlBuilder.AppendLine(reader.ReadToEnd());
                        }
                    }
                }

                // Combined HTML for the whole diagram
                string combinedHtml = htmlBuilder.ToString();

                // Create a MailMessage with a multipart/alternative body (HTML + plain text)
                var mail = new MailMessage
                {
                    From = new MailAddress("sender@example.com"),
                    Subject = "Diagram rendered as HTML"
                };
                mail.To.Add("recipient@example.com");

                // Plain‑text alternative (required for multipart/alternative)
                var plainView = AlternateView.CreateAlternateViewFromString(
                    "Please view this email in an HTML‑capable client.", 
                    Encoding.UTF8, 
                    MediaTypeNames.Text.Plain);
                mail.AlternateViews.Add(plainView);

                // HTML alternative containing the diagram HTML
                var htmlView = AlternateView.CreateAlternateViewFromString(
                    combinedHtml, 
                    Encoding.UTF8, 
                    MediaTypeNames.Text.Html);
                mail.AlternateViews.Add(htmlView);

                // At this point the MailMessage contains a MIME multipart/alternative body
                // with the diagram HTML embedded. Sending can be done via SmtpClient if needed.
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
