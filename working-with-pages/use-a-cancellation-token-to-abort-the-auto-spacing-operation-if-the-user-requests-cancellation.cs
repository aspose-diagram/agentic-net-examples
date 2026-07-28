using System;
using System.Threading;
using Aspose.Diagram;
using Aspose.Diagram.AutoLayout;

class Program
    {
        static void Main(string[] args)
        {
            try
            {

                // Path to the source Visio file
                string inputPath = "input.vsdx";
                // Path for the output Visio file after auto‑spacing
                string outputPath = "output.vsdx";

                // Load the diagram using the standard constructor (lifecycle rule)
                Diagram diagram = new Diagram(inputPath);

                // Assume we work with the first page of the diagram
                Page page = diagram.Pages[0];

                // Create a cancellation token source that can be triggered by the user
                using CancellationTokenSource cts = new CancellationTokenSource();

                // Start a background task that listens for user input to cancel the operation
                Thread cancelThread = new Thread(() =>
                {
                    Console.WriteLine("Press 'c' then Enter to cancel auto‑spacing...");
                    string? line = Console.ReadLine();
                    if (!string.IsNullOrEmpty(line) && line.Equals("c", StringComparison.OrdinalIgnoreCase))
                    {
                        cts.Cancel();
                    }
                });
                cancelThread.IsBackground = true;
                cancelThread.Start();

                try
                {
                    // Perform auto‑spacing with cancellation support
                    AutoSpacePage(page, cts.Token);
                    // Save the modified diagram (lifecycle rule)
                    diagram.Save(outputPath, SaveFileFormat.Vsdx);
                    Console.WriteLine("Auto‑spacing completed and diagram saved.");
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Auto‑spacing was cancelled by the user.");
                }
                finally
                {
                    // Ensure the cancellation thread ends
                    if (cancelThread.IsAlive)
                    {
                        cancelThread.Join();
                    }
                }

            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.Error.WriteLine($"[FileNotFoundException] {ex.Message}");
            }
    }

        /// <summary>
        /// Applies auto‑spacing to the specified page, aborting if cancellation is requested.
        /// </summary>
        /// <param name="page">The page to auto‑space.</param>
        /// <param name="cancellationToken">Token to monitor for cancellation.</param>
        static void AutoSpacePage(Page page, CancellationToken cancellationToken)
        {
            // Check for cancellation before starting the operation
            cancellationToken.ThrowIfCancellationRequested();

            // Configure auto‑spacing options (distance in inches)
            AutoSpaceOptions options = new AutoSpaceOptions
            {
                DistanceInHorizontal = 0.5, // 0.5 inches horizontally
                DistanceInVertical = 0.5    // 0.5 inches vertically
            };

            // Perform the auto‑spacing
            page.AutoSpaceShapes(page.Shapes, options);

            // Check for cancellation after the operation (in case it was long-running)
            cancellationToken.ThrowIfCancellationRequested();
        }
    }