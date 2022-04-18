// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace System.Web.Adapters.SessionState;

internal class RemoteAppSessionStateManager : ISessionManager
{
    private readonly IOptions<RemoteAppSessionStateOptions> _options;
    private readonly RemoteSessionService _remoteSessionService;
    private readonly ILoggerFactory _loggerFactory;

    public RemoteAppSessionStateManager(
        IOptions<RemoteAppSessionStateOptions> options,
        RemoteSessionService remoteSessionService,
        ILoggerFactory loggerFactory)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _remoteSessionService = remoteSessionService ?? throw new ArgumentNullException(nameof(remoteSessionService));
        _loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public async Task<ISessionState> CreateAsync(HttpContextCore context, bool readOnly) =>
        await RemoteSessionState.CreateAsync(context, readOnly, _remoteSessionService, _options.Value, _loggerFactory.CreateLogger<RemoteSessionState>());
}
