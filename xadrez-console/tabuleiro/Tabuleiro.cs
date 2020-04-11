using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace tabuleiro
{
    class Tabuleiro
    {
        public int linhas { get; set; }
        public int colunas { get; set; }
        /*
         * matriz de peças no tabileiro (matriz do tipo peças), 
         * private para não ser acessada externamente, somente
         * a classe pode modificar
         */
        private Peca[,] pecas;
        public Tabuleiro(int linhas, int colunas)
        {
            this.linhas = linhas;
            this.colunas = colunas;
            pecas = new Peca[linhas, colunas];
        }
        //RETORNA SE EM DETERMINADA POSIÇÃO EXISTE OU NÃO ALGUMA PEÇA 
        public Peca peca(int linha, int coluna)
        {
            return pecas[linha, coluna];
        }
        /*
         * A OPERAÇÃO DE COLOCAR PEÇA ENVOLVE COLOCAR UMA PEÇA DEFINIDA COM O NOME DE P DENTRO DA MATRIZ PECAS
         * EM SUA POSIÇÃO X,Y SIMBOLIZADO POR LINHA,COLUNA(LINHA 35)
         * TAMBÉM INFORMAR A ESTA DETERMINADA PEÇA SUA POSIÇÃO DENTRO DO TABULEIRO POIS LEMBRANDO CADA PEÇA POSSUI
         * UMA POSIÇÃO DADA POR DETERMINADA LINHA E COLUNA 
         */
        public void colocarPeca(Peca p, Posicao pos)
        {
            pecas[pos.linha, pos.coluna] = p;
            p.Posicao = pos;
        }
    }
}
