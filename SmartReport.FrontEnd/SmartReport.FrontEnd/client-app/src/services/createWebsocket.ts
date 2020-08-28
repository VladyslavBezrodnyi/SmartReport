import {
    JsonHubProtocol,
    HttpTransportType,
    HubConnectionBuilder,
    LogLevel,
    HubConnection
} from '@microsoft/signalr';

const createWebsocket = (url: string, access_token: string): HubConnection => {
    const protocol = new JsonHubProtocol();
    const transport = HttpTransportType.WebSockets | HttpTransportType.ServerSentEvents | HttpTransportType.LongPolling;

    const options = {
        transport,
        //skipNegotiation: true,
        //logMessageContent: true,
        logger: LogLevel.Trace,
        accessTokenFactory: () => access_token
    };

    const hubConnection = new HubConnectionBuilder()
        .withUrl(url, options)
        .withHubProtocol(protocol)
        .build();

    return hubConnection;
}

export default createWebsocket;