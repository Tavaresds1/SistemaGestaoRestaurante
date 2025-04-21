using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SistemaGestao.Models
{
    public enum StatusPedido
    {
        [Display(Name = "Em Preparo")]
        EmPreparo,

        [Display(Name = "Pronto para Entrega")]
        Pronto,

        [Display(Name = "Entregue")]
        Entregue,

        [Display(Name = "Cancelado")]
        Cancelado
    }

    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O n�mero da mesa � obrigat�rio")]
        [Display(Name = "N�mero da Mesa")]
        public string Mesa { get; set; } = string.Empty;

        [Required(ErrorMessage = "O nome do solicitante � obrigat�rio")]
        [Display(Name = "Nome do Cliente")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string NomeSolicitante { get; set; } = string.Empty;

        [Display(Name = "Status do Pedido")]
        public StatusPedido Status { get; set; } = StatusPedido.EmPreparo;

        [Display(Name = "Data/Hora do Pedido")]
        public DateTime DataHora { get; set; } = DateTime.Now;

        [Display(Name = "Observa��es")]
        [StringLength(500, ErrorMessage = "As observa��es n�o podem exceder 500 caracteres")]
        public string? Observacoes { get; set; }

        public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();

        [Display(Name = "Total do Pedido")]
        public decimal TotalPedido
        {
            get
            {
                return Itens.Sum(item => item.Quantidade * item.Produto?.Preco ?? 0);
            }
        }
    }
}