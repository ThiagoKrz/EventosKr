using Microsoft.AspNetCore.Mvc;
using KrEventos.Domain;
using KrEventos.Persistence;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using KrEventos.Persistence.Contextos;

namespace KrEventos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableCors("AllowAccess_To_API")]
public class EventosController : ControllerBase
{
    private readonly KrEventosContext _context;

    public EventosController(KrEventosContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Evento> Get()
    {
        return _context.Eventos;
    }

    [HttpGet("{id}")]
    public Evento GetById(int id)
    {
        return _context.Eventos.FirstOrDefault(evento => evento.Id == id);
    }

    [HttpPost]
    public string Post()
    {
        return "Postado";
    }

    [HttpPut("{id}")]
    public string Put(int id)
    {
        return $"Exemplo de post com o id: {id}";
    }

    [HttpDelete]
    public string Delete()
    {
        return "Deletado";
    }
}
