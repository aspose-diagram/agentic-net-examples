using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Determine the input file path.
                // If a command‑line argument is provided, use it; otherwise use a default placeholder.
                string diagramPath = args.Length > 0 ? args[0] : "input.vsdx";

                // Load the Visio diagram. The Diagram class implements IDisposable, so use a using block.
                using (Diagram diagram = new Diagram(diagramPath))
                {
                    bool anyMismatch = false;

                    // Iterate over all pages explicitly typing the loop variable.
                    foreach (Aspose.Diagram.Page page in diagram.Pages)
                    {
                        // Access the ScaleX value from the page's PrintProps.
                        double scaleX = page.PageSheet.PrintProps.ScaleX.Value;

                        // Check if ScaleX differs from the default 1.0.
                        if (scaleX != 1.0)
                        {
                            anyMismatch = true;
                            // Output page identification information.
                            // Prefer the page name; fall back to the numeric ID if the name is empty.
                            string pageIdentifier = !string.IsNullOrEmpty(page.Name) ? page.Name : $"ID={page.ID}";
                            Console.WriteLine($"Page '{pageIdentifier}' has ScaleX = {scaleX}");
                        }
                    }

                    if (!anyMismatch)
                    {
                        Console.WriteLine("All pages have ScaleX equal to 1.0.");
                    }
                }

            }
            catch (Aspose.Diagram.DiagramException ex)
            {
                Console.Error.WriteLine($"[DiagramException] {ex.Message}");
            }
    }
    }