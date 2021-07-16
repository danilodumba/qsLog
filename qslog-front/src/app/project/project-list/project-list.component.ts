import { Component, OnInit } from '@angular/core';
import { CoreComponent } from 'src/app/core/core.component';
import { ProjectService } from '../services/project.service';
import Swal from 'sweetalert2'
import { AlertService } from 'src/app/core/alert.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ProjectFormComponent } from '../project-form/project-form.component';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent extends CoreComponent implements OnInit {

  projects: any[] = [];
  showLoading: boolean = false;
  bsModalRef!: BsModalRef;
  filter: string = '';

  constructor(
    private alertService: AlertService,
    private modalService: BsModalService,
    private projectService: ProjectService
  ) { 
    super();
  }

  ngOnInit(): void {
    this.list();
  }

  list() {
    this.showLoading = true;
    this.projectService.list(this.filter)
    .subscribe(p => {
      this.projects = p;
      this.showLoading = false;
    }, e =>{
      this.catchError(e);
    });
  }

  remove(id: string) {
    
    this.alertService.showQuestion('Tem muita certeza disso?', 'Deseja realmente remover esse projeto??', () => {
      this.projectService.remove(id)
      .subscribe(() => {
        this.alertService.showSuccessTimer('Projeto removido com sucesso.');
        this.list();
      });
    });
  }

  onChangeFilter(e: any) {
    let filter = e.target.value.trim();
    if (filter.length > 3) {
      this.filter = filter;
      this.list();
      return;
    } 

    if (filter == '') {
      this.filter = '';
      this.list();
    }
  }

  newItem() {
    this.showModal({});
  }

  edit(id: string) {
    const initialState = {
      id: id
    };

    this.showModal(initialState);
  }

  showModal(initialState: any) {
    this.bsModalRef = this.modalService.show(ProjectFormComponent, {initialState});

    this.bsModalRef.onHide.subscribe(() => {
      this.list();
    });
  }
}
