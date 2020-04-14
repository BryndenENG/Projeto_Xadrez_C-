using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void imprimirPartida(PartidaDeXadrez partida)
        {
            Tela.ImprimirTabuleiro(partida.tab);
            Console.WriteLine();
            imprimirPecasCapturadas(partida);
            Console.WriteLine();
            Console.WriteLine("Turnos: " + partida.turno);
            Console.WriteLine("Aguardando jogada: " + partida.jogadorAtual);
        }
        public static void imprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            imprimirConjunto(partida.pecasCapturadas(Cor.Preta));
            Console.WriteLine();
        }
        public static void imprimirConjunto(HashSet<Peca> conjunto)
        {
            Console.Write("[");
            foreach(Peca x in conjunto)
            {
                Console.Write(x+" ");
            }
            Console.WriteLine("]");
        }


        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            //FIXA UMA LINHA E PERCORRE TODAS AS COLUNAS ATÉ O SEU FIM, PULANDO PARA A PROXIMA COM O USO LOGICO DO FOR
            //E NA IMPRESSÃO AUXILIADO PELO Console.WriteLine()
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i+" ");//PRINTA O NUMERO DAS LINHAS
                for (int j = 0; j < tab.colunas; j++)
                {
                    imprimirPeca(tab.peca(i,j));  
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");//PRINTA AS LETRAS DA COLUNA
        }
        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            //armazenar a configuração do fundo de onde esta sendo escrito 
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            //nova cor referente as possiveis movimentações de peças
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
            for (int i = 0; i < tab.linhas; i++)
            {
                Console.Write(8 - i + " ");//PRINTA O NUMERO DAS LINHAS
                for (int j = 0; j < tab.colunas; j++)
                {
                    //logica para mudança de cor do fundo para posições possiveis 
                    if (posicoesPossiveis[i,j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    imprimirPeca(tab.peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");//PRINTA AS LETRAS DA COLUNA
            //garantir que a cor retorne ao valor inicial
            Console.BackgroundColor = fundoOriginal;
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
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
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
                Console.Write(" ");
            }
        }
    }
}
