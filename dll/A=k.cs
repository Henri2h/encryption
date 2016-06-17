using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crypt_dll_aplication
{
    /// <summary>
    /// This class allow the user to crypt a document using k = number
    /// </summary>
    class A_k
    {

        int a;
        int b;
        int c;
        int d;
        int e;
        int f;
        int g;
        int h;
        int i;
        int j;
        int k;
        int l;
        int m;
        int n;
        int o;
        int p;
        int q;
        int r;
        int s;
        int t;
        int u;
        int v;
        int w;
        int x;
        int y;
        int z;

        // grand
        int A;
        int B;
        int C;
        int D;
        int E;
        int F;
        int G;
        int H;
        int I;
        int J;
        int K;
        int L;
        int M;
        int N;
        int O;
        int P;
        int Q;
        int R;
        int S;
        int T;
        int U;
        int V;
        int W;
        int X;
        int Y;
        int Z;

        // special
        int é = 0;
        int è = 0;
        int à = 0;
        int ù = 0;
        int ê = 0;
        int ç = 0;

        int parentése_open = 0;
        int parentése_closed = 0;


        int ë = 0;
        int virgule = 0;
        int point_virgule = 0;
        int deux_points = 0;
        int point = 0;
        int point_exlamation = 0;
        int point_interrogation = 0;
        int point_de_suspension = 0;
        int arro_base = 0;
        int pour_cent = 0;
        int hachtag = 0;
        int crochet_ouvert = 0;
        int crochet_fermé = 0;
        int plus_petit = 0;
        int plus_grand = 0;
        int egal = 0;
        int guillemet_simple = 0;
        int guillemet_double = 0;
        int guillemet_ligne_ouvert = 0;
        int guillemet_ligne_ferme = 0;
        int guillemet_simple_ouvert = 0;
        int guillemet_simple_ferme = 0;
        int plus = 0;
        int moin = 0;
        int grand_moin = 0;
        int i_trema = 0;
        int â = 0;
        int œ = 0;
        int ô = 0;
        int num = 0;
        int liste_point = 0;
        int î = 0;
        int û = 0;
        int apostrophe = 0;

        // numéros
        int zero = -1;
        int un = -1;
        int deux = -1;
        int trois = -1;
        int quatre = -1;
        int cinq = -1;
        int six = -1;
        int sept = -1;
        int huit = -1;
        int neuf = -1;
        // new
        int À = -1;
        int ü = -1;

        /// <summary>
        /// define all the settings for the letter
        /// </summary>
        /// <param name="complexe">if false, use only (A-Z)</param>
        /// <param name="augment">key</param>
        /// <param name="total">total number letter</param>
        private void settings(bool complexe, int augment, int total)
        {
            // seting the variables
            a = defNum(1, augment, total);
            b = defNum(2, augment, total);
            c = defNum(3, augment, total);
            d = defNum(4, augment, total);
            e = defNum(5, augment, total);
            f = defNum(6, augment, total);
            g = defNum(7, augment, total);
            h = defNum(8, augment, total);
            i = defNum(9, augment, total);
            j = defNum(10, augment, total);
            k = defNum(11, augment, total);
            l = defNum(12, augment, total);
            m = defNum(13, augment, total);
            n = defNum(14, augment, total);
            o = defNum(15, augment, total);
            p = defNum(16, augment, total);
            q = defNum(17, augment, total);
            r = defNum(18, augment, total);
            s = defNum(19, augment, total);
            t = defNum(20, augment, total);
            u = defNum(21, augment, total);
            v = defNum(22, augment, total);
            w = defNum(23, augment, total);
            x = defNum(24, augment, total);
            y = defNum(25, augment, total);
            z = defNum(26, augment, total);

            if (complexe == true)
            {
                é = defNum(27, augment, total);
                è = defNum(28, augment, total);
                à = defNum(29, augment, total);
                ù = defNum(30, augment, total);
                parentése_open = defNum(31, augment, total);
                parentése_closed = defNum(32, augment, total);
                ê = defNum(33, augment, total);
                ç = defNum(34, augment, total);

                ë = defNum(35, augment, total);
                point_virgule = defNum(36, augment, total);
                deux_points = defNum(37, augment, total);
                point = defNum(38, augment, total);
                point_exlamation = defNum(39, augment, total);
                point_interrogation = defNum(40, augment, total);
                arro_base = defNum(41, augment, total);
                pour_cent = defNum(42, augment, total);
                virgule = defNum(43, augment, total);
                hachtag = defNum(44, augment, total);
                crochet_ouvert = defNum(45, augment, total);
                crochet_fermé = defNum(46, augment, total);
                egal = defNum(47, augment, total);
                guillemet_simple = defNum(48, augment, total);
                guillemet_double = defNum(49, augment, total);
                plus = defNum(50, augment, total);
                moin = defNum(51, augment, total);
                i_trema = defNum(52, augment, total);
                â = defNum(53, augment, total);
                œ = defNum(54, augment, total);
                ô = defNum(55, augment, total);
                num = defNum(56, augment, total);
                liste_point = defNum(57, augment, total);
                î = defNum(58, augment, total);
                guillemet_ligne_ouvert = defNum(59, augment, total);
                guillemet_ligne_ferme = defNum(60, augment, total);
                û = defNum(61, augment, total); ;
                apostrophe = defNum(62, augment, total);

                A = defNum(63, augment, total);
                B = defNum(64, augment, total);
                C = defNum(65, augment, total);
                D = defNum(66, augment, total);
                E = defNum(67, augment, total);
                F = defNum(68, augment, total);
                G = defNum(69, augment, total);
                H = defNum(70, augment, total);
                I = defNum(71, augment, total);
                J = defNum(72, augment, total);
                K = defNum(73, augment, total);
                L = defNum(74, augment, total);
                M = defNum(75, augment, total);
                N = defNum(76, augment, total);
                O = defNum(77, augment, total);
                P = defNum(78, augment, total);
                Q = defNum(79, augment, total);
                R = defNum(80, augment, total);
                S = defNum(81, augment, total);
                T = defNum(82, augment, total);
                U = defNum(83, augment, total);
                V = defNum(84, augment, total);
                W = defNum(85, augment, total);
                X = defNum(86, augment, total);
                Y = defNum(87, augment, total);
                Z = defNum(88, augment, total);
                //numeros
                zero = defNum(89, augment, total);
                un = defNum(90, augment, total);
                deux = defNum(91, augment, total);
                trois = defNum(92, augment, total);
                quatre = defNum(93, augment, total);
                cinq = defNum(94, augment, total);
                six = defNum(95, augment, total);
                sept = defNum(96, augment, total);
                huit = defNum(97, augment, total);
                neuf = defNum(98, augment, total);
                // new
                À = defNum(99, augment, total);
                ü = defNum(100, augment, total);
                guillemet_simple_ouvert = defNum(101, augment, total);
                guillemet_simple_ferme = defNum(102, augment, total);
                grand_moin = defNum(103, augment, total);
                point_de_suspension = defNum(104, augment, total);
                plus_petit = defNum(105, augment, total);
                plus_grand = defNum(106, augment, total);
                /*
               = defNum(, augment, total);
                = defNum(, augment, total);   
              */

            }
        }

        /// <summary>
        /// This method allow the user to crypt a string using a key (int)
        /// </summary>
        /// <param name="input_public">input string of the code</param>
        /// <param name="complexe">if the program use only upper case letters (A-Z)</param>
        /// <param name="toAugment">if the program make +1 to the key when he crypt a letter (will be depreciated)</param>
        /// <param name="augment"> input key</param>
        /// <returns></returns>
        public string cryptCustom(string input_public, bool complexe, bool toAugment, int augment)
        {
            // definition des varaiables 

            int total = defTotal(complexe);

            StringBuilder sw = new StringBuilder();

            settings(complexe, augment, total);

            foreach (char ch in input_public)
            {
                bool isChar = true;

                if (ch == 'a') { sw.Append(a); }
                else
                if (ch == 'b') { sw.Append(b); }
                else
                if (ch == 'c') { sw.Append(c); }
                else
                if (ch == 'd') { sw.Append(d); }
                else
                if (ch == 'e') { sw.Append(e); }
                else
                if (ch == 'f') { sw.Append(f); }
                else
                if (ch == 'g') { sw.Append(g); }
                else
                if (ch == 'h') { sw.Append(h); }
                else
                if (ch == 'i') { sw.Append(i); }
                else
                if (ch == 'j') { sw.Append(j); }
                else
                if (ch == 'k') { sw.Append(k); }
                else
                if (ch == 'l') { sw.Append(l); }
                else
                if (ch == 'm') { sw.Append(m); }
                else
                if (ch == 'n') { sw.Append(n); }
                else
                if (ch == 'o') { sw.Append(o); }
                else
                if (ch == 'p') { sw.Append(p); }
                else
                if (ch == 'q') { sw.Append(q); }
                else
                if (ch == 'r') { sw.Append(r); }
                else
                if (ch == 's') { sw.Append(s); }
                else
                if (ch == 't') { sw.Append(t); }
                else
                if (ch == 'u') { sw.Append(u); }
                else
                if (ch == 'v') { sw.Append(v); }
                else
                if (ch == 'w') { sw.Append(w); }
                else
                if (ch == 'x') { sw.Append(x); }
                else
                if (ch == 'y') { sw.Append(y); }
                else
                if (ch == 'z') { sw.Append(z); }
                else
                if (ch == 'z') { sw.Append(z); }
                else
                //majuscules
                if (ch == 'A') { sw.Append(A); }
                else
                if (ch == 'B') { sw.Append(B); }
                else
                if (ch == 'C') { sw.Append(C); }
                else
                if (ch == 'D') { sw.Append(D); }
                else
                if (ch == 'E') { sw.Append(E); }
                else
                if (ch == 'F') { sw.Append(F); }
                else
                if (ch == 'G') { sw.Append(G); }
                else
                if (ch == 'H') { sw.Append(H); }
                else
                if (ch == 'I') { sw.Append(I); }
                else
                if (ch == 'J') { sw.Append(J); }
                else
                if (ch == 'K') { sw.Append(K); }
                else
                if (ch == 'L') { sw.Append(L); }
                else
                if (ch == 'M') { sw.Append(M); }
                else
                if (ch == 'N') { sw.Append(N); }
                else
                if (ch == 'O') { sw.Append(O); }
                else
                if (ch == 'P') { sw.Append(P); }
                else
                if (ch == 'Q') { sw.Append(Q); }
                else
                if (ch == 'R') { sw.Append(R); }
                else
                if (ch == 'S') { sw.Append(S); }
                else
                if (ch == 'T') { sw.Append(T); }
                else
                if (ch == 'U') { sw.Append(U); }
                else
                if (ch == 'V') { sw.Append(V); }
                else
                if (ch == 'W') { sw.Append(W); }
                else
                if (ch == 'X') { sw.Append(X); }
                else
                if (ch == 'Y') { sw.Append(Y); }
                else
                if (ch == 'Z') { sw.Append(Z); }
                else
                //ading the complexe chars
                if (ch == 'é' | ch == 'É') { sw.Append(é); }
                else
                if (ch == 'è') { sw.Append(è); }
                else
                if (ch == 'à') { sw.Append(à); }
                else
                if (ch == 'ù') { sw.Append(ù); }
                else
                if (ch == '(') { sw.Append(parentése_open); }
                else
                if (ch == ')') { sw.Append(parentése_closed); }
                else
                if (ch == 'ê') { sw.Append(ê); }
                else
                if (ch == 'ç') { sw.Append(ç); }
                else
                if (ch == 'ë') { sw.Append(ë); }
                else
                if (ch == '!') { sw.Append(point_exlamation); }
                else
                if (ch == '.') { sw.Append(point); }
                else
                if (ch == '@') { sw.Append(arro_base); }
                else
                if (ch == '?') { sw.Append(point_interrogation); }
                else
                if (ch == ';') { sw.Append(point_virgule); }
                else
                if (ch == ',') { sw.Append(virgule); }
                else
                if (ch == ':') { sw.Append(deux_points); }
                else
                if (ch == '#') { sw.Append(hachtag); }
                else
                if (ch == '%') { sw.Append(pour_cent); }
                else
                if (ch == '<') { sw.Append(plus_petit); }
                else
                if (ch == '>') { sw.Append(plus_grand); }
                else
                if (ch == '[') { sw.Append(crochet_ouvert); }
                else
                if (ch == ']') { sw.Append(crochet_fermé); }
                else
                if (ch == '=') { sw.Append(egal); }
                else
                if (ch == '\'') { sw.Append(guillemet_simple); }
                else
                if (ch == '"') { sw.Append(guillemet_double); }
                else
                if (ch == '+') { sw.Append(plus); }
                else
                if (ch == '-') { sw.Append(moin); }
                else
                if (ch == 'ï') { sw.Append(i_trema); }
                else
                if (ch == 'â') { sw.Append(â); }
                else
                if (ch == 'œ' | ch == 'Œ') { sw.Append(œ); }
                else
                if (ch == 'ô') { sw.Append(ô); }
                else
                if (ch == '°') { sw.Append(num); }
                else
                if (ch == '•') { sw.Append(liste_point); }
                else
                if (ch == 'î') { sw.Append(î); }
                else
                if (ch == '«') { sw.Append(guillemet_ligne_ouvert); }
                else
                if (ch == '»') { sw.Append(guillemet_ligne_ferme); }
                else
                if (ch == 'û') { sw.Append(û); }
                else
                if (ch == '’') { sw.Append(apostrophe); }
                else
                //numéros
                if (ch == '0') { sw.Append(zero); }
                else
                if (ch == '1') { sw.Append(un); }
                else
                if (ch == '2') { sw.Append(deux); }
                else
                if (ch == '3') { sw.Append(trois); }
                else
                if (ch == '4') { sw.Append(quatre); }
                else
                if (ch == '5') { sw.Append(cinq); }
                else
                if (ch == '6') { sw.Append(six); }
                else
                if (ch == '7') { sw.Append(sept); }
                else
                if (ch == '8') { sw.Append(huit); }
                else
                if (ch == '9') { sw.Append(neuf); }
                else
                // new
                if (ch == 'À') { sw.Append(À); }
                else
                if (ch == 'ü' | ch == 'Ü') { sw.Append(ü); }
                else
                if (ch == '“') { sw.Append(guillemet_simple_ouvert); }
                else
                if (ch == '”') { sw.Append(guillemet_simple_ferme); }
                else
                if (ch == '—') { sw.Append(grand_moin); }
                else
                /*else
                if (ch == Convert.ToChar(710))) { sw.Append("===================yesss======================"); }*/

                if (ch == '…') { sw.Append(point_de_suspension); }
                else
                /*
                if (ch == '') { sw.Append(); }
                else
                if (ch == '') { sw.Append(); }
                else
                */
                {
                    if (ch != ' ')
                    {
                        sw.Append(ch);
                    }
                    isChar = false;
                }
                if (toAugment && isChar)
                {
                    if (toAugment && ch != ' ')
                    {
                        augment++;
                        if (augment > 26)
                        {
                            augment = augment - total;
                        }
                    }
                }
                sw.Append(" ");

            }
            if (sw.ToString() != "")
            {
                sw.Remove(sw.Length - 1, 1);
            }
            /*string[] words = code.ToString().Split(' ');
            StringBuilder exit = new StringBuilder();
            foreach (string word in words)
            {

                //char[0] end = ' ';
                Convert.ToByte(word);
                /*end[0] = char.Parse(ch);
                exit.Append(System.Text.Encoding.Default.GetBytes(end).ToString());
                exit.Append(" ");
            }
        */



            return sw.ToString();
        }
        /// <summary>
        /// Decode texte
        /// </summary>
        /// <param name="input_public"></param>
        /// <param name="complexe"></param>
        /// <param name="toAugment"></param>
        /// <param name="augment"></param>
        /// <returns></returns>
        public string decodeCustom(string input_public, bool complexe, bool toAugment, int augment)
        {
            int total = defTotal(complexe);
            StringBuilder sw = new StringBuilder();


            using (StringReader input = new StringReader(input_public))
            {
                string line;

                int countLine = 0;
                while ((line = input.ReadLine()) != null)
                {
                    countLine++;


                    string[] words = line.Split(' ');
                    foreach (string word in words)
                    {
                        // settings the values of  all the variables
                        settings(complexe, augment, total);

                        if (word != "")
                        {
                            try
                            {
                                int ch = Int32.Parse(word);
                                // entering the variables
                                if (ch == a) { sw.Append("a"); }
                                else
                                if (ch == b) { sw.Append("b"); }
                                else
                                if (ch == c) { sw.Append("c"); }
                                else
                                if (ch == d) { sw.Append("d"); }
                                else
                                if (ch == e) { sw.Append("e"); }
                                else
                                if (ch == f) { sw.Append("f"); }
                                else
                                if (ch == g) { sw.Append("g"); }
                                else
                                if (ch == h) { sw.Append("h"); }
                                else
                                if (ch == i) { sw.Append("i"); }
                                else
                                if (ch == j) { sw.Append("j"); }
                                else
                                if (ch == k) { sw.Append("k"); }
                                else
                                if (ch == l) { sw.Append("l"); }
                                else
                                if (ch == m) { sw.Append("m"); }
                                else
                                if (ch == n) { sw.Append("n"); }
                                else
                                if (ch == o) { sw.Append("o"); }
                                else
                                if (ch == p) { sw.Append("p"); }
                                else
                                if (ch == q) { sw.Append("q"); }
                                else
                                if (ch == r) { sw.Append("r"); }
                                else
                                if (ch == s) { sw.Append("s"); }
                                else
                                if (ch == t) { sw.Append("t"); }
                                else
                                if (ch == u) { sw.Append("u"); }
                                else
                                if (ch == v) { sw.Append("v"); }
                                else
                                if (ch == w) { sw.Append("w"); }
                                else
                                if (ch == x) { sw.Append("x"); }
                                else
                                if (ch == y) { sw.Append("y"); }
                                else
                                if (ch == z) { sw.Append("z"); }
                                else
                                if (complexe)
                                {
                                    if (ch == é) { sw.Append("é"); }
                                    else
                                    if (ch == è) { sw.Append("è"); }
                                    else
                                    if (ch == à) { sw.Append("à"); }
                                    else
                                    if (ch == ù) { sw.Append("ù"); }
                                    else
                                    if (ch == parentése_open) { sw.Append("("); }
                                    else
                                    if (ch == parentése_closed) { sw.Append(")"); }
                                    else
                                    if (ch == ê) { sw.Append("ê"); }
                                    else
                                    if (ch == ç) { sw.Append("ç"); }
                                    else
                                    if (ch == ë) { sw.Append("ë"); }
                                    else
                                    if (ch == point_exlamation) { sw.Append("!"); }
                                    else
                                    if (ch == point) { sw.Append("."); }
                                    else
                                    if (ch == arro_base) { sw.Append("@"); }
                                    else
                                    if (ch == point_interrogation) { sw.Append("?"); }
                                    else
                                    if (ch == point_virgule) { sw.Append(";"); }
                                    else
                                    if (ch == virgule) { sw.Append(","); }
                                    else
                                    if (ch == deux_points) { sw.Append(":"); }
                                    else
                                    if (ch == hachtag) { sw.Append("#"); }
                                    else
                                    if (ch == pour_cent) { sw.Append("%"); }
                                    else
                                    if (ch == crochet_ouvert) { sw.Append("["); }
                                    else
                                    if (ch == crochet_fermé) { sw.Append("]"); }
                                    else
                                    if (ch == plus_petit) { sw.Append("<"); }
                                    else
                                    if (ch == plus_grand) { sw.Append(">"); }
                                    else
                                    if (ch == egal) { sw.Append("="); }
                                    else
                                    if (ch == guillemet_simple) { sw.Append("'"); }
                                    else
                                    if (ch == guillemet_double) { sw.Append("\""); }
                                    else
                                    if (ch == plus) { sw.Append("+"); }
                                    else
                                    if (ch == moin) { sw.Append("-"); }
                                    else
                                    if (ch == i_trema) { sw.Append("ï"); }
                                    else
                                    if (ch == â) { sw.Append("â"); }
                                    else
                                    if (ch == œ) { sw.Append("œ"); }
                                    else
                                    if (ch == ô) { sw.Append("ô"); }
                                    else
                                    if (ch == num) { sw.Append("°"); }
                                    else
                                    if (ch == liste_point) { sw.Append("•"); }
                                    else
                                    if (ch == î) { sw.Append("î"); }
                                    else
                                    if (ch == guillemet_ligne_ouvert) { sw.Append("«"); }
                                    else
                                    if (ch == guillemet_ligne_ferme) { sw.Append("»"); }
                                    else
                                    if (ch == û) { sw.Append("û"); }
                                    else
                                    if (ch == apostrophe) { sw.Append("’"); }
                                    else
                                    if (ch == û) { sw.Append("û"); }
                                    else
                                    // majuscule
                                    if (ch == A) { sw.Append("A"); }
                                    else
                                    if (ch == B) { sw.Append("B"); }
                                    else
                                    if (ch == C) { sw.Append("C"); }
                                    else
                                    if (ch == D) { sw.Append("D"); }
                                    else
                                    if (ch == E) { sw.Append("E"); }
                                    else
                                    if (ch == F) { sw.Append("F"); }
                                    else
                                    if (ch == G) { sw.Append("G"); }
                                    else
                                    if (ch == I) { sw.Append("I"); }
                                    else
                                    if (ch == J) { sw.Append("J"); }
                                    else
                                    if (ch == K) { sw.Append("K"); }
                                    else
                                    if (ch == L) { sw.Append("L"); }
                                    else
                                    if (ch == M) { sw.Append("M"); }
                                    else
                                    if (ch == N) { sw.Append("N"); }
                                    else
                                    if (ch == O) { sw.Append("O"); }
                                    else
                                    if (ch == P) { sw.Append("p"); }
                                    else
                                    if (ch == Q) { sw.Append("Q"); }
                                    else
                                    if (ch == R) { sw.Append("R"); }
                                    else
                                    if (ch == S) { sw.Append("S"); }
                                    else
                                    if (ch == T) { sw.Append("T"); }
                                    else
                                    if (ch == U) { sw.Append("U"); }
                                    else
                                    if (ch == V) { sw.Append("V"); }
                                    else
                                    if (ch == W) { sw.Append("W"); }
                                    else
                                    if (ch == X) { sw.Append("X"); }
                                    else
                                    if (ch == Y) { sw.Append("Y"); }
                                    else
                                    if (ch == Z) { sw.Append("Z"); }
                                    else
                                    // numeros
                                    if (ch == zero) { sw.Append("0"); }
                                    else
                                    if (ch == un) { sw.Append("1"); }
                                    else
                                    if (ch == deux) { sw.Append("2"); }
                                    else
                                    if (ch == trois) { sw.Append("3"); }
                                    else
                                    if (ch == quatre) { sw.Append("4"); }
                                    else
                                    if (ch == cinq) { sw.Append("5"); }
                                    else
                                    if (ch == six) { sw.Append("6"); }
                                    else
                                    if (ch == sept) { sw.Append("7"); }
                                    else
                                    if (ch == huit) { sw.Append("8"); }
                                    else
                                    if (ch == neuf) { sw.Append("9"); }
                                    else
                                    // new
                                    if (ch == À) { sw.Append("À"); }
                                    else
                                    if (ch == ü) { sw.Append("ü"); }
                                    else
                                    if (ch == guillemet_simple_ouvert) { sw.Append("“"); }
                                    else
                                    if (ch == guillemet_simple_ferme) { sw.Append("”"); }
                                    else
                                    if (ch == grand_moin) { sw.Append("—"); }
                                    else
                                    if (ch == point_de_suspension) { sw.Append("…"); }
                                    /*
                                    if (ch == ) { sw.Append(""); }
                                    else
                                    if (ch == ) { sw.Append(""); }
                                    else
                                    */
                                }
                                if (toAugment)
                                {
                                    augment++;
                                    if (augment > 26)
                                    {
                                        augment = augment - total;
                                    }
                                }


                            }
                            catch (FormatException)
                            {
                                sw.Append(word);
                            }

                        }
                        else
                        {
                            sw.Append(" ");
                        }
                    }
                }

            }
            // ended
            return sw.ToString();
        }

        /// <summary>
        /// This code retunr the letter position using the key
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="augment"></param>
        /// <param name="total"></param>
        /// <returns></returns>
        private int defNum(int pos, int augment, int total)
        {
            int result = 0;
            if (pos < (total - augment))
            {
                result = pos + augment;
            }
            else
            {
                result = pos + augment - total;
            }
            return result;
        }
        /// <summary>
        /// Total number of letters
        /// </summary>
        /// <param name="complexe"></param>
        /// <returns></returns>
        private int defTotal(bool complexe)
        {
            int total = 26;
            if (complexe)
            {
                total = 106;
            }
            return total;
        }
    }
}
