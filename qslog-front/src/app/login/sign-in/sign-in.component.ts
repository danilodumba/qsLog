import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from 'src/app/core/alert.service';
import { FormComponent } from 'src/app/core/form.component';
import { UserService } from 'src/app/users/services/user.service';
import { environment } from 'src/environments/environment';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent extends FormComponent implements OnInit {
  
  versao: String = '';

  constructor(
    private router: Router,
    public alertService: AlertService,
    private loginService: LoginService,
    private userService: UserService
  ) { 
    super(alertService);
    this.createForm();
  }

  ngOnInit(): void {
    this.checkUserAdmin();
    this.versao = environment.versao;
  }

  createForm(): void {
    this.principalForm = new FormGroup({
      userName: new FormControl('', Validators.required),
      password: new FormControl('', Validators.required),
    });
  }

  login() {
    if (this.principalForm.valid) {
      this.startLoading();
      this.loginService.login(this.principalForm.value)
      .subscribe(l => {
        if (l.success) {
          localStorage.setItem('token', l.token);
          localStorage.setItem('name', l.name);
          localStorage.setItem('email', l.email);
          
          this.router.navigateByUrl('');
        } else {
          this.alertService.showWarning('Errou!!!', 'Login ou senha inválido');
        }
        this.endLoading();
      }, e => {
        this.catchError(e);
      })
    } 

    this.principalForm.markAllAsTouched();
  }

  checkUserAdmin() {
    this.userService.checkUserAdmin()
    .subscribe(
      () => {}, 
      error => {
        this.alertService.showError('Deu ruim!', 'Ocorreu um erro ao validar o usuario admin. Verifique o console log');
        this.catchError(error);
      })
  }
}
