using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Load a Visio diagram. Adjust the file path as needed.
                string diagramPath = "input.vsdx";
                Diagram diagram = new Diagram(diagramPath);

                // Ensure there is at least one page.
                if (diagram.Pages.Count == 0)
                {
                    Console.WriteLine("The diagram contains no pages.");
                    return;
                }

                // Get the first page.
                Page page = diagram.Pages[0];

                // Ensure the page has at least one shape.
                if (page.Shapes.Count == 0)
                {
                    Console.WriteLine("The page contains no shapes.");
                    return;
                }

                // Retrieve the first shape on the page.
                Shape shape = page.Shapes.GetShape(0);

                // Check if the shape has any hyperlinks.
                if (shape.Hyperlinks == null || shape.Hyperlinks.Count == 0)
                {
                    Console.WriteLine("The selected shape has no hyperlinks.");
                    return;
                }

                // Iterate through each hyperlink and log its name and target address.
                foreach (Hyperlink link in shape.Hyperlinks)
                {
                    // The Name property is a simple string.
                    string linkName = link.Name ?? "(no name)";

                    // The Address is a cell; its value must be accessed via .Value.
                    string targetAddress = link.Address?.Value ?? "(no address)";

                    Console.WriteLine($"Hyperlink Name: {linkName}");
                    Console.WriteLine($"Target Address: {targetAddress}");
                    Console.WriteLine(); // Blank line for readability.
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }
    }