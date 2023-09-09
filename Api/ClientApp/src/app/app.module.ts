import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CoreModule } from './core/core.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { HomeModule } from './home/home.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserAnimationsModule,
    HomeModule,
    CoreModule,
    HttpClientModule,
    BrowserModule,
    AppRoutingModule,
    NgxSpinnerModule,
    NgbModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass : ErrorInterceptor, multi : true},
    {provide: HTTP_INTERCEPTORS, useClass : LoadingInterceptor, multi : true},
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
