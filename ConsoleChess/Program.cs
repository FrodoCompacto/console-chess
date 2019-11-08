using System;
using ConsoleChess.tabuleiro;
using ConsoleChess.xadrez;

namespace ConsoleChess {
    internal class Program {
        public static void Main(string[] args) {
            
            
            PartidaDeXadrez partida = new PartidaDeXadrez();
            

            Tela.ImprimirTabuleiro(partida.Tab);
            
            Console.ReadLine();
        }

        
    }
}