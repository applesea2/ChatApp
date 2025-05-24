import { Component } from '@angular/core';
import { ChatService } from '../chat.service';
import { computed } from '@angular/core';

@Component({
  selector: 'app-chat',
  standalone: false,
  templateUrl: './chat.component.html',
  styleUrl: './chat.component.css'
})
export class ChatComponent {
  public messageContent: string = '';
  public userId: string = ''; // User ID for private messages
  public groupName: string = ''; // Group name for group messages
  public messages = computed(() => this.chatService.messages()); // Use computed to get the latest messages

  constructor(private chatService: ChatService) { }

  public sendMessage(): void {
    if (this.messageContent.trim()) {
      this.chatService.sendMessage(this.messageContent, this.userId, this.groupName);
      this.messageContent = '';
    }
  }
}
