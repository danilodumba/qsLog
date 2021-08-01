import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MasterComponent } from './layout/master/master.component';
import { SignInComponent } from './login/sign-in/sign-in.component';
import { LogListComponent } from './logs/log-list/log-list.component';
import { ProjectListComponent } from './project/project-list/project-list.component';
import { UserListComponent } from './users/user-list/user-list.component';

const routes: Routes = [
  { path: 'login', component: SignInComponent },
  { path: '', component: MasterComponent,
    children: [
      { path: '', component: LogListComponent },
      { path: 'log', component: LogListComponent },
      { path: 'user', component: UserListComponent },
      { path: 'project', component: ProjectListComponent },
    ] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
