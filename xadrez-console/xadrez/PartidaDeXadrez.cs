using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cor jogadorAtual;
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }
        public void executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimetos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
        }
        //MÉTODO COM NIVEL MAIS ALTO DE ABSTRAÇÃO
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            //REALIZA A JOGODA
            executaMovimento(origem, destino);
            //INCREMENTA O TURNO DO JOGO 
            turno++;
            //ALTERA A VEZ DE QUEM IRÁ JOGAR 
            mudaJogador();

        }
        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)//çança exceção indicando que a posição indicada não existe 
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("Peça de origem não lhe pertence");
            }
            if (!tab.peca(pos).existeMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida");
            }
        }
        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).podeMoverPara(destino))
            {
                throw new TabuleiroException("Posição invalida!");
            }
        }
        //MÉTDO PRIVADO AUXILIAR DO MÉTODO REALIZA JOGADA 
        private void  mudaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }
        //ESTE MÉTODO TEM COMO PROPOSITO ARMAZENAR AS PEÇAS CAPTURADAS DE MESMA COR DENTRO DE UMA COLEÇÃO E RETORNAR
        //ESTA COLEÇÃO, É CRIADO UMA COLEÇÃO AUXILIAR QUE ARMAZENA TEMPORARIAMENTE AS PEÇAS RETIRADAS DO TABULEIRO 
        public HashSet<Peca> pecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }

        public HashSet<Peca> pecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in pecas)
            {
                if(x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturadas(cor));//faz a subtração das peças totais menos as peças capturadas
            return aux;
        }
        public void colocarNovaPeça(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna,linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {
            colocarNovaPeça('c', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeça('c', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeça('d', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeça('e', 2, new Torre(tab, Cor.Branca));
            colocarNovaPeça('e', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeça('d', 1, new Rei(tab, Cor.Branca));

            colocarNovaPeça('c', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeça('c', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeça('d', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeça('e', 7, new Torre(tab, Cor.Preta));
            colocarNovaPeça('e', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeça('d', 8, new Rei(tab, Cor.Preta));

        }
    }
}
