using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {
        //UTILIZADO PARA JOGADA ESPECIAL 
        private PartidaDeXadrez partida;
        public Rei(Tabuleiro tab, Cor cor,PartidaDeXadrez partida) : base(tab, cor)
        {
            this.partida = partida;
        }
        public override string ToString()
        {
            return "R";
        }
        private bool podeMover(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p == null || p.cor != cor;//retornará se o destino é nullo ou se o destino possui peça adversária
        }
        private bool testeTorreParaRoque(Posicao pos)
        {
            Peca p = tab.peca(pos);
            return p != null && p is Torre && p.cor == cor && p.qteMovimentos == 0;
        }
        public override bool[,] movimentosPossiveis()
        {
            bool[,] mat = new bool[tab.linhas,tab.colunas];

            Posicao pos = new Posicao(0, 0);

            /*
             * TESTE DE POSIÇÕES
             */
            //ACIMA -> MESMA COLUNA UMA LINHA A MENOS
            pos.definirValores(posicao.linha - 1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //NORDESTE -> COLUNA+1 LINHA-1
            pos.definirValores(posicao.linha - 1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //LESTE -> COLUNA+1 LINHA+0
            pos.definirValores(posicao.linha, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //SUDESTE -> COLUNA+1 LINHA+1
            pos.definirValores(posicao.linha+1, posicao.coluna + 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //SUL -> COLUNA1 LINHA+1
            pos.definirValores(posicao.linha+1, posicao.coluna);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //SUDOESTE -> COLUNA-1 LINHA+1
            pos.definirValores(posicao.linha+1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //OESTE -> COLUNA-1 LINHA+0
            pos.definirValores(posicao.linha, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }
            //NOROESTE -> COLUNA-1 LINHA-1
            pos.definirValores(posicao.linha - 1, posicao.coluna - 1);
            if (tab.posicaoValida(pos) && podeMover(pos))
            {
                mat[pos.linha, pos.coluna] = true;
            }

            //#JOGADA ESPECIAL ROQUE
            //VERIFICA SE O REI JA EXECUTOU ALGUM MOVIMENTO OU SE ENCONTRA-SE EM XEQUE 
            if(qteMovimentos == 0 && !partida.xeque)
            {
                //jogada especial roque pequeno, posição onde deve haver uma torre
                Posicao posT1 = new Posicao(posicao.linha, posicao.coluna + 3);
                Posicao p1 = new Posicao(posicao.linha, posicao.coluna + 1);
                Posicao p2 = new Posicao(posicao.linha, posicao.coluna + 2);
                if (testeTorreParaRoque(posT1) && (tab.peca(p1) == null) && (tab.peca(p2) == null))
                {
                    mat[posicao.linha, posicao.coluna + 2] = true;
                }
                //jogada especial roque grande
                Posicao posT2 = new Posicao(posicao.linha, posicao.coluna - 4);
                Posicao p3 = new Posicao(posicao.linha, posicao.coluna - 1);
                Posicao p4 = new Posicao(posicao.linha, posicao.coluna - 2);
                Posicao p5 = new Posicao(posicao.linha, posicao.coluna - 3);
                if (testeTorreParaRoque(posT2) && (tab.peca(p3) == null) && (tab.peca(p4) == null) && (tab.peca(p5) == null))
                {
                    mat[posicao.linha, posicao.coluna - 2] = true;
                }

            }



            return mat;//retorna a matriz dos movimentos possiveis do rei 
        }
    }
}
