using System;
using ConsoleChess.tabuleiro;

namespace ConsoleChess {
    internal class Program {
        public static void Main(string[] args) {
            
            Tabuleiro tab = new Tabuleiro(8,8);
            
            Tela.imprimirTabuleiro(tab);

            Console.ReadLine();
        }
    }
}