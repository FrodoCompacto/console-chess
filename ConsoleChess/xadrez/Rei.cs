using ConsoleChess.tabuleiro;


namespace ConsoleChess.xadrez {
    public class Rei : Peca{
        
        public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) {
        }

        public override string ToString() {
            return "R";
        }
    }
}