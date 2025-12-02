namespace MagazinOnline.Api.Models
{
    //reprezinta un produs din magazinul online
    public class Product
    {
        // id = cheia principala in DB (Primary key)
        public int Id {get; set;}
        // Numele produsul ( ex : tablou)
        public string Name {get; set;} = string.Empty;
        //Descrierea produsului
        public string Description {get; set;} = string.Empty;       
        //Pretul produsului
        public float Price {get; set;}     
        public int   Stock {get;set;}
        //link-ul catre o poza a produsului
        public string ImageUrl {get;set;} = string.Empty;

    }
}