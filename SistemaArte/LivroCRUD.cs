using System;
using System.Collections.Generic;

public class LivroCRUD
{
    private List<Livro> livros;

    public LivroCRUD()
    {
        this.livros = new List<Livro>();
    }

    public void Executar()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== CADASTRO / CONSULTA DE LIVROS ===");
        Console.ResetColor();

        Console.Write("\nDigite o ISBN: ");
        string isbn = Console.ReadLine();

        // procura livro existente
        Livro livroExistente = null;
        foreach (Livro l in livros)
        {
            if (l.isbn == isbn)
            {
                livroExistente = l;
                break;
            }
        }

        if (livroExistente == null)
        {
            Console.WriteLine("\nLivro não encontrado. Deseja cadastrar? (S/N)");
            string resp = Console.ReadLine().ToLower();

            if (resp == "s")
            {
                Livro novoLivro = new Livro();
                novoLivro.isbn = isbn;

                Console.Write("Título: ");
                novoLivro.titulo = Console.ReadLine();

                Console.Write("Autor: ");
                novoLivro.autor = Console.ReadLine();

                Console.Write("Ano de publicação: ");
                novoLivro.anoPublicacao = int.Parse(Console.ReadLine());

                Console.Write("Número de páginas: ");
                novoLivro.paginas = int.Parse(Console.ReadLine());

                livros.Add(novoLivro);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nLivro cadastrado com sucesso!");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nLivro encontrado:");
            Console.ResetColor();
            livroExistente.ImprimirDados();
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    public void ListarLivros()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== LISTA DE LIVROS ===\n");
        Console.ResetColor();

        if (livros.Count == 0)
        {
            Console.WriteLine("Nenhum livro cadastrado.");
        }
        else
        {
            foreach (Livro l in livros)
            {
                l.ImprimirDados();
            }
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
}
