import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { BarService } from 'src/app/Utils/messages/BarService/bar.service';
import { environment } from 'src/environments/environment';
import { Student } from '../models/Student';

@Injectable({
  providedIn: 'root'
})
export class StudentsService {

  private urlController = '/api/students';

  constructor(private http : HttpClient,
    private messageBar : BarService) { }
  
  getUrl(_url: string): string {
    return environment.baseUrl + _url;
  }
  getHeaders() {
    return {
      headers: new HttpHeaders({ 'Content-Type': 'application/json'})
    };
  }

  getStudents(){
    return this.http.get(
      this.getUrl(this.urlController),
      this.getHeaders()
    ).pipe(
      catchError(this.handleError)
    );
  }  

  deleteStudent(id:number){
    let deleteUrl = this.getUrl(this.urlController)+"/"+id;
    return this.http.delete(
      deleteUrl,
      this.getHeaders()
    ).pipe(
      catchError(this.handleError)
    );
  }

  createStudent(_student:Student){
    _student.id=0;
    return this.http.post(
      this.getUrl(this.urlController),
      _student,
      this.getHeaders()      
    ).pipe(
      catchError(this.handleError)
    );
  }

  updateStudent(_student:Student){
    
    return this.http.put(
      this.getUrl(this.urlController),
      _student,
      this.getHeaders()      
    ).pipe(
      catchError(this.handleError)
    );
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
      
      return throwError('A client-side or network error occurred');
    } 
    console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);

    switch(error.status){
      case 500:
        return throwError('Internal server error, please contact customer support');
      case 400:
        return throwError(error.error);
      case 401:
        return throwError('No data found');
      default:
        return throwError('Service not found, please contact customer support');
    }
  }
}
