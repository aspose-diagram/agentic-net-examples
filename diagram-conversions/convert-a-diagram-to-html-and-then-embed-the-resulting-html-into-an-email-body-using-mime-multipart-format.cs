using System;
using System.IO;
using System.Text;
using System.Net.Mail;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class DiagramToEmail
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram from a file
            Diagram diagram = new Diagram("input.vsdx");

            // Prepare a StringBuilder to collect HTML of all shapes
            StringBuilder htmlBuilder = new StringBuilder();

            // HTML save options (single file to simplify concatenation)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions
            {
                SaveAsSingleFile = true
            };

            // Iterate through all pages and their shapes
            foreach (Page page in diagram.Pages)
            {
                foreach (Shape shape in page.Shapes)
                {
                    // Render each shape to HTML using a memory stream
                    using (MemoryStream ms = new MemoryStream())
                    {
                        shape.ToHTML(ms, htmlOptions);
                        ms.Position = 0;
                        using (StreamReader reader = new StreamReader(ms, Encoding.UTF8))
                        {
                            string shapeHtml = reader.ReadToEnd();
                            htmlBuilder.AppendLine(shapeHtml);
                        }
                    }
                }
            }

            // The complete HTML representation of the diagram
            string diagramHtml = htmlBuilder.ToString();

            // Create a mail message with multipart/alternative (plain text + HTML)
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("sender@example.com");
            mail.To.Add("recipient@example.com");
            mail.Subject = "Diagram as HTML";

            // Plain‑text alternative
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(
                "Please view this email in an HTML‑compatible client to see the diagram.",
                Encoding.UTF8,
                "text/plain");

            // HTML alternative containing the diagram HTML
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                diagramHtml,
                Encoding.UTF8,
                "text/html");

            mail.AlternateViews.Add(plainView);
            mail.AlternateViews.Add(htmlView);

            // At this point the MailMessage object contains the MIME multipart body.
            // It can be sent using SmtpClient or saved to a file as needed.
            // Example (commented out):
            // using (SmtpClient client = new SmtpClient("smtp.example.com"))
            // {
            //     client.Send(mail);
            // }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
