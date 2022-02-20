import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
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
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import localePtBr from '@angular/common/locales/pt';

import { registerLocaleData } from '@angular/common';
import { defineLocale } from 'ngx-bootstrap/chronos';
import { ptBrLocale } from 'ngx-bootstrap/locale';

registerLocaleData(localePtBr);
defineLocale('pt-br', ptBrLocale);

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
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BlockUIModule.forRoot(),
    ModalModule.forRoot(),
    BsDatepickerModule.forRoot(),
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: HttpClientInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
