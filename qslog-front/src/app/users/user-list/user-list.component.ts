import { Component, OnInit } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AlertService } from 'src/app/core/alert.service';
import { CoreComponent } from 'src/app/core/core.component';
import { UserService } from '../services/user.service';
import { UserFormComponent } from '../user-form/user-form.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent extends CoreComponent implements OnInit {

  users: any[] = [];
  showLoading: boolean = false;
  bsModalRef!: BsModalRef;
  filter: string = '';

  constructor(
    private alertService: AlertService,
    private modalService: BsModalService,
    private userService: UserService
  ) { 
    super();
  }

  ngOnInit(): void {
    this.list();
  }

  list() {
    this.showLoading = true;
    this.userService.list(this.filter)
    .subscribe(p => {
      this.users = p;
      this.showLoading = false;
    }, e =>{
      this.catchError(e);
    });
  }

  remove(id: string) {
    this.alertService.showQuestion('Tem muita certeza disso?', 'Deseja realmente remover esse usuario??', () => {
      this.userService.remove(id)
      .subscribe(() => {
        this.alertService.showSuccessTimer('Usuário removido com sucesso.');
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
    this.bsModalRef = this.modalService.show(UserFormComponent, {initialState});

    this.bsModalRef.onHide.subscribe(() => {
      this.list();
    });
  }
}