using DDDPratica.Catalogo.Application.Serviços;
using DDDPratica.Core.Communication.Mediator;
using DDDPratica.Core.Mensagens.CommonMessages.Notifications;
using DDDPratica.Vendas.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDPratica.WebApps.MVC.Controllers;

public class CarrinhoController : BaseController
{
    private readonly IProdutoAppService _produtoAppService;
    private readonly IMediatorHandler _mediatorHandler;

    public CarrinhoController(
        IProdutoAppService produtoAppService,
        IMediatorHandler mediatorHandler,
        INotificationHandler<DomainNotification> notificationHandler
    ) : base(notificationHandler, mediatorHandler)
    {
        _produtoAppService = produtoAppService;
        _mediatorHandler = mediatorHandler;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost("meu-carrinho")]
    public async Task<IActionResult> AdicionarItem(Guid id, int quantidade)
    {
        var produto = await _produtoAppService.ObterPorId(id);
        if (produto == null) return BadRequest();

        if (produto.QuantidadeEstoque < quantidade)
        {
            TempData["Erro"] = "Produto com estoque insuficiente";
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        var command = new AdicionarItemPedidoCommand
        (
            Guid.NewGuid(), // TODO Trocar pela claim do usuário
            produto.Id,
            produto.Nome,
            quantidade,
            produto.Valor
        );

        await _mediatorHandler.EnviarComando(command);

        if (OperacaoValida())
        {
            return RedirectToAction("Index"); 
        }
        
        TempData["Erros"] = ObterMensagensErro(); //Toda vez que damos um redirect perdemos o estado 
                                                   //do request anteriro. Para contornar isto, usamos o TempData
        return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
    }
}