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
        public bool xeque { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            xeque = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }
        public Peca executaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.retirarPeca(origem);
            p.incrementarQteMovimetos();
            Peca pecaCapturada = tab.retirarPeca(destino);
            tab.colocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            return pecaCapturada;
        }
        //MÉTODO COM NIVEL MAIS ALTO DE ABSTRAÇÃO
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            //REALIZA A JOGODA
            Peca pecaCapturada = executaMovimento(origem, destino);
            if (estaEmXeque(jogadorAtual))//teste
            {
                desfazMovimento(origem,destino,pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em Xeque!");
            }
            if (estaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }
            if (testeXequemate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                //INCREMENTA O TURNO DO JOGO 
                turno++;
                //ALTERA A VEZ DE QUEM IRÁ JOGAR 
                mudaJogador();
            }
        }

        //DESFAZ MOVIMENTO EM CASO DE OCORRENCIA DE SITUAÇÃO DE XEQUE 
        public void desfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.retirarPeca(destino);
            p.decrementarQteMovimetos();
            if (pecaCapturada != null)
            {
                tab.colocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.colocarPeca(p, origem);
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
            if (!tab.peca(origem).movimentoPossivel(destino))
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
        //RETORNA O VALOR COR DA PEÇA ADVERSÁRIA
        private Cor adversaria(Cor cor)
        {
            if(cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        //RETORNA A PEÇA REI DENTRO DO TABULEIRO, A UNICA QUE PODE RECEBER O XEQUE
        private Peca rei(Cor cor)
        {
            foreach(Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                } 
            }
            return null;//condição que nunca ocorrerá colocada apenas por questão de desenvolvimento 
        }

        //VERIFICA SE O REI DE DETERMINADA PEÇA ESTA EM XEQUE COM BASE NA MATRIZ DE MOVIMENTO DE TODAS AS PEÇAS 
        //ADVERSÁRIAS DENTRO DO TABULEIRO 
        public bool estaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            //EXCEÇÃO QUE EM TESE NUNCA DEVE OCORRER DE UM REI NÃO ESTAR MAIS EM JOGO E O JOGO PROSSEGUIR (SEGURANÇA)
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da " + cor + " no tabuleiro!");
            }
            foreach(Peca x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.movimentosPossiveis();
                if(mat[R.posicao.linha,R.posicao.coluna])
                {
                    return true;//se o rei esta em xeque retorna true
                }
            }
            return false;//se não existe rei em xeque retorna false
        }

        public bool testeXequemate(Cor cor)
        {
            //VERIFICA INICIALMENTE SE EXISTE ALGUMA CONDIÇÃO DE XEQUE, CASO DE NÃO EXISTENCIA NÃO PROSSEGUE 
            if (!estaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.movimentosPossiveis();
                for (int i = 0; i < tab.colunas; i++)
                {
                    for (int j = 0; j < tab.linhas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = executaMovimento(origem, destino);
                            bool testeXeque = estaEmXeque(cor);
                            desfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        public void colocarNovaPeça(char coluna, int linha, Peca peca)
        {
            tab.colocarPeca(peca, new PosicaoXadrez(coluna,linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {
            colocarNovaPeça('a', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeça('b', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeça('c', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeça('d', 1, new Dama(tab, Cor.Branca));
            colocarNovaPeça('e', 1, new Rei(tab, Cor.Branca));
            colocarNovaPeça('f', 1, new Bispo(tab, Cor.Branca));
            colocarNovaPeça('g', 1, new Cavalo(tab, Cor.Branca));
            colocarNovaPeça('h', 1, new Torre(tab, Cor.Branca));
            colocarNovaPeça('a', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeça('b', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeça('c', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeça('d', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeça('e', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeça('f', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeça('g', 2, new Peao(tab, Cor.Branca));
            colocarNovaPeça('h', 2, new Peao(tab, Cor.Branca));

            colocarNovaPeça('a', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeça('b', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeça('c', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeça('d', 8, new Dama(tab, Cor.Preta));
            colocarNovaPeça('e', 8, new Rei(tab, Cor.Preta));
            colocarNovaPeça('f', 8, new Bispo(tab, Cor.Preta));
            colocarNovaPeça('g', 8, new Cavalo(tab, Cor.Preta));
            colocarNovaPeça('h', 8, new Torre(tab, Cor.Preta));
            colocarNovaPeça('a', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeça('b', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeça('c', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeça('d', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeça('e', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeça('f', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeça('g', 7, new Peao(tab, Cor.Preta));
            colocarNovaPeça('h', 7, new Peao(tab, Cor.Preta));

        }
    }
}
