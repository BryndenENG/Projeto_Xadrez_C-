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
        //SOBRECARGA DO MÉTODO ACIMA 
        public Peca peca(Posicao pos)
        {
            return pecas[pos.linha, pos.coluna];
        }
        public bool existePeca(Posicao pos)
        {
            validarPosicao(pos); 
            return peca(pos) != null;
        }
        /*
         * A OPERAÇÃO DE COLOCAR PEÇA ENVOLVE COLOCAR UMA PEÇA DEFINIDA COM O NOME DE P DENTRO DA MATRIZ PECAS
         * EM SUA POSIÇÃO X,Y SIMBOLIZADO POR LINHA,COLUNA(LINHA 35)
         * TAMBÉM INFORMAR A ESTA DETERMINADA PEÇA SUA POSIÇÃO DENTRO DO TABULEIRO POIS LEMBRANDO CADA PEÇA POSSUI
         * UMA POSIÇÃO DADA POR DETERMINADA LINHA E COLUNA 
         */
        public void colocarPeca(Peca p, Posicao pos)
        {
            if (existePeca(pos))
            {
                throw new TabuleiroException("Já existe uma peça nesta posição");
            }
            pecas[pos.linha, pos.coluna] = p;
            p.Posicao = pos;
        }
        /*
         * A PEÇA SERÁ RETIRADA E RETORNADA ALGUM USO POSTERIOR
         */
        public Peca retirarPeca(Posicao pos)
        {
            if (peca(pos) == null)//TESTA SE EXISTE PEÇA NA POSIÇÃO DO TABULEIRO, CASO NÃO EXISTA RETORNA NULO
            {
                return null;
            }
            //CASO PASSO DO TESTE ACIMA, SIGNIFICA A EXISTENCIA DA PEÇA NO TABULEIRO
            Peca aux = peca(pos);//A VARIAVEL AUX ARMAZENA DADOS DA PEÇA QUE SE ENCONTRA NO TABULEIRO
            aux.Posicao = null;//A POSIÇÃO DA PEÇA RECEBE VALOR NULO
            pecas[pos.linha, pos.coluna] = null;//RECEBE VALOR NULO ESVAZIANDO ASSIM A POSIÇÃO DO TABULEIRO (MATRIZ)
            return aux;//RETORNA OS DADOS DA PEÇA
        }
        /*
         * EFETUA O TESTE SE A POSIÇÃO INFORMADA É VALIDA PARA OS PARAMETROS DO TAMANHO DO TABULEIRO 
         */
        public bool posicaoValida(Posicao pos)
        {
            if (pos.linha < 0 || pos.linha >= linhas || pos.coluna < 0 || pos.coluna >= colunas)
            {
                return false;
            }
            return true;
        }
        /*
         * EFETUA A VALIDAÇÃO DA POSIÇÃO E TRATA DE POSSIVEIS ERROS COM EXCEÇÕES PERSONALIZADAS
         */
         public void validarPosicao(Posicao pos)
        {
            if (!posicaoValida(pos))
            {
                throw new TabuleiroException("Posição invalida!");
            }

        }
    }
}
