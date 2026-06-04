using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
    {
        static void Main()
        {
            try
            {

                // Path to the source Visio diagram file
                const string diagramPath = "input.vsdx";

                // Load the diagram from file
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    // Export the diagram to HTML stored in a memory stream
                    using (MemoryStream htmlStream = new MemoryStream())
                    {
                        HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                        diagram.Save(htmlStream, htmlOptions);

                        // Read the HTML content as a string
                        htmlStream.Position = 0;
                        string htmlBody = new StreamReader(htmlStream, Encoding.UTF8).ReadToEnd();

                        // Create the email message
                        MailMessage mail = new MailMessage
                        {
                            From = new MailAddress("sender@example.com"),
                            Subject = "Visio Diagram as HTML",
                            IsBodyHtml = true
                        };
                        mail.To.Add("recipient@example.com");

                        // Set the HTML body
                        mail.Body = htmlBody;

                        // Optional: add a plain‑text alternative view
                        AlternateView plainView = AlternateView.CreateAlternateViewFromString(
                            "Please view this email in an HTML‑compatible client to see the diagram.",
                            Encoding.UTF8,
                            MediaTypeNames.Text.Plain);
                        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(
                            htmlBody,
                            Encoding.UTF8,
                            MediaTypeNames.Text.Html);
                        mail.AlternateViews.Add(plainView);
                        mail.AlternateViews.Add(htmlView);

                        // Send the email (configure SMTP settings as needed)
                        using (SmtpClient smtp = new SmtpClient("smtp.example.com"))
                        {
                            // smtp.Credentials = new System.Net.NetworkCredential("user", "password");
                            // smtp.EnableSsl = true;
                            smtp.Send(mail);
                        }
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }