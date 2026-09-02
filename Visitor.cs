using System;

namespace PatternsDemo.Visitor
{
    // Item de nota fiscal — domínio próprio do Visitor, não depende do Adapter
    public class ItemNota
    {
        public string Nome { get; set; } = "";
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }

        // "aqui estou eu, faça o que precisar comigo"
        public void Aceitar(IItemVisitor visitor) => visitor.Visit(this);
    }

    public interface IItemVisitor
    {
        void Visit(ItemNota item);
    }

    // Visitor 1: soma o total, com imposto embutido
    public class VisitorCalculoTotal : IItemVisitor
    {
        public decimal Total { get; private set; }

        public void Visit(ItemNota item)
        {
            decimal totalItem = item.ValorUnitario * item.Quantidade * 1.1m; // +10% imposto
            Total += totalItem;
            Console.WriteLine($"   [Total] {item.Nome}: R$ {totalItem:F2}  ({item.Quantidade}x R$ {item.ValorUnitario:F2} + 10% imposto)");
        }
    }

    // Visitor 2: monta um resumo em texto dos itens
    public class VisitorResumoTexto : IItemVisitor
    {
        public string Texto { get; private set; } = "";

        public void Visit(ItemNota item)
        {
            string linha = $"{item.Nome} x{item.Quantidade}";
            Texto += linha + "\n";
            Console.WriteLine($"   [Resumo] {linha}");
        }
    }
}
