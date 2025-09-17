using AutoMapper;
using FluentResults;
using FluentValidation;
using FluentValidation.Results;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloHospede.Commands;
using GestaoDeEstacionamento.Core.Aplicacao.ModuloHospede.Handlers;
using GestaoDeEstacionamento.Core.Dominio.Compartilhado;
using GestaoDeEstacionamento.Core.Dominio.ModuloAutenticacao;
using GestaoDeEstacionamento.Core.Dominio.ModuloHospede;
using Microsoft.Extensions.Logging;
using Moq;

[TestClass]
[TestCategory("Testes de Aplicacao de Hospede")]
public class CadastrarHospedeCommandHandlerTests
{
    private readonly Mock<IRepositorioHospede> _repoMock = new();
    private readonly Mock<ITenantProvider> _tenantMock = new();
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<IValidator<CadastrarHospedeCommand>> _cadastrarValidatorMock = new();
    private readonly Mock<ILogger<CadastrarHospedeCommandHandler>> _cadastrarLoggerMock = new();
    private readonly Mock<IValidator<EditarHospedeCommand>> _editarValidatorMock = new();
    private readonly Mock<ILogger<EditarHospedeCommandHandler>> _editarLoggerMock = new();  

    private CadastrarHospedeCommandHandler CriarCadastrarHandler()
    {
        return new CadastrarHospedeCommandHandler(
            _repoMock.Object,
            _tenantMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object,
            _cadastrarValidatorMock.Object,
            _cadastrarLoggerMock.Object
        );
    }

    private EditarHospedeCommandHandler CriarEditarHandler()
    {
        return new EditarHospedeCommandHandler(
            _repoMock.Object,
            _tenantMock.Object,
            _unitOfWorkMock.Object,
            _mapperMock.Object,
            _editarValidatorMock.Object,
            _editarLoggerMock.Object
            );
    }

    [TestMethod]
    public async Task Handle_DeveFalhar_QuandoValidacaoInvalida()
    {
        // Arrange
        var command = new CadastrarHospedeCommand("a", "12345678900");

        _cadastrarValidatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult(new List<ValidationFailure>
                      {
                          new ValidationFailure("Nome", "O nome é obrigatório.")
                      }));

        var handler = CriarCadastrarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsFailed);
        Assert.AreEqual("Requisição inválida", result.Errors.First().Message); 
        _repoMock.Verify(r => r.CadastrarAsync(It.IsAny<Hospede>()), Times.Never);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Never);
    }

    [TestMethod]
    public async Task Handle_DeveCadastrarHospede_QuandoValidacaoValida()
    {
        // Arrange
        var command = new CadastrarHospedeCommand("Maria", "98765432100");
        var hospede = new Hospede { Id = Guid.NewGuid(), Nome = command.Nome, CPF = command.CPF };

        _cadastrarValidatorMock.Setup(v => v.ValidateAsync(command, It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult());

        _mapperMock.Setup(m => m.Map<Hospede>(command)).Returns(hospede);
        _mapperMock.Setup(m => m.Map<CadastrarHospedeResult>(hospede))
                   .Returns(new CadastrarHospedeResult(hospede.Id));

        _tenantMock.Setup(t => t.UsuarioId).Returns(Guid.NewGuid());

        var handler = CriarCadastrarHandler();

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsTrue(result.IsSuccess);
        Assert.AreEqual(hospede.Id, result.Value.Id);

        _repoMock.Verify(r => r.CadastrarAsync(It.Is<Hospede>(h => h.Nome == "Maria")), Times.Once);
        _unitOfWorkMock.Verify(u => u.CommitAsync(), Times.Once);
    }

}
