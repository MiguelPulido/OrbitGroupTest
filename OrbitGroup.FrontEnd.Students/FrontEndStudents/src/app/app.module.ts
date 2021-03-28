import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppMaterialModule } from './app-material/app-material.module';


import { SpinnerComponent } from './Utils/spinner/spinner.component';
import { AreYouSureComponent } from './Utils/messages/are-you-sure/are-you-sure.component';
import { HttpClientModule } from '@angular/common/http';
import { StudentsFormComponent } from './Students/components/students-form/students-form.component';
import { StudentsListComponent } from './Students/components/students-list/students-list.component';
import { StudentsViewComponent } from './Students/components/students-view/students-view.component';


@NgModule({
  declarations: [
    AppComponent,
    SpinnerComponent,
    AreYouSureComponent,
    StudentsViewComponent,
    StudentsFormComponent,
    StudentsListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    AppMaterialModule  
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
