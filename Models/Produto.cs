using System.ComponentModel.DataAnnotations;

namespace SistemaGestao.Models;

    public enum TipoProduto
    {
        [Display(Name = "Prato")]
        Prato,
        [Display(Name = "Bebida")]
        Bebida,
    }

    public class Produto
    {
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public TipoProduto Tipo { get; set; } = TipoProduto.Prato;
    public decimal Preco { get; set; }
    }
