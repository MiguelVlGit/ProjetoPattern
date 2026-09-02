using System;
using System.Collections.Generic;
using PatternsDemo.Adapter;
using PatternsDemo.Visitor;

namespace PatternsDemo
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DemoAdapter();
            DemoVisitor();
        }

        // ---- Demo 1: Adapter — independente do Visitor ----
        static void DemoAdapter()
        {
            Titulo("PADRÃO ADAPTER — normalizando pedidos de duas fontes diferentes");

            var pedidoA = new PedidoFonteA
            {
                Item = "Parafuso M6",
                Qtd = 100,
                Preco = 0.35m
            };

            var pedidoB = new PedidoFonteB
            {
                Descricao = "Chapa de Aço 2mm",
                QuantidadeTexto = "20",
                ValorTexto = "45.90"
            };

            SubTitulo("PedidoFonteA (formato original)");
            Atributo("Item", pedidoA.Item);
            Atributo("Qtd", pedidoA.Qtd);
            Atributo("Preco", $"R$ {pedidoA.Preco:F2}");

            SubTitulo("PedidoFonteB (formato original — valores em texto)");
            Atributo("Descricao", pedidoB.Descricao);
            Atributo("QuantidadeTexto", pedidoB.QuantidadeTexto);
            Atributo("ValorTexto", pedidoB.ValorTexto);

            // a aplicação só enxerga IPedido — não sabe de qual fonte veio
            var pedidos = new List<IPedido>
            {
                new AdapterFonteA(pedidoA),
                new AdapterFonteB(pedidoB)
            };

            SubTitulo("Depois do Adapter -> IPedido.Normalizar()");
            foreach (var pedido in pedidos)
            {
                var normalizado = pedido.Normalizar();
                Atributo(normalizado.Nome, $"{normalizado.Quantidade}x R$ {normalizado.ValorUnitario:F2}");
            }
        }

        // ---- Demo 2: Visitor — independente do Adapter ----
        static void DemoVisitor()
        {
            Titulo("PADRÃO VISITOR — operações sobre itens de uma nota fiscal");

            var itens = new List<ItemNota>
            {
                new ItemNota { Nome = "Cabo de Rede", Quantidade = 5, ValorUnitario = 12.50m },
                new ItemNota { Nome = "Switch 8 portas", Quantidade = 2, ValorUnitario = 189.00m }
            };

            SubTitulo("Itens da nota");
            foreach (var item in itens)
            {
                Atributo(item.Nome, $"{item.Quantidade}x R$ {item.ValorUnitario:F2}");
            }

            SubTitulo("VisitorResumoTexto");
            var visitorResumo = new VisitorResumoTexto();
            foreach (var item in itens)
            {
                item.Aceitar(visitorResumo);
            }

            Console.WriteLine();
            SubTitulo("VisitorCalculoTotal");
            var visitorTotal = new VisitorCalculoTotal();
            foreach (var item in itens)
            {
                item.Aceitar(visitorTotal);
            }

            Console.WriteLine();
            Atributo("TOTAL DA NOTA", $"R$ {visitorTotal.Total:F2}");
        }

        // ---- helpers só de exibição, não fazem parte dos padrões ----

        static void Titulo(string texto)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('═', 60));
            Console.WriteLine($"  {texto}");
            Console.WriteLine(new string('═', 60));
            Console.ResetColor();
        }

        static void SubTitulo(string texto)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n▸ {texto}");
            Console.WriteLine(new string('─', 50));
            Console.ResetColor();
        }

        static void Atributo(string nome, object valor)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"   {nome,-14}: ");
            Console.ResetColor();
            Console.WriteLine(valor);
        }
    }
}
