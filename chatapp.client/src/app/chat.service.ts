import { Injectable, signal } from '@angular/core';
import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  private hubConnection: signalR.HubConnection | null = null;
  public messages = signal<string[]>([]);

  constructor() { }

  public async connect(): Promise<void> {
    const token = localStorage.getItem('jwtToken');

    if (!token) {
      console.error('No JWT token found, cannot connect to SignalR hub.');
      return;
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:7219/chathub', {
        accessTokenFactory: () => token
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.hubConnection.on('ReceiveMessage', (message: string) => {
      this.messages.update((current) => [...current, message]);
    });

    try {
      await this.hubConnection.start();
      console.log('SignalR connected');
    } catch (err) {
      console.error('SignalR connection error:', err);
    }
  }

  public async sendMessage(message: string, userId?: string, groupName?: string) {
    if (!this.hubConnection || this.hubConnection.state !== signalR.HubConnectionState.Connected) {
      console.error('SignalR connection not established');
      return;
    }

    try {
      if (userId) {
        await this.hubConnection.invoke('SendPrivateMessage', userId, message);
      } else if (groupName) {
        await this.hubConnection.invoke('SendMessageToGroup', groupName, message);
      } else {
        await this.hubConnection.invoke('SendBroadCastMessage', message);
      }
    } catch (err) {
      console.error('Error sending message:', err);
    }
  }

  public disconnect(): void {
    this.hubConnection?.stop().catch(err => console.error('Error disconnecting SignalR:', err));
  }
}
