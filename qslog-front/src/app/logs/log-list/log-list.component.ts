import { Component, OnInit } from '@angular/core';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { AlertService } from 'src/app/core/alert.service';
import { CoreComponent } from 'src/app/core/core.component';
import { ProjectService } from 'src/app/project/services/project.service';
import { LogService } from '../services/log.service';
import * as moment from 'moment';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { LogViewComponent } from '../log-view/log-view.component';

@Component({
  selector: 'app-log-list',
  templateUrl: './log-list.component.html',
  styleUrls: ['./log-list.component.css']
})
export class LogListComponent extends CoreComponent implements OnInit {

  period: any = [];
  logs: any = [];
  projects: any = [];
  description = '';
  projectID = '';
  type = '';
  bsModalRef!: BsModalRef;

  constructor(
    public alertService: AlertService,
    private logService: LogService,
    private projectService: ProjectService,
    private localeService: BsLocaleService,
    private modalService: BsModalService,
  ) {
    super(alertService);
    this.localeService.use('pt-br');
   }

  ngOnInit(): void {

    this.period.push(new Date(moment().startOf('month').format()));
    this.period.push(new Date(moment().endOf('month').format()));

    this.get();
    this.getProjects();
  }

  get() {

    const initDate = moment(this.period[0]).format('YYYY-MM-DD');
    const endDate =  moment(this.period[1]).format('YYYY-MM-DD');


    this.startLoading();
    this.logService.list(initDate, endDate, this.description, this.type, this.projectID)
    .subscribe(l => {
      this.logs = l;
      this.endLoading();
    }, e => {
      this.catchError(e);
    });
  }

  getProjects() {
    this.projectService
    .list('')
    .subscribe(
      p => {
        this.projects = p;
      }, e => {
        this.catchError(e);
      })
  }

  viewLog(id: string) {
    const initialState = {
      logID: id
    };
    this.bsModalRef = this.modalService.show(LogViewComponent, {initialState, class: 'modal-lg'});
  }



  getStatusCss(type: number) : string{

    switch (type) {
      case 1:
        return 'label-light-info'
      case 2:
        return 'label-light-warning'
      case 3:
        return 'label-light-danger'
      default:
        return 'label-light-danger'
    }
  }

  getNameType(type: number) : string{

    switch (type) {
      case 1:
        return 'Info'
      case 2:
        return 'Alerta'
      case 3:
        return 'Erro'
      default:
        return 'Erro'
    }
  }

}
