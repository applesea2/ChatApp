import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: false,
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username = '';
  password = '';
  error = '';

  constructor(private http: HttpClient, private router: Router) {}

  login() {
    this.http.post<{ token: string }>('https://localhost:7219/api/login/login', {
      username: this.username,
      password: this.password
    }).subscribe({
      next: (res) => {
        localStorage.setItem('jwtToken', res.token);
        this.error = '';
        // Redirect to chat component on successful login
        this.router.navigate(['/chat']);
      },
      error: () => {
        // Display error message on unsuccessful login
        this.error = 'Invalid username or password';
      }
    });
  }
}
