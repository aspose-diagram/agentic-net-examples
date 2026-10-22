using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Input Visio diagram path (first argument or default)
        string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";
        // Output HTML file path
        string htmlPath = "output.html";

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Diagram file not found: {diagramPath}");
            return;
        }

        // Convert Visio diagram to HTML
        try
        {
            // Load the diagram
            Diagram diagram = new Diagram(diagramPath);

            // Set up HTML save options (default PNG images)
            HTMLSaveOptions htmlOptions = new HTMLSaveOptions();

            // Save the diagram as HTML
            diagram.Save(htmlPath, htmlOptions);

            // Release resources
            diagram.Dispose();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error converting diagram to HTML: {ex.Message}");
            return;
        }

        // Guard: ensure the HTML file was created
        if (!File.Exists(htmlPath))
        {
            Console.Error.WriteLine($"HTML file was not created: {htmlPath}");
            return;
        }

        // Read the generated HTML content
        string htmlContent;
        try
        {
            htmlContent = File.ReadAllText(htmlPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading HTML file: {ex.Message}");
            return;
        }

        // Email parameters (subsequent arguments or defaults)
        string smtpHost = args.Length > 1 ? args[1] : "smtp.example.com";
        string fromAddress = args.Length > 2 ? args[2] : "sender@example.com";
        string toAddress = args.Length > 3 ? args[3] : "recipient@example.com";

        // Create the email message with HTML body
        MailMessage mail = new MailMessage();
        try
        {
            mail.From = new MailAddress(fromAddress);
            mail.To.Add(toAddress);
            mail.Subject = "Visio Diagram as HTML";
            mail.Body = htmlContent;
            mail.IsBodyHtml = true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error constructing email message: {ex.Message}");
            return;
        }

        // Send the email using SMTP
        try
        {
            using (SmtpClient client = new SmtpClient(smtpHost))
            {
                // Optional: configure port and credentials if needed
                // client.Port = 587;
                // client.Credentials = new NetworkCredential("username", "password");
                // client.EnableSsl = true;

                client.Send(mail);
                Console.WriteLine("Email sent successfully.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error sending email: {ex.Message}");
        }
    }
}