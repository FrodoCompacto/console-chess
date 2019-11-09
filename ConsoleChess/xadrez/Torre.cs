﻿using ConsoleChess.tabuleiro;


namespace ConsoleChess.xadrez {
    public class Torre : Peca{
        
        public Torre(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) {
        }

        public override string ToString() {
            return "T";
        }
        
        public override bool[,] MovimentosPossiveis() {
            //TODO
            throw new System.NotImplementedException();
        }
    }
}