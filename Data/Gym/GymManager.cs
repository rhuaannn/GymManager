using System.ComponentModel.DataAnnotations;

namespace GymManager.Data.Gym
{
    public class GymManager
    {
        [Key]
        public int Id { get; set; } 

        [Required(ErrorMessage = "O ID do aluno é obrigatório")]
        public string IdAcademia { get; set; }

        [Required(ErrorMessage = "O GymId é obrigatório")]
        public string GymId { get; set; }

        public string CriadoPorUsuario { get; set; }

        public DateTime DataCriacao { get; set; } = DateTime.Now;

        public EnumStatus Status { get; set; } = EnumStatus.Novo;
    }
}

