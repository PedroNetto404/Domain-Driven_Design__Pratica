using DDDPratica.Catalogo.Application.Serviços;
using DDDPratica.Core.Communication.Mediator;
using DDDPratica.Core.Mensagens.CommonMessages.Notifications;
using DDDPratica.Vendas.Application.Commands;
using DDDPratica.Vendas.Application.Commands.DTO_s;
using DDDPratica.Vendas.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DDDPratica.WebApps.MVC.Controllers;

public class CarrinhoController : BaseController
{
    private readonly IProdutoAppService _produtoAppService;
    private readonly IPedidoQueriesFacade _pedidoQueriesFacade;
    private readonly IMediatorHandler _mediatorHandler;

    public CarrinhoController
    (
        IProdutoAppService produtoAppService,
        IMediatorHandler mediatorHandler,
        INotificationHandler<DomainNotification> notificationHandler,
        IPedidoQueriesFacade pedidoQueriesFacade
    ) : base(notificationHandler, mediatorHandler)
    {
        _produtoAppService = produtoAppService;
        _mediatorHandler = mediatorHandler;
        _pedidoQueriesFacade = pedidoQueriesFacade;
    }

    [Route("meu-carrinho")]
    public async Task<IActionResult> Index()
    {
        return View(await _pedidoQueriesFacade.ObterCarrinhoCliente(Guid.NewGuid()));//TODO Cookies
    }

    [HttpPost("remover-item")]
    public async Task<IActionResult> RemoverItem(Guid id)
    {
        var produto = await _produtoAppService.ObterPorId(id);
        if (produto == null) return BadRequest(); 
        
        var command =  new RemoverItemPedidoCommand(); 
        await _mediatorHandler.EnviarComando(command);

        if (OperacaoValida())
        {
            return RedirectToAction("Index"); 
        }

        return View("Index", await _pedidoQueriesFacade.ObterCarrinhoCliente(Guid.NewGuid())); 
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

    [HttpPost("atualizar-item")]
    public async Task<IActionResult> AtualizarItem(Guid id, int quantidade)
    {
        var produto = await _produtoAppService.ObterPorId(id);
        if (produto == null) return BadRequest();

        var command = new AtualizarItemPedidoCommand(Guid.NewGuid(), id, quantidade);
        await _mediatorHandler.EnviarComando(command);

        if (OperacaoValida())
            return RedirectToAction("Index");

        return View("Index", await _pedidoQueriesFacade.ObterCarrinhoCliente(Guid.NewGuid())); 
    }

    [HttpPost("aplicar-voucher")]
    public async Task<IActionResult> AplicarVoucher(string voucherCodigo)
    {
        var command = new AplicarVoucherPedidoCommand(Guid.NewGuid(), voucherCodigo);

        await _mediatorHandler.EnviarComando(command);

        if (OperacaoValida()) return RedirectToAction("Index");

        return View("Index", await _pedidoQueriesFacade.ObterCarrinhoCliente(Guid.NewGuid())); 
    }
    
}