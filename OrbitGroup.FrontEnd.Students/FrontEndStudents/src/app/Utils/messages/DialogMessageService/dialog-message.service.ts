import { Injectable } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AreYouSureComponent } from '../are-you-sure/are-you-sure.component';

@Injectable({
  providedIn: 'root'
})
export class DialogMessageService {
  private dialogRef;
  constructor(public dialog: MatDialog
    ) { }

    openDialogConfirm(_width:string, message:string) : Promise<boolean> {
      this.dialogRef = this.dialog.open(AreYouSureComponent, {
        width: _width,
        data: message
      });
  
      let promise = new Promise<boolean>((resolve, reject) => {
        this.dialogRef.afterClosed().toPromise()
      .then(
        res => { // Success
          //console.log(res);
        resolve(res==true);
        },
        msg => { // Error
        reject(false);
        });
      });
      return promise;
    }
    closeDialogConfirm(){
      try{        
      this.dialogRef.close();
      }
      catch{
        
      }
    }
}
