using System;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using Aspose.Diagram;
using Aspose.Diagram.Saving;

class Program
{
    static void Main(string[] args)
    {
        // Validate input arguments
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: Program <input-diagram-path>");
            return;
        }

        string diagramPath = args[0];

        // Guard: ensure the diagram file exists
        if (!File.Exists(diagramPath))
        {
            Console.Error.WriteLine($"Error: Diagram file not found at '{diagramPath}'.");
            return;
        }

        // Define temporary paths for HTML output and the generated email file
        string tempHtmlPath = Path.Combine(Path.GetTempPath(), "diagram.html");
        string emailOutputDir = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedEmails");
        string emailFilePath = Path.Combine(emailOutputDir, "DiagramEmail.eml");

        try
        {
            // Load the Visio diagram inside a using block as required
            using (Diagram diagram = new Diagram(diagramPath))
            {
                // Export the entire diagram to a single HTML file
                HTMLSaveOptions htmlOptions = new HTMLSaveOptions();
                diagram.Save(tempHtmlPath, htmlOptions);
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during diagram conversion: {ex.Message}");
            return;
        }

        // Read the generated HTML content
        string htmlContent;
        try
        {
            htmlContent = File.ReadAllText(tempHtmlPath);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error reading generated HTML file: {ex.Message}");
            return;
        }

        // Prepare the email message with HTML body
        MailMessage message = new MailMessage();
        try
        {
            // Set basic email fields (replace with actual addresses as needed)
            message.From = new MailAddress("sender@example.com");
            message.To.Add(new MailAddress("recipient@example.com"));
            message.Subject = "Visio Diagram as HTML";

            // Create an alternate view for HTML content
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlContent, null, MediaTypeNames.Text.Html);
            message.AlternateViews.Add(htmlView);
            message.IsBodyHtml = true;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error constructing email message: {ex.Message}");
            return;
        }

        // Ensure the output directory exists
        try
        {
            if (!Directory.Exists(emailOutputDir))
                Directory.CreateDirectory(emailOutputDir);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error creating email output directory: {ex.Message}");
            return;
        }

        // Use SmtpClient with SpecifiedPickupDirectory to generate a .eml file (no actual sending)
        try
        {
            using (SmtpClient client = new SmtpClient())
            {
                client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                client.PickupDirectoryLocation = emailOutputDir;
                client.Send(message);
            }

            Console.WriteLine($"Email with embedded HTML saved to: {emailFilePath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error generating email file: {ex.Message}");
        }
        finally
        {
            // Clean up temporary HTML file
            try
            {
                if (File.Exists(tempHtmlPath))
                    File.Delete(tempHtmlPath);
            }
            catch
            {
                // Suppress any cleanup errors
            }
        }
    }
}