namespace ConsoleChess.tabuleiro {
    public abstract class Peca {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Cor cor, Tabuleiro tabuleiro) {
            Posicao = null;
            Cor = cor;
            QtdMovimentos = 0;
            Tabuleiro = tabuleiro;
        }

        public void IncrementarQtdMovimentos() {
            QtdMovimentos++;
        }
        
        public void DecrementarQtdMovimentos() {
            QtdMovimentos--;
        }

        public bool ExitemMovimentosPossiveis() {
            bool[,] aux = MovimentosPossiveis();

            for (int i = 0; i < Tabuleiro.Linhas; i++) {
                for (int j = 0; j < Tabuleiro.Colunas; j++) {
                    if (aux[i, j]) {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool MovimentoPossivel(Posicao pos) {
            return MovimentosPossiveis()[pos.Linha, pos.Coluna];
        }

        public abstract bool[,] MovimentosPossiveis();
    }
}