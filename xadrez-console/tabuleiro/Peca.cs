using tabuleiro;

namespace tabuleiro
{
    class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }
        public Peca(Tabuleiro tab, Cor cor)
        {
            Posicao = null;
            this.cor = cor;
            this.tab = tab;
            this.qteMovimentos = 0;
        }
    }
}
