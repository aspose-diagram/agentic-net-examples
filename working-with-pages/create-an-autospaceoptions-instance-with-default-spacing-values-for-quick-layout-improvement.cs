using System;
using System.IO;
using Aspose.Diagram;

class AutoSpaceExample
{
    static void Main()
    {
        // Create an AutoSpaceOptions instance using the default constructor.
        // The default values are 0.375 inches for both horizontal and vertical spacing.
        AutoSpaceOptions options = new AutoSpaceOptions();

        // Access the default spacing values (optional, for verification).
        double horizontalSpacing = options.DistanceInHorizontal; // 0.375 inches by default
        double verticalSpacing = options.DistanceInVertical;     // 0.375 inches by default

        // The 'options' object can now be passed to methods such as Page.AutoSpaceShapes
        // to improve layout spacing automatically.
    }
}
