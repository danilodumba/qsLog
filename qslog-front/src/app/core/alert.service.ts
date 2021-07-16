import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon } from 'sweetalert2'

@Injectable({
    providedIn: 'root'
  })
export class AlertService {

    public showSuccess(title: string, message: string) {
       this.showAlert('success', title, message);
    }

    public showWarning(title: string, message: string) {
        this.showAlert('warning', title, message);
    }

    public showInfo(title: string, message: string) {
        this.showAlert('info', title, message);
    }

    public showQuestion(title: string, message: string, yes: Function) {
        
        Swal.fire({
            title: title,
            text: message,
            icon: 'question',
            showCancelButton: true,
            confirmButtonText: 'Sim',
            cancelButtonText: 'Não',
            confirmButtonColor: '#1BC5BD',
            cancelButtonColor: '#d33',
        }).then((result) => {   
            if (result.isConfirmed) {
              yes();
            } 
        });
    }

    private showAlert(type: SweetAlertIcon, title: string, message: string) {
        Swal.fire({
            title: title,
            text: message,
            icon: type,
            confirmButtonColor: '#1BC5BD',
        });
    }

    public showSuccessTimer(title: string) {
        this.showMessageTimer('success', title);
     }

    private showMessageTimer(type: SweetAlertIcon, title: string) {
        Swal.fire({
            title: title,
            icon: type,
            showConfirmButton: false,
            timer: 2000,
            position: 'top-end',
        });
    }
}
