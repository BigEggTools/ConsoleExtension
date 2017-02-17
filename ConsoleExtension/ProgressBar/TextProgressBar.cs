namespace BigEgg.Tools.ConsoleExtension.ProgressBar
{
    using System;

    /// <summary>
    /// Helper class for drawing the progress bar.
    /// </summary>
    public class TextProgressBar
    {
        /// <summary>
        /// Draws the progress bar with specified progress.
        /// </summary>
        /// <param name="progress">The progress.</param>
        /// <param name="total">The total.</param>
        public static void Draw(int progress, int total)
        {
            Console.CursorLeft = 0;
            Console.Write("["); //start
            Console.CursorLeft = 32;
            Console.Write("]"); //end
            Console.CursorLeft = 1;

            int grayLength = total == 0 ? 0 : (int)((progress * 1.0 / total) * 30.0) + 1;

            //draw filled part
            for (int i = 1; i <= grayLength; i++)
            {
                Console.BackgroundColor = ConsoleColor.Gray;
                Console.CursorLeft = i;
                Console.Write(" ");
            }

            //draw unfilled part
            for (int i = grayLength + 1; i <= 31; i++)
            {
                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.CursorLeft = i;
                Console.Write(" ");
            }

            //draw totals
            Console.CursorLeft = 35;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Write(progress.ToString() + " of " + total.ToString() + "    "); //blanks at the end remove any excess

            if (progress == total)
            {
                Console.WriteLine();
            }
        }
    }
}
