using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            //FIXA UMA LINHA E PERCORRE TODAS AS COLUNAS ATÉ O SEU FIM, PULANDO PARA A PROXIMA COM O USO LOGICO DO FOR
            //E NA IMPRESSÃO AUXILIADO PELO Console.WriteLine()
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i+" ");//PRINTA O NUMERO DAS LINHAS
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        imprimirPeca(tab.peca(i,j));
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");//PRINTA AS LETRAS DA COLUNA
        }
        public static PosicaoXadrez lerPosicaoXadrez()
        {
            string s = Console.ReadLine();//digitará uma posição de xadrez
            char coluna = s[0];//RECEBERÁ O PRIMEIRO CARACTERE DO VALOR INSERIDO A-H
            int linha = int.Parse(s[1] + "");//COLETA O VALOR NUMERICO DA INSERÇÃO, FOI INSERIDO O "" DENTRO DOS 
                                             //PARENTESIS PARA FACILITAR A CONVERSÃO EFETUADA
            return new PosicaoXadrez(coluna, linha);
        }
        public static void imprimirPeca(Peca peca)
        {
            if (peca.cor == Cor.Branca)
            {
                Console.Write(peca);
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;//ARMAZENA O VALOR/CONFIGURAÇÃO DE COR ATUAL
                Console.ForegroundColor = ConsoleColor.DarkYellow;//MUDA A COR DE ESCRITA NO CONSOLE PARA AMARELO
                Console.Write(peca);//IMPRIME A PEÇA NA COR ACIMA 
                Console.ForegroundColor = aux;//RETONA A CONFIGURAÇÃO ANTERIOR DE COR DE IMPRESSÃO NA TELA 
            }
        }
    }
}
