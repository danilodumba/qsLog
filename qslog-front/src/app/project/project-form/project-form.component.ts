import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AlertService } from 'src/app/core/alert.service';
import { FormModalComponent } from 'src/app/core/form-modal.component';
import { ProjectService } from '../services/project.service';

@Component({
  selector: 'app-project-form',
  templateUrl: './project-form.component.html',
  styleUrls: ['./project-form.component.css']
})
export class ProjectFormComponent extends FormModalComponent implements OnInit {
 
  isUpdate: boolean = false;

  constructor(
    public bsModalRef: BsModalRef,
    public alertService: AlertService,
    private projectService: ProjectService
    ) {
    super(bsModalRef, alertService);
    this.createForm();
  }
 
  ngOnInit() {
    if (this.id) {
      this.title = 'Alterar projeto';
      this.get();
      this.isUpdate = true;
      return;
    }
    this.title = 'Incluir projeto';  
    this.isUpdate = false;
  }

  private get() {
    this.startLoading();
    this.projectService.getById(this.id)
    .subscribe(p => {
      console.log(p);
      var project = {
        name: p.name,
        apiKey: p.apiKey
      };

      this.principalForm.setValue(project);
      this.endLoading();
    }, e => {
      this.catchError(e);
    })
  }

  createForm(): void {
    this.principalForm = new FormGroup({
      name: new FormControl('', Validators.required),
      apiKey: new FormControl('')
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

  private create() {
    this.startLoading();
    var project = {
      name: this.principalForm.value.name
    };

    this.projectService.create(project)
    .subscribe(() => {
      this.alertService.showSuccessTimer('Projeto criado com sucesso.');
      this.endLoading();
      this.close();
    }, e => {
      this.catchError(e);
    });
  }

  private update() {
    this.startLoading();
    this.projectService.update(this.principalForm.value, this.id)
    .subscribe(() => {
      this.alertService.showSuccessTimer('Projeto alterado com sucesso.');
      this.endLoading();
      this.close();
    }, e => {
      this.catchError(e);
    });
  }
}
