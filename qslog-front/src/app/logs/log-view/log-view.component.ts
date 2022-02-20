import { Component, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { AlertService } from 'src/app/core/alert.service';
import { CoreComponent } from 'src/app/core/core.component';
import { LogModel } from '../models/log.model';
import { LogService } from '../services/log.service';

@Component({
  selector: 'app-log-view',
  templateUrl: './log-view.component.html',
  styleUrls: ['./log-view.component.css']
})
export class LogViewComponent extends CoreComponent implements OnInit {

  logID!: string;
  logModel: LogModel = new LogModel();
  descricao: string = '';

  constructor(
    public bsModalRef: BsModalRef,
    public alertService: AlertService,
    private logService: LogService
  ) { 
    super(alertService);
  }

  ngOnInit(): void {
    this.getLog();
  }

  getLog() {
    this.startLoading();

    this.logService
    .getById(this.logID)
    .subscribe(l => {
      this.logModel = l;
      this.descricao = this.logModel.description;
      this.endLoading();
      console.log(this.logModel);
    }, e => {
      this.catchError(e);
    });
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
