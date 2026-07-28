using System;
using Aspose.Diagram;

class Program
    {
        static void Main()
        {
            // Create a new Diagram instance without loading any file.
            using (Diagram diagram = new Diagram())
            {
                // Create a new Page instance using the default constructor.
                Page testPage = new Page();

                // Add the page to the diagram's Pages collection.
                // The Pages collection implements IList, so we can use Add.
                diagram.Pages.Add(testPage);

                // Prepare AutoSpaceOptions with custom spacing values (in inches).
                AutoSpaceOptions spacingOptions = new AutoSpaceOptions
                {
                    DistanceInHorizontal = 0.5, // 0.5 inches horizontal spacing
                    DistanceInVertical = 0.5    // 0.5 inches vertical spacing
                };

                // Invoke the AutoSpaceShapes method on the page.
                // Here we use the page's Shapes collection (currently empty) to keep the test isolated.
                testPage.AutoSpaceShapes(testPage.Shapes, spacingOptions);

                // Optionally, verify that the method completed without exceptions.
                Console.WriteLine("AutoSpaceShapes executed successfully on mock page.");
            }
        }
    }