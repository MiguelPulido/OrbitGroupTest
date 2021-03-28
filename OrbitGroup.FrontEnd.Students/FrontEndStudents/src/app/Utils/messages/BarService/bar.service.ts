import { Injectable, NgZone } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class BarService {

  constructor(public snackBar: MatSnackBar) { }
  
  openSnackBar(message: string, action: string) {
   
      this.snackBar.open(message, action, {
      duration: 8000,
      verticalPosition: 'top',
    });
  
  }  
}
