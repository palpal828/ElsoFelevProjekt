using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kalapacsvetes_PP_SZH
{
    internal class ProjektHaziPPSzH
    {
        public static Sportolo sportolo;
        static void Main(string[] args)
        {
            List<Sportolo> sportolok = new List<Sportolo>();
            List<double> eredmenyek = new List<double>();
            //beolvassa a fájlt
            string f = File.ReadAllText("./kalapacsvetes.txt");
            double median;
            //felosztja a f változót a ';', '.', '\n' karaktereknél
            string[] g = f.Split(';', '.', '\n');
            //for ciklus ami 6-tól (mert a 6-os számú elem az első statsztika) és 8-asával lép (ami 1 rekordnak felel meg)
            for (int i = 6; i < g.Length; i += 8)
            {
                //a g+1 (ami az eredmény tízedes törtben) átalakítja a ',' karaktert '.'-ra hogy (legalább is nekem) tízedes törtnek érezze
                g[i+1] = g[i+1].Replace(',', '.');
                //a sportoló változót beállítja a g[i] elemekre
                sportolo = new Sportolo(Convert.ToInt32(g[i]), Convert.ToDouble(g[i + 1]), g[i + 2], g[i + 3], g[i + 4], Convert.ToInt32(g[i + 5]), Convert.ToInt32(g[i + 6]), Convert.ToInt32(g[i + 7]));
                //listába jegyzem a sportolókat
                sportolok.Add(sportolo);
                //az eredményeket lejegyezzük külön hogy könnyebben kezeljem később
                eredmenyek.Add(sportolo.eredmeny);
            }
            //könnyebb matekos statisztikák
            Console.WriteLine($"2 tízedes törtre kerekített eredmények átlaga: {Math.Round(eredmenyek.Average(), 2)} \n");
            Console.WriteLine($"A legkisebb eredmény: {eredmenyek.Min()} és ezt {sportolok[eredmenyek.IndexOf(eredmenyek.Min())].nev} érte el \n");
            Console.WriteLine($"A legnagyobb eredmény: {eredmenyek.Max()} és ezt {sportolok[eredmenyek.IndexOf(eredmenyek.Max())].nev} érte el \n");
            //ez egy kis lopott kód a netrők :(, ez a medián természetesen
            if (eredmenyek.Count() % 2 == 0) median = (eredmenyek[eredmenyek.Count() / 2] + eredmenyek[eredmenyek.Count() / 2 - 1]) / 2;
            else median = eredmenyek[eredmenyek.Count() / 2];
            //idáig
            Console.WriteLine($"Az eredmények mediánja pedig: {median} \n");
            Var();
            //szűréses függvény ahol bármelyik adattal találhatunk rekordot, a könnyű szűrés érdekében
            while (true)
            {
                Console.WriteLine("Adj meg valamilyen adatot amin keresztül szűrni szeretnél! (üres = vége!)");
                string szures = Console.ReadLine();
                Console.Clear();
                //ha üres akkor kitörünk a while-ból
                if (szures == "") break;
                string talalatok = "";
                for (int i = 0; i < sportolok.Count(); i++)
                {
                    if (szures == sportolok[i].nev ||
                        szures == Convert.ToString(sportolok[i].helyezes) ||
                        szures == sportolok[i].helyszin  ||
                        szures == sportolok[i].orszagkod ||
                        szures == Convert.ToString(sportolok[i].helyezes))
                    {
                        Console.WriteLine($"Sportoló neve {sportolok[i].nev}, Ő a {sportolok[i].helyezes}. helyezést érte el, {sportolok[i].eredmeny} eredménnyel, {sportolok[i].helyszin} városában, {sportolok[i].orszagkod} az országkódja, {sportolok[i].ev} évében, {sportolok[i].honap} hónapában, {sportolok[i].nap}-ján");
                        talalatok += $"Sportoló neve {sportolok[i].nev}, Ő a {sportolok[i].helyezes}. helyezést érte el, {sportolok[i].eredmeny} eredménnyel, {sportolok[i].helyszin} városában, {sportolok[i].orszagkod} az országkódja, {sportolok[i].ev} évében, {sportolok[i].honap} hónapában, {sportolok[i].nap}-ján\n";
                        
                    }
                        
                }
                File.WriteAllText("./talalatok.txt", talalatok);
            }
            Var();  
            
        }
        //csak kiírja hogy folytassa, és törli a konzolt
        static void Var()
        {
            Console.WriteLine("Enterre folytatódik a program / bezáródik!");
            Console.ReadLine(); 
            Console.Clear();
        }
    }
    //a sportoló osztály minden neki szükséges (vagy ahogy megbírtam oldani) adattal
    internal class Sportolo
    {
        public int helyezes;
        public double eredmeny;
        public string nev;
        public string orszagkod;
        public string helyszin;
        public int ev;      // ˇ
        public int honap;   //-> ez a három összefüggő mert jobban nem bírtam megoldani :(
        public int nap;     // ^

        public Sportolo(int helyezes, double eredmeny, string nev, string orszagkod, string helyszin, int ev, int honap, int nap)
        {
            this.helyezes = helyezes;
            this.eredmeny = eredmeny;
            this.nev = nev;
            this.orszagkod = orszagkod;
            this.helyszin = helyszin;
            this.ev = ev;
            this.honap = honap;
            this.nap = nap;
        }
    }
}
