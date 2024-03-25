using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TaskManager.Common.Dto;


namespace TaskManager.Common.Application.Services.SvcClient;


public interface ISvcClient
{
    Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest payload, CancellationToken ct = default) where TRequest : ISvcBase<TResponse>;
    Task<T> RequestAsync<T>(string service, string path, CancellationToken cancellationToken = default);
    Task<T> RequestAsync<T>(string service, string path, string authorisationHeader, CancellationToken cancellationToken = default);
    Task<T> RequestAsync<TBody, T>(string service, string path, TBody body, CancellationToken cancellationToken = default);
    Task<T> RequestAsync<TBody, T>(string service, string path, TBody body, string authorisationHeader, CancellationToken cancellationToken = default);
}
