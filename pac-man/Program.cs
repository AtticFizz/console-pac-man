using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pac_man
{
    public class Program
    {
        // Starting positions:
        // red (blinky) - (9, 8)
        // cyan (inky) - (8, 10)
        // pink (pinky) - (9, 10)
        // orange (clyde) - (10, 10)
        // pac-man - (9, 16)

        // Other positions:
        // ghost exit - (9, 9)
        // fruit - (9, 12)

        private static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;

            Task.Factory.StartNew(() =>
            {
                ConsoleKey key;

                while ((key = Console.ReadKey().Key) != ConsoleKey.Q)
                {
                    JoyStick.SetDirection(Game.Input(key));
                }

                Game.Exit = true;
            });

            Game.Loop();

            Console.ReadKey();
        }

        
    }
}
