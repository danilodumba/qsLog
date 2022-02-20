import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import Swal from 'sweetalert2/dist/sweetalert2.js';

@Injectable()
export class HttpClientInterceptor implements HttpInterceptor {

  constructor(
    private router: Router
  )
  {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = localStorage.getItem('token');

    if (token) {
        request = request.clone({
        setHeaders: {
        'Authorization': 'Bearer ' + token,
        'Content-Type':  'application/json'
        }
        });
    } else {
      this.goToLogin();
    }
    return next.handle(request).pipe(
      tap(() => {}, 
      error => {
      if (!token) {
        return;
      }
          if (error.status === 401) {
            this.showSessionExpired();
          }
          if (error.status === 403) {
            Swal.fire('Não autorizado.', 'Você não tem autorização para fazer essa operação.', 'warning');
          }
          else if(error.status >= 400 && error.status < 500) {
              //Swal.fire('Oops... Fique atento!', error.error, 'warning');
          }
          else if (error.status >= 500){
              //Swal.fire('Algo errado aconteceu!', 'Ocorreu um erro interno no servidor, tente novamente mais tarde ou entre em contrato com nosso suporte.', 'error');
          }
    })
  );
  }

  goToLogin() {
    this.router.navigateByUrl('/login');
  }

  showSessionExpired() {
    Swal.fire({
        title: 'Sessão Expirada',
        text: 'Por favor, faça novamente seu login no sistema.',
        icon: 'info',
        showCancelButton: false,
        confirmButtonText: 'Ok',
        confirmButtonColor: '#1BC5BD',
    }).then((result) => {   
        this.goToLogin();
    });
  }
}

