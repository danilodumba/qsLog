import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AlertService } from 'src/app/core/alert.service';
import { FormComponent } from 'src/app/core/form.component';
import { ProfileModel } from '../models/profile.model';
import { ProfileService } from '../services/profile.service';

@Component({
  selector: 'app-my-profile',
  templateUrl: './my-profile.component.html',
  styleUrls: ['./my-profile.component.css']
})
export class MyProfileComponent extends FormComponent implements OnInit {
  

  constructor(
    public alertService: AlertService,
    private profileService: ProfileService
  ) { 
    super(alertService);
    this.createForm();
  }

  ngOnInit(): void {
    this.get();
  }

  get() {
    this.startLoading();
    this.profileService
    .getProfile()
    .subscribe(
      p => {
        p.password = '',
        p.confirmPassword = '';
        p.oldPassword = '';
        this.principalForm.setValue(p);
        this.endLoading();
      },
      e => {
        this.catchError(e);
      }
    )
  }

  save() {
    if (this.principalForm.valid) {

      const model: ProfileModel = this.principalForm.value;
      if (model.password != model.confirmPassword) {
        this.alertService.showWarning('Digita certo ai!', 'Senhas não conferem');
        return;
      }

      this.startLoading();
      this.profileService
      .savePassword(model)
      .subscribe(
        () => {
          this.alertService.showSuccess('Sucesso!', 'Senha atualizada com sucesso!');
          this.endLoading();
          this.get();
        }, e => {
          this.catchError(e);
        });
    }

    this.principalForm.markAllAsTouched();
  }

  createForm(): void {
    this.principalForm = new FormGroup({
      name: new FormControl(''),
      email: new FormControl(''),
      userName: new FormControl(''),
      oldPassword: new FormControl('', Validators.required), 
      password: new FormControl('', Validators.required), 
      confirmPassword: new FormControl('', Validators.required)
    });
  }
}
