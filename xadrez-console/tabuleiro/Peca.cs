using tabuleiro;

namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }
        public Peca(Tabuleiro tab, Cor cor)
        {
            posicao = null;
            this.cor = cor;
            this.tab = tab;
            this.qteMovimentos = 0;
        }
        public void incrementarQteMovimetos()
        {
            qteMovimentos++;
        }
        public bool existeMovimentosPossiveis()
        {
            bool[,] mat = movimentosPossiveis();//recebe a matriz de movimentos possiveis 
            //PERCORRERÁ TODAS AS LINHAS DA MATRIZ DE MOVIMENTOS POSSIVEIS CHECANDO ALGUM VALOR POSITIVO
            for (int i = 0; i < tab.linhas; i++)
            {
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (mat[i,j])//caso encontre algum valor positivo returna true 
                    {
                        return true;
                    }
                }
            }
            return false;//se nenhum valor positivo for encontrado retorna false 
        }
        //MELHORA A LEITURA DE POSIÇÃO DE DESTINO POSSIVEL
        public bool podeMoverPara(Posicao pos)
        {
            return movimentosPossiveis()[pos.linha, pos.coluna];
        }
        public abstract bool[,] movimentosPossiveis();
    }
}
