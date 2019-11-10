using ConsoleChess.tabuleiro;


namespace ConsoleChess.xadrez {
    public class Rei : Peca{
        
        public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro) {
        }

        public override string ToString() {
            return "R";
        }

        private bool PodeMover(Posicao pos) {
            Peca p = Tabuleiro.Peca(pos);
            return p == null || p.Cor != this.Cor;
        }
        
        public override bool[,] MovimentosPossiveis() {
            bool[,] mat = new bool[Tabuleiro.Linhas,Tabuleiro.Colunas];
            
            Posicao pos = new Posicao(0,0);
            
            
            //acima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            
            //ne
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna+1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            
            //direita
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna+1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            
            //se
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna+1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            
            //abaixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            
            //so
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna -1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            
            //esq
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna-1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }
            
            //no
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna -1);
            if (Tabuleiro.PosicaoValida(pos) && PodeMover(pos)) {
                mat[pos.Linha, pos.Coluna] = true;
            }

            return mat;
        }
    }
}