using System;
using Aspose.Diagram;

class Program
    {
        static void Main(string[] args)
        {
            // Expect three arguments: input file, output file, maximum allowed page count
            if (args.Length < 3)
            {
                Console.WriteLine("Usage: DiagramGuidePlugin <inputPath> <outputPath> <maxPageCount>");
                return;
            }

            string inputPath = args[0];
            string outputPath = args[1];
            if (!int.TryParse(args[2], out int maxPageCount))
            {
                Console.WriteLine("Invalid maxPageCount argument.");
                return;
            }

            // Load the diagram
            Diagram diagram = new Diagram(inputPath);

            // Check page count
            int pageCount = diagram.Pages.Count;
            if (pageCount > maxPageCount)
            {
                // Ensure at least one window exists; create a default drawing window if needed
                if (diagram.Windows.Count == 0)
                {
                    Window defaultWindow = new Window();
                    defaultWindow.WindowType = WindowTypeValue.Drawing;
                    diagram.Windows.Add(defaultWindow);
                }

                // Set ShowGuides to false for all windows
                foreach (Window win in diagram.Windows)
                {
                    win.ShowGuides = BOOL.False;
                }
            }

            // Save the modified diagram
            diagram.Save(outputPath, SaveFileFormat.Vsdx);
        }
    }