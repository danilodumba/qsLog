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
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BlockUIModule } from 'ng-block-ui';
import { ModalModule } from 'ngx-bootstrap/modal';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientInterceptor } from './core/httpClient.interceptor';
import { MasterComponent } from './layout/master/master.component';
import { MenuProfileComponent } from './profile/menu-profile/menu-profile.component';
import { UpdadePasswordComponent } from './profile/updade-password/updade-password.component';
import { MyProfileComponent } from './profile/my-profile/my-profile.component';

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
    SignInComponent,
    MasterComponent,
    MenuProfileComponent,
    UpdadePasswordComponent,
    MyProfileComponent
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BlockUIModule.forRoot(),
    ModalModule.forRoot()
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: HttpClientInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
