import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: false,
})
export class LoginComponent {
  username = '';
  password = '';
  error = '';

  constructor(private http: HttpClient) {}

  login() {
    this.http.post<{ token: string }>('https://localhost:7219/api/login/login', {
      username: this.username,
      password: this.password
    }).subscribe({
      next: (res) => {
        localStorage.setItem('jwtToken', res.token);
        this.error = '';
        // Optionally, redirect or update UI
      },
      error: () => {
        this.error = 'Invalid username or password';
      }
    });
  }
}
