namespace VesteTemplateWorker.Tests.Bases;

/// <summary>
/// Interface para criação de moq de objetos
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IFake<T>
{
    T? GerarEntidadeValida();
    T GerarEntidadeInvalida();
}
