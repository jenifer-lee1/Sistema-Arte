using System;
using System.Collections.Generic;

public class ObraCRUD
{
    private List<Obra> obras;

    public ObraCRUD()
    {
        this.obras = new List<Obra>();
    }

    public void Executar()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Cadastro / Consulta de Obras");
        Console.ResetColor();

        Console.Write("\nDigite o codigo: ");
        string codigo = Console.ReadLine();

        // procura obra existente
        Obra obraExistente = null;
        foreach (Obra l in obras)
        {
            if (l.codigo == codigo)
            {
                obraExistente = l;
                break;
            }
        }

        if (obraExistente == null)
        {
            Console.WriteLine("\nObra não encontrado no sistema! Deseja cadastrar? (S/N)");
            string resp = Console.ReadLine().ToLower();

            if (resp == "s")
            {
                Obra novoObra = new Obra();
                novoObra.codigo = codigo;

                Console.Write("Nome: ");
                novoObra.nome = Console.ReadLine();

                Console.Write("Autor/Pintor: ");
                novoObra.autor = Console.ReadLine();

                Console.Write("Ano da criação: ");
                novoObra.anoCriacao = int.Parse(Console.ReadLine());

                Console.Write("Valor estimado: ");
                novoObra.valor = int.Parse(Console.ReadLine());

                obras.Add(novoObra);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nObra cadastrada com sucesso!");
                Console.ResetColor();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nObra encontrado:");
            Console.ResetColor();
            obraExistente.ImprimirDados();
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }

    public void ListarLivros()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Lista de Obras\n");
        Console.ResetColor();

        if (obras.Count == 0)
        {
            Console.WriteLine("Nenhuma obra cadastrada!");
        }
        else
        {
            foreach (Obra l in obras)
            {
                l.ImprimirDados();
            }
        }

        Console.WriteLine("Pressione qualquer tecla para voltar ao menu...");
        Console.ReadKey();
    }
}
