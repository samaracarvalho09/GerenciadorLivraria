namespace GerenciadorLivraria.Communication.Responses;

public class ResponseRegistedBookJson
{

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty; //`ficção`, `romance`, `mistério`
    public int Price { get; set; }
    public int QuantityInStock { get; set; }

}
