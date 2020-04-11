using tabuleiro;

namespace xadrez
{
    class PosicaoXadrez
    {
        public char coluna { get; set; }
        public int linha { get; set; }

        public PosicaoXadrez(char coluna, int linha)
        {
            this.coluna = coluna;
            this.linha = linha;
        }
        /*
         * ESTE MÉTODO EFETUA A CONVERSÃO DA ENTRADA DE XADREZ PARA UMA ENTRADA DE MATRIZ, QUANDO O REFERE-SE A LINHA
         * TODA A POSIÇÃO DE XADREZ INSERIDA SE SUBTRAIDA DE 8 RETORNA A POSIÇÃO CORRESPONDENTE DENTRO DA MATRIZ DO 
         * TABULEIRO. TEMOS QUE 'a' REPRESENTA UM VALOR DENTRO DA TABELA UNICODE, AO PEGARMOS O VALOR INSERIDO E
         * SUBTRAIRMOS 'a' SERÁ POSSIVEL EFETUAR UMA CONVERSÃO DAS COLUNAS DA MATRIZ DO TABULEIRO DE XADREZ 
         */
        public Posicao toPosicao()
        {
            return new Posicao(8 - linha, coluna - 'a');
        }
        public override string ToString()
        {
            //O "" MACETE PARA FAZER A IMPRESSÃO SEM USO DE OUTRAS ESTRUTURAS (FORÇA A CONVERSÃO PARA STRING)
            return ""+coluna+linha;
        }
    }
}
