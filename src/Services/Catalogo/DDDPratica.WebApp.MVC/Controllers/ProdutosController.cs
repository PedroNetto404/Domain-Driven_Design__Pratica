using DDDPratica.Catalogo.Application.Serviços;
using Microsoft.AspNetCore.Mvc;

namespace DDDPratica.WebApp.MVC.Controllers;

public class ProdutosController : Controller
{
    private IProdutoAppService _produtoAppService;

    public ProdutosController(IProdutoAppService produtoAppService)
    {
        _produtoAppService = produtoAppService;
    }
}