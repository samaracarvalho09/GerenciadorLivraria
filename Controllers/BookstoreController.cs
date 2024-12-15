using GerenciadorLivraria.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using GerenciadorLivraria.Communication.Requests;

namespace GerenciadorLivraria.Controllers;
[Route("api/[controller]")]
[ApiController]
public class BookstoreController : ControllerBase
{
    // Simulando um banco de dados em memória:
    private static readonly Dictionary<int, ResponseRegistedBookJson> Books = new()
    {
        { 1, new ResponseRegistedBookJson
            {
                Id = 1,
                Title = "Teste 1",
                Author = "Samara Carvalho",
                Gender = "Ação",
                Price = 89,
                QuantityInStock = 15
            }
        },
        { 2, new ResponseRegistedBookJson
            {
                Id = 2,
                Title = "Teste 2",
                Author = "Mara Lúcia.",
                Gender = "Romance",
                Price = 45,
                QuantityInStock = 9
            }
        },
        { 3, new ResponseRegistedBookJson
            {
                Id = 3,
                Title = "Teste 3",
                Author = "Caio B.",
                Gender = "Ação",
                Price = 109,
                QuantityInStock = 52
            }
        },
         { 4, new ResponseRegistedBookJson
            {
                Id = 4,
                Title = "Teste 4",
                Author = "Rebeca C.",
                Gender = "Ação",
                Price = 19,
                QuantityInStock = 5
            }
        }
    };

    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseRegistedBookJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)] 

    public IActionResult Get()
    {
           return Ok(Books);
    }


    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegistedBookJson), StatusCodes.Status201Created)]
    public IActionResult RegisterBook(RequestRegisterBookJson request)

    {
        var newId = Books.Keys.Max() + 1; // Gera um novo ID baseado no maior ID atual
        var response = new ResponseRegistedBookJson
        
        {
            Id = newId,
            Title = request.Title,
            Author = request.Author,
            Gender = request.Gender,
            Price = request.Price,
            QuantityInStock = request.QuantityInStock,
        };

        Books.Add(newId, response);

        return CreatedAtAction(nameof(Get), new { id = newId }, response);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ResponseRegistedBookJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public IActionResult Put(int id, [FromBody] RequestRegisterBookJson request)
    {
        Console.WriteLine(Books);
        if (!Books.ContainsKey(id))
        {
            return (NotFound("Livro não encontrado"));
        }

        var bookToUpdate = Books[id];
        bookToUpdate.Title = request.Title;
        bookToUpdate.Author = request.Author;
        bookToUpdate.Gender = request.Gender;
        bookToUpdate.Price = request.Price;
        bookToUpdate.QuantityInStock = request.QuantityInStock;
        Console.WriteLine(Books);
        return Ok(bookToUpdate);
    }


    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]

    public IActionResult Delete(int id)
    {
        if (Books.ContainsKey(id))
        {
            Books.Remove(id);
            return NoContent();
        }

        return NotFound("Book not found");
    }
}
