using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Aspose.Diagram;

class DiagramToEmail
{
    static void Main()
    {
        try
        {

            // Load the Visio diagram
            Diagram diagram = new Diagram("input.vsdx");

            // Convert the diagram to HTML and save to a temporary file
            string htmlPath = Path.Combine(Path.GetTempPath(), "diagram.html");
            diagram.Save(htmlPath, SaveFileFormat.Html);

            // Read the generated HTML content
            string htmlBody = File.ReadAllText(htmlPath);

            // Create the email message
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("sender@example.com");
            mail.To.Add("recipient@example.com");
            mail.Subject = "Diagram as HTML";

            // Set the email body as HTML using an alternate view (multipart/alternative)
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, new ContentType(MediaTypeNames.Text.Html));
            mail.AlternateViews.Add(htmlView);
            mail.IsBodyHtml = true; // Ensure the client treats the body as HTML

            // (Optional) Add a plain‑text alternative view
            string plainText = "Please view this email in an HTML‑compatible client to see the diagram.";
            AlternateView plainView = AlternateView.CreateAlternateViewFromString(plainText, new ContentType(MediaTypeNames.Text.Plain));
            mail.AlternateViews.Add(plainView);

            // Send the email (SMTP settings must be configured appropriately)
            using (SmtpClient smtp = new SmtpClient("smtp.example.com", 587))
            {
                smtp.Credentials = new System.Net.NetworkCredential("username", "password");
                smtp.EnableSsl = true;
                smtp.Send(mail);
            }

            // Clean up the temporary HTML file
            if (File.Exists(htmlPath))
            {
                File.Delete(htmlPath);
            }

        }
        catch (System.IO.FileNotFoundException ex)
        {
            Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
        }
    }
}
