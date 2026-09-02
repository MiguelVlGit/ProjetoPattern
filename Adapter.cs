using System.Globalization;

namespace PatternsDemo.Adapter
{
    // Modelo unificado que a aplicação usa — pertence só ao Adapter, não é compartilhado com o Visitor
    public class PedidoNormalizado
    {
        public string Nome = "";
        public int Quantidade;
        public decimal ValorUnitario;
    }

    // Formato da Fonte A — já vem com os nomes de campo "certos", mas em inglês/abreviado
    public class PedidoFonteA
    {
        public string Item = "";
        public int Qtd;
        public decimal Preco;
    }

    // Formato da Fonte B — nomes diferentes e valores vêm como texto
    public class PedidoFonteB
    {
        public string Descricao = "";
        public string QuantidadeTexto = "";
        public string ValorTexto = "";
    }

    // Interface que a aplicação conhece — não importa a fonte do pedido
    public interface IPedido
    {
        PedidoNormalizado Normalizar();
    }

    // Adapter 1: traduz o formato da Fonte A para PedidoNormalizado
    public class AdapterFonteA : IPedido
    {
        private readonly PedidoFonteA _origem;

        public AdapterFonteA(PedidoFonteA origem)
        {
            _origem = origem;
        }

        public PedidoNormalizado Normalizar()
        {
            return new PedidoNormalizado
            {
                Nome = _origem.Item,
                Quantidade = _origem.Qtd,
                ValorUnitario = _origem.Preco
            };
        }
    }

    // Adapter 2: traduz o formato da Fonte B para PedidoNormalizado
    public class AdapterFonteB : IPedido
    {
        private readonly PedidoFonteB _origem;

        public AdapterFonteB(PedidoFonteB origem)
        {
            _origem = origem;
        }

        public PedidoNormalizado Normalizar()
        {
            return new PedidoNormalizado
            {
                Nome = _origem.Descricao,
                Quantidade = int.Parse(_origem.QuantidadeTexto),
                ValorUnitario = decimal.Parse(_origem.ValorTexto, CultureInfo.InvariantCulture)
            };
        }
    }
}
