using System;
using tabuleiro;

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
                for (int j = 0; j < tab.colunas; j++)
                {
                    if (tab.peca(i,j) == null)
                    {
                        Console.Write("_ ");
                    }
                    else
                    {
                        Console.Write(tab.peca(i,j)+" ");
                    }
                }
                Console.WriteLine();
            } 
        }
    }
}
