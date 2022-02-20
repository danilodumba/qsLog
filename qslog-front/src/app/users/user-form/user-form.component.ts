import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AlertService } from 'src/app/core/alert.service';
import { FormModalComponent } from 'src/app/core/form-modal.component';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css']
})
export class UserFormComponent extends FormModalComponent implements OnInit {
 
  isUpdate: boolean = false;

  constructor(
    public bsModalRef: BsModalRef,
    public alertService: AlertService,
    private userService: UserService
    ) {
    super(bsModalRef, alertService);
    this.createForm();
  }
 
  ngOnInit() {
    if (this.id) {
      this.title = 'Alterar usuário';
      this.get();
      this.isUpdate = true;
      return;
    }
    this.title = 'Incluir usuário';  
    this.isUpdate = false;
  }

  private get() {
    this.startLoading();
    this.userService.getById(this.id)
    .subscribe(u => {
      this.principalForm.setValue(u);
      this.endLoading();
    }, e => {
      this.catchError(e);
    })
  }

  createForm(): void {
    this.principalForm = new FormGroup({
      name: new FormControl('', Validators.required),
      email: new FormControl('', Validators.required),
      userName: new FormControl('', Validators.required),
      administrator: new FormControl('')
    });
  }

  save() {
    if (this.principalForm.valid) {
      if (this.isUpdate) {
        this.update();
      } else {
        this.create();
      }
    } else {
      this.principalForm.markAllAsTouched();
    }
  }

  resetPassword() {

    this.alertService.showQuestion('Tem certeza disso??', 'Deseja realmente atualizar a senha desse usuario?', () => {
      this.startLoading();
      this.userService.resetPassword(this.id)
      .subscribe(
        () => {
          this.alertService.showSuccess('Atualizado', 'A nova senha será 123456. Lembre o esperto de trocar em meus dados.');
          this.endLoading();
        }, e => {
          this.catchError(e);
        }
      )
    });
  }

  private create() {
    this.startLoading();
    
    this.userService.create(this.principalForm.value)
    .subscribe(() => {
      this.alertService.showSuccessTimer('Usuário criado com sucesso.');
      this.endLoading();
      this.close();
    }, e => {
      this.catchError(e);
    });
  }

  private update() {
    this.startLoading();
    this.userService.update(this.principalForm.value, this.id)
    .subscribe(() => {
      this.alertService.showSuccessTimer('Usuário alterado com sucesso.');
      this.endLoading();
      this.close();
    }, e => {
      this.catchError(e);
    });
  }
}