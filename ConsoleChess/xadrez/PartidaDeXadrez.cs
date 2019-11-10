using System.Collections.Generic;
using System.Linq;
using ConsoleChess.tabuleiro;

namespace ConsoleChess.xadrez {
    public class PartidaDeXadrez {
        public Tabuleiro Tab { get; set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }
        private HashSet<Peca> Pecas { get; set; }
        private HashSet<Peca> Capturadas { get; set; }
        public bool Xeque { get; private set; }


        public PartidaDeXadrez() {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino) {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimentos();

            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if (pecaCapturada != null) {
                Capturadas.Add(pecaCapturada);
            }

            return pecaCapturada;
        }

        public void RealizaJogada(Posicao origem, Posicao destino) {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual)) {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque!");
            }

            if (EstaEmXeque(Adversaria(JogadorAtual))) {
                Xeque = true;
            }
            else {
                Xeque = false;
            }

            if (TexteXequeMate(Adversaria(JogadorAtual))) {
                Terminada = true;
            }
            else {
                this.Turno++;
                MudaJogador();   
            }
        }

        private void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada) {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if (pecaCapturada != null) {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }

            Tab.ColocarPeca(p, origem);
        }

        public void ValidaPosicaoOrigem(Posicao pos) {
            if (Tab.Peca(pos) == null) {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida.");
            }

            if (JogadorAtual != Tab.Peca(pos).Cor) {
                throw new TabuleiroException("A peça escolhida não é sua.");
            }

            if (!Tab.Peca(pos).ExitemMovimentosPossiveis()) {
                throw new TabuleiroException("Não existem movimentos possíveis para esta peça.");
            }
        }

        public void ValidarPosicaoDestino(Posicao origem, Posicao destino) {
            if (!Tab.Peca(origem).MovimentoPossivel(destino)) {
                throw new TabuleiroException("Posição de destino inválida.");
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor) {
            HashSet<Peca> temp = new HashSet<Peca>();
            foreach (Peca x in Capturadas) {
                if (x.Cor == cor) {
                    temp.Add(x);
                }
            }

            return temp;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor) {
            HashSet<Peca> temp = new HashSet<Peca>();
            foreach (Peca x in Pecas) {
                if (x.Cor == cor) {
                    temp.Add(x);
                }
            }

            temp.ExceptWith(PecasCapturadas(cor));
            return temp;
        }

        private Cor Adversaria(Cor cor) {
            return cor == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        private Peca Rei(Cor cor) {
            return PecasEmJogo(cor).OfType<Rei>().FirstOrDefault();
//            foreach (Peca x in PecasEmJogo(cor)) {
//                if (x is Rei) {
//                    return x;
//                }
//            }
//
//            return null;
        }

        public bool EstaEmXeque(Cor cor) {
            Peca R = Rei(cor);
            if (R == null) {
                throw new TabuleiroException("Rei da cor " + cor + " inexistente.");
            }

            return PecasEmJogo(Adversaria(cor)).Select(x => x.MovimentosPossiveis())
                .Any(mat => mat[R.Posicao.Linha, R.Posicao.Coluna]);
        }

        public bool TexteXequeMate(Cor cor) {
            if (!EstaEmXeque(cor)) {
                return false;
            }

            foreach (Peca x in PecasEmJogo(cor)) {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++) {
                    for (int j = 0; j < Tab.Colunas; j++) {
                        if (mat[i, j]) {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque) {
                                return false;
                            }
                        }
                    }
                }
            }

            return true;
        }

        private void MudaJogador() {
            JogadorAtual = JogadorAtual == Cor.Branca ? Cor.Preta : Cor.Branca;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca) {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas() {
            ColocarNovaPeca('c', 1, new Torre(Cor.Branca, Tab));
//            ColocarNovaPeca('c', 2, new Torre(Cor.Branca, Tab));
//            ColocarNovaPeca('d', 2, new Torre(Cor.Branca, Tab));
//            ColocarNovaPeca('e', 2, new Torre(Cor.Branca, Tab));
//            ColocarNovaPeca('e', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('d', 1, new Rei(Cor.Branca, Tab));
            ColocarNovaPeca('h', 7, new Torre(Cor.Branca, Tab));

//            ColocarNovaPeca('c', 7, new Torre(Cor.Preta, Tab));
//            ColocarNovaPeca('c', 8, new Torre(Cor.Preta, Tab));
//            ColocarNovaPeca('d', 7, new Torre(Cor.Preta, Tab));
//            ColocarNovaPeca('e', 7, new Torre(Cor.Preta, Tab));
//            ColocarNovaPeca('e', 8, new Torre(Cor.Preta, Tab));
//            ColocarNovaPeca('d', 8, new Rei(Cor.Preta, Tab));
            ColocarNovaPeca('a', 8, new Rei(Cor.Preta, Tab));
            ColocarNovaPeca('b', 8, new Torre(Cor.Preta, Tab));
        }
    }
}