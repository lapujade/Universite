namespace Universite.Models.ViewModels
{
    public class CheckEnseigne
    {
        // Cette view model contient les données des checkbox pour les pages Enseignant
        public int UEID { get; set; }
        public string NomComplet { get; set; }
        public bool IsCheck { get; set; }
    }
}