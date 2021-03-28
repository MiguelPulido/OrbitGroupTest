import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { StudentsViewComponent } from './Students/components/students-view/students-view.component';

const routes: Routes = [
  {path: "students", component: StudentsViewComponent},
{path:"",redirectTo:"students", pathMatch:"full"}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
