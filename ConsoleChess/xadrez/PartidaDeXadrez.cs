using ConsoleChess.tabuleiro;

namespace ConsoleChess.xadrez {
    public class PartidaDeXadrez {
        public Tabuleiro Tab { get; set; }
        private int Turno { get; set; }
        private Cor JogadorAtual { get; set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez() {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
        }

        public void ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();

            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            
        }

        private void ColocarPecas() {
            Tab.ColocarPeca(new Rei(Cor.Preta, Tab),new PosicaoXadrez('c', 1).ToPosicao() );
        }
    }
}