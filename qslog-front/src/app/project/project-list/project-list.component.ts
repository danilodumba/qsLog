import { Component, OnInit } from '@angular/core';
import { CoreComponent } from 'src/app/core/core.component';
import { ProjectService } from '../services/project.service';

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.css']
})
export class ProjectListComponent extends CoreComponent implements OnInit {

  projects: any[] = [];
  name: string = '';
  constructor(
    private projectService: ProjectService
  ) { 
    super();
  }

  ngOnInit(): void {
    this.list();
  }

  list() {
    this.startLoading();
    this.projectService.list()
    .subscribe(p => {
      this.projects = p;
      this.endLoading();
    }, e =>{
      this.endLoading();
    });
  }

  delete(id: string) {
    this.list();
  }
}
