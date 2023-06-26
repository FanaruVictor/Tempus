import { Injectable } from '@angular/core';
import {
  HubConnection,
  HubConnectionBuilder,
  HubConnectionState,
  LogLevel,
} from '@microsoft/signalr';
import { environment } from '../../environments/environment';
import { NotificationService } from './notification.service';
import { GroupService } from './group/group.service';
import { ClientEvent, ResponseType } from '../_commons/models/client-event';
import { RegistrationOverview } from '../_commons/models/registrations/registrationOverview';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class ClientEventsService {
  private readonly reconnectIntervals: number[];
  private retryConnectionInterval: any;
  private retryConnectionIntervalIndex = 0;
  private retryIntervalMs: number = -1;
  private eventHubConnection!: HubConnection;
  private connectionUrl = environment.apiUrl.replace('/api', '');

  constructor(
    private notificationService: NotificationService,
    private groupService: GroupService,
    private router: Router
  ) {
    if (!environment.production) {
      this.reconnectIntervals = new Array(720);
      this.reconnectIntervals.fill(5000, 0, 720);
      return;
    }

    this.reconnectIntervals = [0, 2000, 5000, 10000, 30000, 60000, 300000];
  }

  startConnection = (accessToken) => {
    if (
      this.eventHubConnection &&
      this.eventHubConnection.state === HubConnectionState.Connected
    ) {
      console.error('Tried to connect but already connected');
      return;
    }
    this.eventHubConnection = new HubConnectionBuilder()
      .withUrl(`${this.connectionUrl}/hub`, {
        accessTokenFactory: () => `${accessToken}`,
      })
      .withAutomaticReconnect(this.reconnectIntervals)
      .configureLogging(LogLevel.Information)
      .build();

    this.eventHubConnection.serverTimeoutInMilliseconds = 2 * 60 * 60 * 1000;
    this.eventHubConnection.keepAliveIntervalInMilliseconds = 5000;

    this.establishConnection();
  };

  stopConnection() {
    if (
      this.eventHubConnection &&
      this.eventHubConnection.state === HubConnectionState.Connected
    ) {
      this.eventHubConnection
        .stop()
        .then(() => console.warn('Connection to client events Hub was closed'))
        .catch((reason) =>
          console.error(`Error while disconnecting from events hub: ${reason}`)
        );
    }
  }

  private initializeListener(): void {
    this.eventHubConnection.on('client-events', (data: ClientEvent) => {
      var response = JSON.parse(data.innerEventJson);
      debugger;
      switch (data.responseType) {
        case ResponseType.AddRegistration:
          this.addRegistration(response);
          break;
        case ResponseType.DeleteRegistration:
          this.deleteRegistration(response);
          break;
        case ResponseType.UpdateRegistration:
          this.showUpdatedRegistrationMessage(response);
      }
    });
  }

  private addRegistration(response) {
    var registration: RegistrationOverview = {
      id: response.Id,
      categoryColor: response.CategoryColor,
      content: response.Content,
      description: response.Description,
      lastUpdatedAt: response.LastUpdatedAt,
    };

    this.groupService.addRegistration(registration);
  }

  private deleteRegistration(response) {
    this.groupService.deleteRegistration(response.RegistrationId);

    if (
      this.router.url.includes(
        `/groups/${response.GroupId}/notes/${response.RegistrationId}`
      )
    ) {
      this.router.navigate([`/groups/${response.GroupId}/notes`]);
    }
  }

  private showUpdatedRegistrationMessage(response: any) {
    debugger
    const registration: RegistrationOverview = {
      id: response.Registration.Id,
      categoryColor: response.Registration.CategoryColor,
      content: response.Registration.Content,
      description: response.Registration.Description,
      lastUpdatedAt: response.Registration.LastUpdatedAt,
    };
    if (
      this.router.url.includes(
        `/groups/${response.GroupId}/notes/${registration.id}`
      )
    ) {
      this.notificationService.showRegistrationUpdatedMessage(
        response.Message,
        'Registration already updated'
      );
    } else {
      this.groupService.deleteRegistration(registration.id);
      this.groupService.addRegistration(registration);
    }
  }

  private establishConnection() {
    this.eventHubConnection
      .start()
      .then(() => {
        this.initializeListener();
        this.onConnected(this.eventHubConnection.connectionId);
      })
      .catch((err) => this.onConnectionFailure(err));
  }

  private onConnected(connectionId: string | null): void {
    if (!connectionId) {
      console.error('ConnectionId is null');
      return;
    }
    this.retryConnectionIntervalIndex = 0;
  }

  private onConnectionFailure(error): void {
    if (!this.retryConnectionInterval) {
    }
    console.error('Error while starting connection with events hub: ', error);

    this.retryIntervalMs =
      this.reconnectIntervals[this.retryConnectionIntervalIndex];

    clearInterval(this.retryConnectionInterval);
    this.retryConnectionIntervalIndex++;

    if (this.retryIntervalMs === null) {
      return;
    }

    this.retryConnectionInterval = setInterval(
      () => this.establishConnection(),
      this.retryIntervalMs
    );
  }
}
