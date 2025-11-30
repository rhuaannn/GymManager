namespace GymManager.Models
{
    public class RegistroPaginado
    {
        public List<Data.Gym.GymManager> Itens { get; set; } = new();
        public int TotalRegistros { get; set; }
    }
}