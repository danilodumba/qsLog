import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './layout/menu/menu.component';
import { FootComponent } from './layout/foot/foot.component';
import { HeadComponent } from './layout/head/head.component';
import { UserListComponent } from './users/user-list/user-list.component';
import { UserFormComponent } from './users/user-form/user-form.component';
import { ProjectFormComponent } from './project/project-form/project-form.component';
import { ProjectListComponent } from './project/project-list/project-list.component';
import { LogListComponent } from './logs/log-list/log-list.component';
import { LogViewComponent } from './logs/log-view/log-view.component';
import { SignInComponent } from './login/sign-in/sign-in.component';

@NgModule({
  declarations: [
    AppComponent,
    MenuComponent,
    FootComponent,
    HeadComponent,
    UserListComponent,
    UserFormComponent,
    ProjectFormComponent,
    ProjectListComponent,
    LogListComponent,
    LogViewComponent,
    SignInComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
