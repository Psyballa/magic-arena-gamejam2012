using System;

namespace WindowsGame1
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (KingsOfAlchemy game = new KingsOfAlchemy())
            {
                game.Run();
            }
        }
    }
#endif
}

