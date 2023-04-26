import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HttpTransportType } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class SignalRService {
  constructor() {}

  initializeSignalRConnection = (accessToken) => {
    const connection = new HubConnectionBuilder()
      .withUrl('https://localhost:7077/connection', {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
        accessTokenFactory: () => `Bearer ${accessToken}`,
      })
      .build();

    connection
      .start()
      .then(() => {
        console.log('SignalR Connected!');
      })
      .catch((err) => console.error(err.toString()));

    return connection;
  };
}
