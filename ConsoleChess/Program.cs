using System;
using ConsoleChess.tabuleiro;
using ConsoleChess.xadrez;

namespace ConsoleChess {
    internal class Program {
        public static void Main(string[] args) {
            
            
            PartidaDeXadrez partida = new PartidaDeXadrez();

            while (!partida.Terminada) {

                Console.Clear();
                Tela.ImprimirTabuleiro(partida.Tab);
                
                Console.Write("Origem: ");
                Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                Console.Write("Destino: ");
                Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                
                partida.ExecutaMovimento(origem,destino);

            }

            Console.ReadLine();
        }

        
    }
}