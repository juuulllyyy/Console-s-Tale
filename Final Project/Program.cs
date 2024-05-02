using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Linq.Expressions;

namespace Final_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(125, 40);
            // Instantations
            Shower show = new Shower();
            Dialogue dia = new Dialogue();
            Game game = new Game();
            Randomizer rnd = new Randomizer();
            // Media files
            SoundPlayer dragS = new SoundPlayer("8 bit dragon.wav");
            SoundPlayer enemS = new SoundPlayer("8 bit fight.wav");
            SoundPlayer theme = new SoundPlayer("8 bit theme.wav");
            SoundPlayer explode = new SoundPlayer("8 bit explosion.wav");
            SoundPlayer wander = new SoundPlayer("8 bit wander.wav");

            // Asks name of the Player
            theme.PlayLooping();// music
            Title();
            Knight();
            Hero hero = new Hero(show.NameAsker(), 100); // Hero Instantation
        error1:
        again1:
            Console.Clear();
            Title();
            // Asks the Player what to do
            Knight();
            theme.Load();
            hero.Hp = 100;
            int c = show.Choice(hero.Name);
            if (c == 0) { goto error1; }
            do
            {
                switch (c)
                {
                    case 1:// New game
                        // Skip Intro
                        if (show.IntroSkip() == "y") { goto skip; }
                        Console.Clear();
                        // Game Intro
                        Castle();
                        dia.Welcome(hero.Name);
                        dia.Instructions(hero.Name);
                        Console.ReadKey();
                    skip:
                    wander:
                        // Player Wandering in the Outskirts
                        wander.PlayLooping(); // music
                        Console.Clear();
                        Scenery();
                        dia.Wander(hero.Name);
                        int chance = rnd.randomizer(); // Searching for an enemy
                        explode.Play();
                        show.HeroSense(hero.Name);
                        chance = 70;
                        // Encounter Chance
                        if (chance <= 10) // DRAGON
                        {
                            // Instantation of new Dragon
                            dragS.PlayLooping();
                            Dragon dragon = new Dragon(100);
                            dragon.Encounter();
                        D:
                            //Show HP
                            Console.Clear();
                            Dragon();
                            dragon.ShowHP(dragon.HP, Separator());
                            dragon.Move();
                            Direction();
                            hero.ShowHeroHP(Separator(), hero.Name, hero.Hp);

                            switch (show.Do())
                            {
                                case 1: // ATTACK
                                    // hero attack and ragon tries to evade
                                    if (rnd.randomizer() <= 15) { dragon.Evade(hero.Name); goto Skip2; }
                                    else { dragon.HP = dragon.RecieveDamage(hero.Hit()); }

                                    // GAME WON
                                    if (dragon.HP == 0)
                                    {
                                        explode.Play();
                                        dragon.Slay();
                                        Console.Clear();
                                        Castle();
                                        game.GameWon(hero.Name);
                                        goto again1;
                                    }
                                Skip2:
                                    // dragon attack
                                    hero.Hp = hero.RecieveDamage(dragon.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    else { goto D; }

                                case 2: // HEAL

                                    //hero heal
                                    hero.Hp = hero.Heal();
                                    // dragon attack
                                    hero.Hp = hero.RecieveDamage(dragon.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    goto D;

                                case 3: // EVADE

                                    // dragon attacks and hero tries to evade

                                    hero.Hp = hero.Evade(rnd.randomizer(), dragon.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    goto D;

                                case 4: // RETREAT

                                    if (show.Retreat() == "y") { goto again1; }
                                    else { goto D; }
                                default:
                                    goto D;
                            }
                        }
                        else if (chance > 10 && chance <= 40) // SPIDER
                        {
                            // Instantation of new Spider
                            enemS.PlayLooping();
                            Spider spdr = new Spider(25);
                            spdr.Encounter();
                        S:
                            //Show Hp
                            Console.Clear();
                            Spider();
                            spdr.ShowHP(spdr.HP, Separator());
                            spdr.Move();
                            Direction();
                            hero.ShowHeroHP(Separator(), hero.Name, hero.Hp);

                            switch (show.Do())
                            {
                                case 1: // ATTACK

                                    //hero attack
                                    spdr.HP = spdr.RecieveDamage(hero.Hit());
                                    if (spdr.HP == 0) { explode.Play(); spdr.Slay(); enemS.Stop(); goto wander; }
                                    // spider attack
                                    hero.Hp = hero.RecieveDamage(spdr.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }

                                    else { goto S; }

                                case 2: // HEAL

                                    // hero heals
                                    hero.Hp = hero.Heal();
                                    // Spider attack
                                    hero.Hp = hero.RecieveDamage(spdr.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    goto S;

                                case 3: // EVADE

                                    // Spider attacks and hero tries to evade
                                    hero.Hp = hero.Evade(rnd.randomizer(), spdr.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    goto S;

                                case 4: // RETREAT

                                    if (show.Retreat() == "y") { goto again1; }
                                    else { goto S; }
                                default:
                                    goto S;
                            }
                        }
                        else if (chance > 40 && chance <= 70) // BAT
                        {
                            // Instantation of new Bat
                            enemS.PlayLooping();
                            Bat bat = new Bat(20);
                            bat.Encounter();
                        B:
                            //Show HP
                            Console.Clear();
                            Bat();
                            bat.ShowHP(bat.HP, Separator());
                            bat.Move();
                            Direction();
                            hero.ShowHeroHP(Separator(), hero.Name, hero.Hp);

                            switch (show.Do())
                            {
                                case 1: // ATTACK

                                    // hero attack
                                    bat.HP = bat.RecieveDamage(hero.Hit());
                                    if (bat.HP == 0) { explode.Play(); bat.Slay(); enemS.Stop(); goto wander; }
                                    // bat attack
                                    hero.Hp = hero.RecieveDamage(bat.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    else { goto B; }

                                case 2: // HEAL

                                    // hero heal
                                    hero.Hp = hero.Heal();
                                    // bat attack
                                    hero.Hp = hero.RecieveDamage(bat.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    goto B;

                                case 3: // EVADE

                                    // dragon attacks and hero tries to evade
                                    hero.Hp = hero.Evade(rnd.randomizer(), bat.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    goto B;

                                case 4: // RETREAT

                                    if (show.Retreat() == "y") { goto again1; }
                                    else { goto B; }
                                default:
                                    goto B;
                            }
                        }
                        else // WOLF
                        {
                            // Instantation of new Wolf
                            enemS.PlayLooping();
                            Wolf wolf = new Wolf(30);
                            wolf.Encounter();
                        W:
                            //Show HP
                            Console.Clear();
                            Wolf();
                            wolf.ShowHP(wolf.HP, Separator());
                            wolf.Move();
                            Direction();
                            hero.ShowHeroHP(Separator(), hero.Name, hero.Hp);

                            switch (show.Do())
                            {
                                case 1: // ATTACK

                                    // hero attack
                                    wolf.HP = wolf.RecieveDamage(hero.Hit());
                                    if (wolf.HP == 0) { explode.Play(); wolf.Slay(); enemS.Stop(); goto wander; }
                                    // wolf attack
                                    hero.Hp = hero.RecieveDamage(wolf.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    else { goto W; }

                                case 2: // HEAL

                                    // hero heal
                                    hero.Hp = hero.Heal();
                                    //wolf attack
                                    hero.Hp = hero.RecieveDamage(wolf.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    goto W;

                                case 3: // EVADE

                                    // wolf attacks and hero tries to evade
                                    hero.Hp = hero.Evade(rnd.randomizer(), wolf.Hit());
                                    if (hero.Hp == 0) { game.GameOver(); goto again1; }
                                    goto W;

                                case 4: // RETREAT

                                    if (show.Retreat() == "y") { goto again1; }
                                    else { goto W; }

                                default:
                                    goto W;
                            }
                        }


                    case 2: // Enemy dex
                        show.EnemyScroll();
                        goto again1;
                    case 3: // EXIT
                        show.Exit(hero.Name);
                        break;

                    default:
                        goto again1;
                }
            } while (c == 1);
        }
        static void Title()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            string a = "============================================================================================================================";
            Console.SetCursorPosition((Console.WindowWidth - a.Length) / 2, Console.CursorTop);
            Console.WriteLine(a);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(10, 1); Console.WriteLine(" ██████╗ ██████╗ ███╗   ██╗███████╗ ██████╗ ██╗     ███████╗███████╗    ████████╗ █████╗ ██╗     ███████╗");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(10, 2); Console.WriteLine("██╔════╝██╔═══██╗████╗  ██║██╔════╝██╔═══██╗██║     ██╔════╝██╔════╝    ╚══██╔══╝██╔══██╗██║     ██╔════╝");
            Console.SetCursorPosition(10, 3); Console.WriteLine("██║     ██║   ██║██╔██╗ ██║███████╗██║   ██║██║     █████╗  ███████╗       ██║   ███████║██║     █████╗  ");
            Console.SetCursorPosition(10, 4); Console.WriteLine("██║     ██║   ██║██║╚██╗██║╚════██║██║   ██║██║     ██╔══╝  ╚════██║       ██║   ██╔══██║██║     ██╔══╝  ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(10, 5); Console.WriteLine("╚██████╗╚██████╔╝██║ ╚████║███████║╚██████╔╝███████╗███████╗███████║       ██║   ██║  ██║███████╗███████╗");
            Console.SetCursorPosition(10, 6); Console.WriteLine(" ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝╚══════╝ ╚═════╝ ╚══════╝╚══════╝╚══════╝       ╚═╝   ╚═╝  ╚═╝╚══════╝╚══════╝");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("============================================================================================================================");
            Console.ForegroundColor = ConsoleColor.White;

        }
        static void Wolf()
        {
            string s = "     / ,    .        .     |         ";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                        ,     ,      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                        |\\---/|     ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                       /  , , |      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                  __.-'|  / \\ /     ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("         __ ___.-'        ._O|       ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("      .-'  '        :      _/        ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("     / ,    .        .     |         ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("    :  ;    :        :   _/          ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("    |  |   .'     __:   /            ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("    |  :   /'----'| \\  |            ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("    \\  |\\  |      | /| |           ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("     '.'| /       || \\ |            ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("     | /|.'       '.l \\_            ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("     || ||             '-'           ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("     '-''-'");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Bat()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(45, Console.CursorTop); Console.WriteLine("    =/\\                 /\\=");
            Console.SetCursorPosition(45, Console.CursorTop); Console.WriteLine("    / \\'._   (\\_/)   _.'/ \\");
            Console.SetCursorPosition(45, Console.CursorTop); Console.WriteLine("   / .''._'--(o.o)--'_.''. \\");
            Console.SetCursorPosition(45, Console.CursorTop); Console.WriteLine("  /.' _/ |`'=/ \" \\='`| \\_ `.\\");
            Console.SetCursorPosition(45, Console.CursorTop); Console.WriteLine(" /` .' `\\;-,'\\___/',-;/` '. '\\");
            Console.SetCursorPosition(45, Console.CursorTop); Console.WriteLine("/.-'       `\\(-V-)/`       `-.\\");
            Console.SetCursorPosition(45, Console.CursorTop); Console.WriteLine("`            \"   \"            `");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Spider()
        {
            string s = "              (                  ";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("              (                  ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("               )                 ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("              (                  ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        /\\  .-\"\"\"-.  /\\     ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("       //\\\\/  ,,,  \\//\\\\    ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("       |/\\| ,;;;;;, |/\\|       ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("       //\\\\\\;-\"\"\"-;///\\\\ ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("      //  \\/   .   \\/  \\\\    ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("     (| ,-_| \\ | / |_-, |)      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("       //`__\\.-.-./__`\\\\      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("      // /.-(() ())-.\\ \\\\     ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("     (\\ |)   '---'   (| /)      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("      ` (|           |) `        ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        \\)           (/         ");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Dragon()
        {
            string s = "      <>=======()                                    ";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("      <>=======()                                    ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("     (/\\___   /|\\\\          ()==========<>_       ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("           \\_/ | \\\\        //|\\   ______/ \\)    ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("             \\_|  \\\\      // | \\_/               ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("               \\|\\/|\\_   //  /\\/                 ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                (oo)\\ \\_//  /                      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("               //_/\\_\\/ /  |                       ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("              @@/  |=\\  \\  |                       ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                   \\_=\\_ \\ |                      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                     \\==\\ \\|\\_                   ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                  __(\\===\\(  )\\                   ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                 (((~) __(_/   |                     ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                      (((~) \\  /                    ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                      ______/ /                      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("                      '------'                       ");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Scenery()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("                      _");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("                     /#\\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("                    /###\\     /\\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("                   /  ###\\   /##\\  /\\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("                  /      #\\ /####\\/##\\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("                 /  /      /   # /  ##\\             _       /\\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("               // //  /\\  /    _/  /  #\\ _         /#\\    _/##\\    /\\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("              // /   /  \\     /   /    #\\ \\      _/###\\_ /   ##\\__/ _\\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("             /  \\   / .. \\   / /   _   { \\ \\   _/       / //    /    \\\\");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("     /\\     /    /\\  ...  \\_/   / / \\   } \\ | /  /\\  \\ /  _    /  /    \\ /\\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("  _ /  \\  /// / .\\  ..%:.  /... /\\ . \\ {:  \\\\   /. \\     / \\  /   ___   /  \\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine(" /.\\ .\\.\\// \\/... \\.::::..... _/..\\ ..\\:|:. .  / .. \\\\  /.. \\    /...\\ /  \\ \\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("/...\\.../..:.\\. ..:::::::..:..... . ...\\{:... / %... \\\\/..%. \\  /./:..\\__   \\");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine(" .:..\\:..:::....:::;;;;;;::::::::.:::::.\\}.....::%.:. \\ .:::. \\/.%:::.:..\\");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine("::::...:::;;:::::;;;;;;;;;;;;;;:::::;;::{:::::::;;;:..  .:;:... ::;;::::..");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine(";;;;:::;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;];;;;;;;;;;::::::;;;;:.::;;;;;;;;:..");
            Console.SetCursorPosition(20, Console.CursorTop); Console.WriteLine(";;;;;;;;;;;;;;ii;;;;;;;;;;;;;;;;;;;;;;;;[;;;;;;;;;;;;;;;;;;;;;;:;;;;;;;;;;;;;");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("===========================================================================================================================");
        }
        static string Separator()
        {
            string sep = "============================================================================================================================";
            Console.ForegroundColor = ConsoleColor.White;
            return sep;
        }
        static void Castle()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(30, 2); Console.WriteLine("                           o                    ");
            Console.SetCursorPosition(30, 3); Console.WriteLine("                       _---|         _ _ _ _ _ ");
            Console.SetCursorPosition(30, 4); Console.WriteLine("                    o   ---|     o   ]-I-I-I-[ ");
            Console.SetCursorPosition(30, 5); Console.WriteLine("   _ _ _ _ _ _  _---|      | _---|    \\ ` ' / ");
            Console.SetCursorPosition(30, 6); Console.WriteLine("   ]-I-I-I-I-[   ---|      |  ---|    |.   | ");
            Console.SetCursorPosition(30, 7); Console.WriteLine("    \\ `   '_/       |     / \\    |    | /^\\| ");
            Console.SetCursorPosition(30, 8); Console.WriteLine("     [*]  __|       ^    / ^ \\   ^    | |*|| ");
            Console.SetCursorPosition(30, 9); Console.WriteLine("     |__   ,|      / \\  /    `\\ / \\   | ===| ");
            Console.SetCursorPosition(30, 10); Console.WriteLine("  ___| ___ ,|__   /    /=_=_=_=\\   \\  |,  _|");
            Console.SetCursorPosition(30, 11); Console.WriteLine("  I_I__I_I__I_I  (====(_________)___|_|____|____");
            Console.SetCursorPosition(30, 12); Console.WriteLine("  \\-\\--|-|--/-/  |     I  [ ]__I I_I__|____I_I_| ");
            Console.SetCursorPosition(30, 13); Console.WriteLine("   |[]      '|   | []  |`__  . [  \\-\\--|-|--/-/  ");
            Console.SetCursorPosition(30, 14); Console.WriteLine("   |.   | |' |___|_____I___|___I___|---------| ");
            Console.SetCursorPosition(30, 15); Console.WriteLine("  / \\| []   .|_|-|_|-|-|_|-|_|-|_|-| []   [] | ");
            Console.SetCursorPosition(30, 16); Console.WriteLine(" <===>  |   .|-=-=-=-=-=-=-=-=-=-=-|   |    / \\  ");
            Console.SetCursorPosition(30, 17); Console.WriteLine(" ] []|`   [] ||.|.|.|.|.|.|.|.|.|.||-      <===> ");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(30, 18); Console.WriteLine(" ] []| ` |   |/////////\\\\\\\\\\.||__.  | |[] [ ");
            Console.SetCursorPosition(30, 19); Console.WriteLine(" <===>     ' ||||| |   |   | ||||.||  []   <===>");
            Console.SetCursorPosition(30, 20); Console.WriteLine("  \\T/  | |-- ||||| | O | O | ||||.|| . |'   \\T/ ");
            Console.SetCursorPosition(30, 21); Console.WriteLine("   |      . _||||| |   |   | ||||.|| |     | |");
            Console.SetCursorPosition(30, 22); Console.WriteLine("../|' v . | .|||||/____|____\\|||| /|. . | . ./");
            Console.SetCursorPosition(30, 23); Console.WriteLine(".|//\\............/...........\\........../../\\\\\\");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("===========================================================================================================================\n");
            Console.ForegroundColor = ConsoleColor.White;
        }
        static void Direction()
        {
            string a = "(1) Attack   ||  (2) Heal    ||  (3) Evade   || (4)  Retreat";
            string b = "Evade      =     Has a chance to evade the attack of the enemy and heal 10 hp  ";

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("============================================================================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(22, Console.CursorTop); Console.WriteLine("Attack     =     Attacks the enemy reducing their HP                           ");
            Console.SetCursorPosition(22, Console.CursorTop); Console.WriteLine("Heal       =     Heals the Adventurer for 25 HP                                ");
            Console.SetCursorPosition(22, Console.CursorTop); Console.WriteLine(b);
            Console.SetCursorPosition(22, Console.CursorTop); Console.WriteLine("Retreat    =     Returns back to the castle healing the adventurers HP fully   ");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("============================================================================================================================");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("============================================================================================================================");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition((Console.WindowWidth - a.Length) / 2, Console.CursorTop); Console.WriteLine(a);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("============================================================================================================================");
        }
        static void Knight()
        {
            string s = "  / )                    ";
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("  / )                    ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("  | |                    ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("  | |                    ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("  |.|                    ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("  |:|      __            ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine(",_|:|_,   /  )           ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("  (Oo    / _I_           ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("   +\\ \\  || __|        ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("      \\ \\||___|        ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        \\ /.:.\\-\\     ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("         |.:. /-----\\   ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("         |___|::oOo::|   ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("         /   |:<_T_>:|   ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        |_____\\ ::: /   ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("         | |  \\ \\:/    ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("         | |   | |       ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("         \\ /   | \\___  ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("=========/ |===\\_____\\=====");
        }
    }
    interface Attack_and_Recieve
    {
        int Hit();
        int RecieveDamage(int dam);
    }
    abstract class Enemy
    {
        private int hp;
        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        public Enemy(int hp)
        {
            this.hp = hp;
        }
        public abstract void Move();
        public abstract void Encounter();
        public abstract void Slay();
        public abstract void ShowHP(int hp, string sep);
    }
    class Spider : Enemy, Attack_and_Recieve
    {
        public Spider(int hp) : base(hp) { }
        //Attack
        public int Hit()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("The spider attacked using bite...");
            System.Threading.Thread.Sleep(1000);
            Random random = new Random();
            int rnd = random.Next(9, 12);
            Console.WriteLine("The spider dealt {0} damage...", rnd);
            System.Threading.Thread.Sleep(1000);
            return rnd;
        }
        // Recieve Damage
        public int RecieveDamage(int dam)
        {
            if (HP <= dam)
            {
                HP = 0;
                return HP;
            }
            else { return HP - dam; }
        }
        // Movement
        public override void Move()
        {
            string s = "The spider crawled...";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine("The spider crawled...");
            Console.ForegroundColor = ConsoleColor.White;
        }
        // Encounter
        public override void Encounter()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("A spider jumps out from a rock..");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to engage");
            Console.ReadKey();
        }
        // Slain
        public override void Slay()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The Spider has been slain!...");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to continue");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
        // ShowHP
        public override void ShowHP(int hp, string sep)
        {
            string s = "SPIDER HP = [{0}]";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(sep);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine("SPIDER HP = [{0}]", hp);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(sep);
        }
    }
    class Wolf : Enemy, Attack_and_Recieve
    {
        public Wolf(int hp) : base(hp) { }
        // Attack
        public int Hit()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("The wolf attacked using claw scratch...");
            System.Threading.Thread.Sleep(1000);
            Random random = new Random();
            int rnd = random.Next(12, 15);
            Console.WriteLine("The wolf dealt {0} damage...", rnd);
            System.Threading.Thread.Sleep(1000);
            return rnd;
        }
        // Recieve Damage
        public int RecieveDamage(int dam)
        {
            if (HP <= dam)
            {
                HP = 0;
                return HP;
            }
            else { return HP - dam; }
        }
        // Movement
        public override void Move()
        {
            string s = "The Wolf howled...";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine("The Wolf howled...");
            Console.ForegroundColor = ConsoleColor.White;
        }
        //Encounter
        public override void Encounter()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("A wolf jumps out of the bushes...");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to engage");
            Console.ReadKey();
        }
        //Slain
        public override void Slay()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The wolf has been slain!...");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to continue");
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
        }
        // ShowHP
        public override void ShowHP(int hp, string sep)
        {

            string s = "WOLF HP = [{0}]";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(sep);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine("WOLF HP = [{0}]", hp);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(sep);
        }
    }
    class Bat : Enemy, Attack_and_Recieve
    {
        public Bat(int hp) : base(hp) { }
        //Attack
        public int Hit()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("The bat attacked using screech...");
            System.Threading.Thread.Sleep(1000);
            Random random = new Random();
            int rnd = random.Next(5, 8);
            Console.WriteLine("The Bat dealt {0} damage...", rnd);
            System.Threading.Thread.Sleep(1000);
            return rnd;
        }
        //Recieve Damage
        public int RecieveDamage(int dam)
        {
            if (HP <= dam)
            {
                HP = 0;
                return HP;
            }
            else { return HP - dam; }
        }
        // Movement
        public override void Move()
        {
            string s = "The Bat has flown outside of the cave...";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine("The Bat has flown outside of the cave...");
            Console.ForegroundColor = ConsoleColor.White;
        }
        // Encounter
        public override void Encounter()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("A bat glared at you...");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to engage");
            Console.ReadKey();
        }
        // Slain
        public override void Slay()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The bat has been slain!...");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;
        }
        // ShowHP
        public override void ShowHP(int hp, string sep)
        {
            string s = "BAT HP = [{0}]";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(sep);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine("BAT HP = [{0}]", hp);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(sep);
        }
    }
    class Dragon : Enemy, Attack_and_Recieve
    {
        public Dragon(int hp) : base(hp) { }
        //Attack
        public int Hit()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("The dragon attacked using fire breath...");
            System.Threading.Thread.Sleep(1000);
            Random random = new Random();
            int rnd = random.Next(19, 28);
            Console.WriteLine("The elder dragon dealt {0} damage...", rnd);
            System.Threading.Thread.Sleep(1000);
            return rnd;
        }
        //Recieve Damage
        public int RecieveDamage(int dam)
        {
            if (HP <= dam)
            {
                HP = 0;
                return HP;
            }
            else { return HP - dam; }
        }
        // Movement
        public override void Move()
        {
            string s = "The Dragon roared...";
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(s);
            Console.ForegroundColor = ConsoleColor.White;
        }
        // Encounter
        public override void Encounter()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("The Elder Dragon landed in front of you!!!!!...");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to engage");
            Console.ReadKey();
        }
        //Slain
        public override void Slay()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The Elder Dragon has been slain!...");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.ForegroundColor = ConsoleColor.White;
        }
        // ShowHP
        public override void ShowHP(int hp, string sep)
        {
            string s = "ELDER DRAGON HP = [{0}]";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(sep);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine("ELDER DRAGON HP = [{0}]", hp);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(sep);
        }
        // EVADE
        public void Evade(string name)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} attacked using the Claymore...", name);
            System.Threading.Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dragon evaded the attack");
            System.Threading.Thread.Sleep(1000);
        }
    }

    class Hero : Attack_and_Recieve
    {
        private string name;
        private int hp;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Hp
        {
            get { return hp; }
            set { hp = value; }
        }
        public Hero(string name, int hp)
        {
            this.name = name;
            this.hp = hp;
        }
        // Deal damage
        public int Hit()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} attacked using the Claymore...", name);
            System.Threading.Thread.Sleep(1000);
            Random random = new Random();
            int rnd = random.Next(15, 23);
            Console.WriteLine(name + " dealt {0} damage...", rnd);
            System.Threading.Thread.Sleep(1000);
            return rnd;
        }
        // Recieve damage
        public int RecieveDamage(int dam)
        {
            if (hp <= dam)
            {
                hp = 0;
                return hp;
            }
            else { return Hp - dam; }
        }
        // Evasion
        public int Evade(int eva, int dam)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Adventurer tried to evade the attack...");
            System.Threading.Thread.Sleep(1000);
            if (eva > 40)
            {
                Console.WriteLine("Adventurer was hit!");
                System.Threading.Thread.Sleep(1000);
                if (hp <= dam)
                {
                    hp = 0;
                    return hp;
                }
                else { return hp - dam; }
            }
            else
            {
                Console.WriteLine("Adventurer evaded the attack and healed 10 hp!");
                System.Threading.Thread.Sleep(1000);
                if (hp >= 90) { return hp = 100; }
                else { return hp + 10; }
            }
        }
        // Heal
        public int Heal()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0} drank a potion...", name);
            System.Threading.Thread.Sleep(1000);
            Console.WriteLine("{0} gained 25 hp", name);
            System.Threading.Thread.Sleep(1000);
            if (hp >= 75) { return hp = 100; }
            else { return hp + 25; }
        }
        // Show HP
        public void ShowHeroHP(string sep, string n, int hp)
        {
            string s = n + " HP = [{00}]";
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(sep);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop);
            Console.WriteLine(n + " HP = [{0}]", hp);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine(sep);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Dialogue
    {
        public void Welcome(string name)
        {
            string wel = "Greetings adventurer " + name + "! Welcome to Consoldor! The land of the Elder Dragon...";
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (char st in wel)
            {
                Console.Write(st);
                System.Threading.Thread.Sleep(10);
            }
            Console.WriteLine();
        }
        public void Instructions(string name)
        {
            string ins1 = "Our kingdom has been ravaged by the wrath of the Elder Dragon for centuries...";
            string ins2 = "You must journey the outskirts to search and slay the Elder Dragon...";
            string ins3 = "You may encounter creatures that may injure you along the way but you can retreat back to the kingdom for restoration..";
            string ins4 = "Good luck Adventurer " + name + "! You are our only hope...";
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (char st1 in ins1)
            {
                Console.Write(st1);
                System.Threading.Thread.Sleep(10);

            }
            Console.WriteLine();
            foreach (char st2 in ins2)
            {
                Console.Write(st2);
                System.Threading.Thread.Sleep(10);
            }
            Console.WriteLine();
            foreach (char st3 in ins3)
            {
                Console.Write(st3);
                System.Threading.Thread.Sleep(10);
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine();
            foreach (char st4 in ins4)
            {
                Console.Write(st4);
                System.Threading.Thread.Sleep(10);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nPress any key to continue...");
        }
        public void Wander(string name)
        {
            Console.Write("Press any key to start wandering...");
            Console.ReadKey();
            string wander = "\nAdventurer " + name + " is wandering in the outskirts";
            string dots = "...";
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (char st1 in wander)
            {
                Console.Write(st1);
                System.Threading.Thread.Sleep(40);
            }
            foreach (char st2 in dots)
            {
                Console.Write(st2);
                System.Threading.Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }
    class Game
    {
        public void GameOver()
        {
            SoundPlayer theme = new SoundPlayer("8 bit theme.wav");
            SoundPlayer gameover = new SoundPlayer("8 bit game over.wav");
            Console.Clear();
            gameover.Play();
            string s = "        _.---,._,'                                           ";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        _.---,._,'                                           ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("       /' _.--.<                                             ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("         /'     `'                                           ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("       /' _.---._____                                        ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("       \\.'   ___, .-'`                                      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("           /'    \\             .                            ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("         /'       `-.          -|-                           ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        |                       |                            ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        |                   .-'~~~`-.                        ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        |                 .'         `.                      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        |                 |  R  I  P  |                      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        |                 |           |                      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("        |                 |           |                      ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("         \\              \\\\|           |//                 ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("   ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("============================================================================================================================");
            string go = "\nRest now adventurer... You will forever be known as the bravest hero in another world...";
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (char st in go)
            {
                Console.Write(st);
                System.Threading.Thread.Sleep(40);
            }
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\nPress any key to resurrect..");
            Console.ReadKey();
            theme.PlayLooping();
        }
        public void GameWon(string name)
        {
            SoundPlayer wonGame = new SoundPlayer("8 bit won.wav");
            wonGame.Play();
            string won = "The Kingdom of Consoldor is grateful for your service thy good Adventurer!!...\n";
            string won2 = "You shall be known as " + name + " the Dragon Slayer!!\n";
            string but = "But....your journey has just begun....... H..E..L..P..U..";
            Console.ForegroundColor = ConsoleColor.Cyan;
            foreach (char st1 in won)
            {
                Console.Write(st1);
                System.Threading.Thread.Sleep(30);
            }
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (char st2 in won2)
            {
                Console.Write(st2);
                System.Threading.Thread.Sleep(30);
            }
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            foreach (char st3 in but)
            {
                Console.Write(st3);
                System.Threading.Thread.Sleep(100);
            }
            SoundPlayer theme = new SoundPlayer("8 bit theme.wav");
            theme.Play();
        }
    }
    class Randomizer
    {
        public int randomizer()
        {
            Random r = new Random();
            int rnd = r.Next(1, 100);
            return rnd;
        }
    }
    class Shower
    {
        public string NameAsker()
        {
            string s = "| Hello Adventurer! What might be your name? |";
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("==============================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.Write("  Hello Adventurer! What might be your name?");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop); Console.Write(" |");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("|");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("==============================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.Write("Enter Name: ");
            Console.ForegroundColor = ConsoleColor.Red;
            string n = Console.ReadLine();
            return n;
        }
        public int Choice(string n)
        {
            try
            {
                Console.WriteLine();
                string s = "| What would you like to do Adventurer " + n + "? |";
                string s1 = "=================================================================================";
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.Write(" What would you like to do Adventurer ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(n);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("? ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.SetCursorPosition((Console.WindowWidth - s1.Length) / 2, Console.CursorTop); Console.WriteLine("=================================================================================");
                Console.SetCursorPosition((Console.WindowWidth - s1.Length) / 2, Console.CursorTop); Console.WriteLine("(1) New Journey     ||      (2) Creature Scroll     ||      (3) Leave the Kingdom");
                Console.SetCursorPosition((Console.WindowWidth - s1.Length) / 2, Console.CursorTop); Console.WriteLine("=================================================================================");
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.Write("Enter Choice: ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop); int c = int.Parse(Console.ReadLine());
                return c;
            }
            catch { return 0; }
        }
        public void EnemyScroll()
        {
        again:
            Console.Clear();
            string s = "===============================================================================================";
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("===============================================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("Enemy          ||  Damage           ||   Health Points  ||  Special Abilities                  ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("===============================================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("Bat            ||  5 - 8   damage   ||       20 hp      ||  None                               ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("Spider         ||  9 - 12  damage   ||       25 hp      ||  None                               ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("Wolf           ||  12 - 15 damage   ||       25 hp      ||  None                               ");
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("Elder Dragon   ||  19 - 28 damage   ||       25 hp      ||  Has a 10% chance to evade an attack");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine("===============================================================================================");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.Write("Close the Scroll? yes (y) || no (any key) || ");
            Console.ForegroundColor = ConsoleColor.Red;
            string c = Console.ReadLine();
            if (c == "y" || c == "Y") { return; }
            else { goto again; }
        }
        public string IntroSkip()
        {
            string s = "Skip Intro? y || any key || ";
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.WriteLine();
            Console.SetCursorPosition((Console.WindowWidth - s.Length) / 2, Console.CursorTop); Console.Write("Skip Intro? y || any key || ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop); string skip = Console.ReadLine();
            return skip;
        }
        public void HeroSense(string n)
        {
            Console.WriteLine("{0} sensed something...", n);
            System.Threading.Thread.Sleep(1500);
        }
        public int Do()
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("What would you like to do adventurer? || ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                int ch = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.White;
                return ch;
            }
            catch { return 0; }

        }
        public string Retreat()
        {
            SoundPlayer theme = new SoundPlayer("8 bit theme.wav");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Are you sure you want to retreat back to the kingdom? y || anykey || ");
            string ch = Console.ReadLine();
            theme.PlayLooping();
            return ch;
        }

        public void Exit(string n)
        {
            Console.WriteLine();
            Console.WriteLine("{0} is leaving Consoldor", n);
            System.Threading.Thread.Sleep(1500);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("{0} left the Kingdom to die...", n);
            Environment.Exit(0);
        }
    }
}
