import { Injectable, signal } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection: signalR.HubConnection;
  private connectionPromise: Promise<void>;
  public messages = signal<string[]>([]);

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7219/chathub') // Make sure this matches your server endpoint
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.connectionPromise = this.hubConnection.start()
      .catch(err => {
        console.error('SignalR connection error:', err.toString());
      });

    this.hubConnection.on('ReceiveMessage', (message: string) => {
      this.messages.update((currentMessages: any) => {
        currentMessages.push(message);
        return currentMessages;
      });
    });
  }

  public async sendMessage(message: string, userId?: string, groupName?: string) {
    await this.connectionPromise; // Ensure connection is established

    if (this.hubConnection.state !== signalR.HubConnectionState.Connected) {
      console.error("SignalR connection is not in a connected state.");
      return;
    }

    if (userId) {
      this.hubConnection.invoke('SendPrivateMessage', userId, message)
        .catch(err => console.error(err));
    } else if (groupName) {
      this.hubConnection.invoke('SendGroupMessage', groupName, message)
        .catch(err => console.error(err));
    } else {
      this.hubConnection.invoke('SendBroadcastMessage', message)
        .catch(err => console.error(err));
    }
  }
}
